using HtmlAgilityPack;
using Microsoft.Extensions.Options;
using VacanciesScrapper_Utils.Enums;
using VacanciesScrapper_Utils.Utils;
using VacanciesScrapper_BLL.Models;
using VacanciesScrapper_BLL.Services.Interfaces;
using VacanciesScrapper_BLL.Services.Logging;
using VacanciesScrapper_BLL.Switches;
using VacanciesScrapper_Utils.Options;

namespace VacanciesScrapper_BLL.Services.Realizations
{
    public class DouVacanciesService : IDouVacanciesService
    {
        private IScrapperService _scrapperService;
        private ILoggerService _logger;
        private JobSitesUrlsOptions _options;

        public DouVacanciesService(IScrapperService scrapperService, ILoggerService logger,
            IOptions<JobSitesUrlsOptions> options)
        {
            _scrapperService = scrapperService;
            _logger = logger;
            _options = options.Value;
        }

        public async Task<IEnumerable<Vacancy>> GetAllDouVacanciesByCategory(Categories? cat, YearsOfExperience? exp)
        {
            var url = _options.DouBaseUrl + CategoriesDou.GetCategory(cat) + CategoriesDou.GetExperience(exp);

            var document = await _scrapperService.GetHtml(url);

            var nodes = document.DocumentNode.SelectNodes("//li[@class='l-vacancy']");

            var vacancy = new List<Vacancy>();

            if (nodes is null)
            {
                _logger.LogError(nodes, "nodes not found");
                return vacancy;
            }

            foreach (var node in nodes)
            {
                var salaryNode = node.SelectSingleNode(".//div[@class='title']/span[@class='salary']");
                var salary = salaryNode is null ? string.Empty : salaryNode.InnerText;
                var date = node.SelectSingleNode(".//div[@class='date']").InnerText;
                var title = node.SelectSingleNode(".//div[@class='title']/a[@class='vt']").InnerText;
                var location = node.SelectSingleNode(".//div[@class='title']/span[@class='cities']").InnerText;
                var shortDescription = node.SelectSingleNode(".//div[@class='sh-info']").InnerText.Trim();
                var company = node.SelectSingleNode(".//div[@class='title']/strong/a[@class='company']").InnerText
                    .Trim();
                var link = node.SelectSingleNode(".//div[@class='title']/a[@class='vt']").Attributes["href"].Value;
                var companyImgNode = node.SelectSingleNode(".//div[@class='title']/strong/a[@class='company']/img");
                var companyImg = companyImgNode is null
                    ? "https://ui-avatars.com/api/?name=" + company
                    : companyImgNode.Attributes["src"].Value;

                CodeCleaner.ScrubHtml(ref title);
                CodeCleaner.ScrubHtml(ref location);
                CodeCleaner.ScrubHtml(ref shortDescription);
                CodeCleaner.ScrubHtml(ref company);
                CodeCleaner.ScrubHtml(ref salary);

                vacancy.Add(new Vacancy
                {
                    CreationDate = date,
                    Title = title,
                    Location = location,
                    shortDescription = shortDescription,
                    Company = company,
                    Link = link,
                    Salary = salary,
                    CompanyImg = companyImg,
                    //Description = await GetFullDescription(link)
                });
            }

            return vacancy;
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