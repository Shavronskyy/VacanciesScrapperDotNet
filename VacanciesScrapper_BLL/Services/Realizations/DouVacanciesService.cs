using HtmlAgilityPack;
using Microsoft.Extensions.Options;
using VacanciesScrapper_Utils.Enums;
using VacanciesScrapper_Utils.Utils;
using VacanciesScrapper_BLL.Models;
using VacanciesScrapper_BLL.Services.Interfaces;
using VacanciesScrapper_BLL.Services.Logging;
using VacanciesScrapper_BLL.Switches;
using VacanciesScrapper_Utils.Options;
using VacanciesScrapper_DAL.Repositories.Interfaces.Base;
using AutoMapper;
using VacanciesScrapper_DAL.Entities;

namespace VacanciesScrapper_BLL.Services.Realizations
{
    public class DouVacanciesService : IDouVacanciesService
    {
        private readonly IScrapperService _scrapperService;
        private readonly ILoggerService _logger;
        private readonly JobSitesUrlsOptions _options;
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IMapper _mapper;

        public DouVacanciesService(IScrapperService scrapperService, ILoggerService logger,
            IOptions<JobSitesUrlsOptions> options, IRepositoryWrapper repositoryWrapper, IMapper mapper)
        {
            _scrapperService = scrapperService;
            _logger = logger;
            _options = options.Value;
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
        }

        public async Task GetAllDouVacanciesByCategory(Categories? cat, YearsOfExperience? exp)
        {
            var url = _options.DouBaseUrl + CategoriesDou.GetCategory(cat) + CategoriesDou.GetExperience(exp);

            var document = await _scrapperService.GetHtml(url);

            var nodes = document.DocumentNode.SelectNodes("//li[@class='l-vacancy']");

            if (nodes is null)
            {
                _logger.LogError(nodes, "nodes not found");
                return;
            }

            foreach (var node in nodes)
            {
                var link = node.SelectSingleNode(".//div[@class='title']/a[@class='vt']").Attributes["href"].Value;
                var isExistVacancy = await _repositoryWrapper.VacanciesRepository.GetFirstOrDefaultAsync(x => x.Link == link);
                if (isExistVacancy == null)
                {
                    break;
                }

                var salaryNode = node.SelectSingleNode(".//div[@class='title']/span[@class='salary']");
                var salary = salaryNode is null ? string.Empty : salaryNode.InnerText;
                var date = node.SelectSingleNode(".//div[@class='date']").InnerText;
                var title = node.SelectSingleNode(".//div[@class='title']/a[@class='vt']").InnerText;
                var location = node.SelectSingleNode(".//div[@class='title']/span[@class='cities']").InnerText;
                var shortDescription = node.SelectSingleNode(".//div[@class='sh-info']").InnerText.Trim();
                var companyTitle = node.SelectSingleNode(".//div[@class='title']/strong/a[@class='company']").InnerText
                    .Trim();
                var companyImgNode = node.SelectSingleNode(".//div[@class='title']/strong/a[@class='company']/img");
                var companyImg = companyImgNode is null
                    ? "https://ui-avatars.com/api/?name=" + companyTitle
                    : companyImgNode.Attributes["src"].Value;

                CodeCleaner.ScrubHtml(ref title);
                CodeCleaner.ScrubHtml(ref location);
                CodeCleaner.ScrubHtml(ref shortDescription);
                CodeCleaner.ScrubHtml(ref companyTitle);
                CodeCleaner.ScrubHtml(ref salary);

                var vacancy = new VacancyDto
                {
                    CreationDate = date,
                    Title = title,
                    Location = location,
                    ShortDescription = shortDescription,
                    Link = link,
                    Salary = salary,
                    Description = await GetFullDescription(link)
                };

                var company = new CompanyDto()
                {
                    Title = companyTitle,
                    Locations = location,
                    CompanyImg = companyImg
                };

                var result = _mapper.Map<Vacancy>(vacancy);

                await Create(vacancy, company);
            }
        }

        private async Task Create(VacancyDto vacancyDto, CompanyDto companyDto)
        {
            var company = await _repositoryWrapper.CompaniesRepository.GetFirstOrDefaultAsync(x => x.Title == companyDto.Title);
            if (company == null)
            {
                company = new Company
                {
                    Title = companyDto.Title,
                    Locations = new List<string>() { vacancyDto.Location },
                    CompanyImg = companyDto.CompanyImg
                };
                await _repositoryWrapper.CompaniesRepository.CreateAsync(company);
            }
            var vacancy = _mapper.Map<Vacancy>(vacancyDto);
            vacancy.Company = company;
            await _repositoryWrapper.VacanciesRepository.CreateAsync(vacancy);
        }

        private async Task<string> GetFullDescription(string url)
        {
            var document = await _scrapperService.GetHtml(url);

            var descriptionNodes =
                document.DocumentNode.SelectNodes(".//div[@class='l-vacancy']/div[@class='b-typo vacancy-section']");

            var description = string.Empty;

            var innerTexts = descriptionNodes.Descendants()
                .Where(n => n.NodeType == HtmlNodeType.Text && !string.IsNullOrWhiteSpace(n.InnerText))
                .Select(n => n.InnerText.Trim());

            foreach (var text in innerTexts)
            {
                description += text + "\n";
            }

            CodeCleaner.ScrubHtml(ref description);

            return description;
        }
    }
}