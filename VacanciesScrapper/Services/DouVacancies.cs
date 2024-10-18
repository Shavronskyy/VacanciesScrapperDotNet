using System;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using HtmlAgilityPack;
using VacanciesScrapper.Enums;
using VacanciesScrapper.Models;
using VacanciesScrapper.Switches;
using VacanciesScrapper.Utils;

namespace VacanciesScrapper.Services
{
	public class DouVacancies
    {
        private static HttpClient _client;
		static DouVacancies()
        {
            _client = new();
            _client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (X11; Linux x86_64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/88.0.4324.96 Safari/537.36");
		}

        public async static Task<IEnumerable<Vacancy>> GetShortVacanciesByCategory(Categories? cat, YearsOfExperience? exp)
        {
            var url = "https://jobs.dou.ua/" + CategoriesDou.GetCategory(cat) + CategoriesDou.GetExperience(exp);

            HttpResponseMessage response = await _client.GetAsync(url);
            response.EnsureSuccessStatusCode(); // Throw if not a success code

            // Get the response content as a string
            string pageContent = await response.Content.ReadAsStringAsync();
                
            // Load the page content into an HtmlDocument
            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(pageContent);


            var nodes = document.DocumentNode.SelectNodes("//li[@class='l-vacancy']");

            var shortVacancy = new List<Vacancy>();
            foreach (var node in nodes)
            {
                var salaryNode = node.SelectSingleNode(".//div[@class='title']/span[@class='salary']");
                var salary = salaryNode is null ? string.Empty : salaryNode.InnerText;
                var date = node.SelectSingleNode(".//div[@class='date']").InnerText;
                var title = node.SelectSingleNode(".//div[@class='title']/a[@class='vt']").InnerText;
                var location = node.SelectSingleNode(".//div[@class='title']/span[@class='cities']").InnerText;
                var shortDescription = node.SelectSingleNode(".//div[@class='sh-info']").InnerText.Trim();
                var company = node.SelectSingleNode(".//div[@class='title']/strong/a[@class='company']").InnerText.Trim();
                var link = node.SelectSingleNode(".//div[@class='title']/a[@class='vt']").Attributes["href"].Value;
                var companyImgNode = node.SelectSingleNode(".//div[@class='title']/strong/a[@class='company']/img");
                var companyImg = companyImgNode is null ? "https://ui-avatars.com/api/?name=" + company : companyImgNode.Attributes["src"].Value;

                CodeCleaner.ScrubHtml(ref title);
                CodeCleaner.ScrubHtml(ref location);
                CodeCleaner.ScrubHtml(ref shortDescription);
                CodeCleaner.ScrubHtml(ref company);
                CodeCleaner.ScrubHtml(ref salary);

                shortVacancy.Add(new Vacancy
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

            return shortVacancy;
        }

        private static async Task<string> GetFullDescription(string vacancyLink)
        {
            HttpResponseMessage response = await _client.GetAsync(vacancyLink);
            response.EnsureSuccessStatusCode(); // Throw if not a success code

            // Get the response content as a string
            string pageContent = await response.Content.ReadAsStringAsync();
                
            // Load the page content into an HtmlDocument
            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(pageContent);
            
            var descriptionNodes = document.DocumentNode.SelectNodes(".//div[@class='l-vacancy']/div[@class='b-typo vacancy-section']");
            
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

