﻿using HtmlAgilityPack;
using Microsoft.Extensions.Options;
using VacanciesScrapper_Utils.Enums;
using VacanciesScrapper_BLL.Models;
using VacanciesScrapper_BLL.Services.Interfaces;
using VacanciesScrapper_BLL.Services.Logging;
using VacanciesScrapper_BLL.Switches;
using VacanciesScrapper_Utils.Options;
using VacanciesScrapper_Utils.Utils;

namespace VacanciesScrapper_BLL.Services.Realizations
{
    public class DjinniVacanciesService : IDjinniVacanciesService
    {
        private readonly IScrapperService _scrapperService;
        private readonly ILoggerService _logger;
        private readonly JobSitesUrlsOptions _options;

        public DjinniVacanciesService(IScrapperService scrapperService,
            ILoggerService logger, IOptions<JobSitesUrlsOptions> options)
        {
            _scrapperService = scrapperService;
            _logger = logger;
            _options = options.Value;
        }

        public async Task<IEnumerable<VacancyDto>> GetAllDjinniVacanciesByCategory(Categories? cat, YearsOfExperience? exp)
        {
            var url = _options.DjinniBaseUrl + CategoriesDjinni.GetCategory(cat) + CategoriesDjinni.GetExperience(exp);

            var document = await _scrapperService.GetHtml(url);

            var nodes = document.DocumentNode.SelectNodes(
                "//ul[@class='list-unstyled list-jobs mb-4']/li[@class='mb-4']");

            var result = new List<VacancyDto>();

            if (nodes is null)
            {
                _logger.LogError(nodes, "nodes not found");
                return result;
            }

            foreach (var node in nodes)
            {
                var salaryNode =
                    node.SelectSingleNode(
                        ".//h3[@class='mb-2']/strong[@class='text-success']/span[@class='public-salary-item']");
                var salary = salaryNode is null ? string.Empty : salaryNode.InnerText;
                var title = node.SelectSingleNode(".//h3[@class='mb-2']/a[@class='job-item__title-link']").InnerText;
                var location = node.SelectSingleNode(".//span[@class='location-text']").InnerText;
                var shortDescription = node.SelectSingleNode(".//span[@class='js-truncated-text']").InnerText.Trim();
                var company = node.SelectSingleNode(".//a[@class='text-body']").InnerText.Trim();
                var link = "https://djinni.co" +
                           node.SelectSingleNode(".//h3[@class='mb-2']/a").Attributes["href"].Value;
                var companyImgNode = node.SelectSingleNode(".//img[@class='userpic-image userpic-image_img']");
                var companyImg = companyImgNode is null
                    ? "https://ui-avatars.com/api/?name=" + company
                    : companyImgNode.Attributes["src"].Value;
                //var date = await GetVacancyCreationDate(link);
                var fullDescription = await GetFullDescription(link);

                CodeCleaner.ScrubHtml(ref title);
                CodeCleaner.ScrubHtml(ref location);
                CodeCleaner.ScrubHtml(ref shortDescription);
                CodeCleaner.ScrubHtml(ref company);
                CodeCleaner.ScrubHtml(ref salary);

                result.Add(new VacancyDto
                {
                    Title = title,
                    Location = location,
                    ShortDescription = shortDescription,
                    Company = company,
                    Link = link,
                    Salary = salary,
                    CompanyImg = companyImg,
                    //CreationDate = date,
                    //Description = fullDescription
                });
            }

            return result;
        }

        private async Task<string> GetFullDescription(string url)
        {
            var document = await _scrapperService.GetHtml(url);

            var descriptionNodes =
                document.DocumentNode.SelectSingleNode(".//div[@class='mb-4 job-post__description']");

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