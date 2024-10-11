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
        private static HttpClient _client = new();
		public DouVacancies()
		{
		}

        public async static Task<IEnumerable<ShortVacancy>> GetShortVacanciesByCategory(Categories cat, YearsOfExperience? exp)
        {
            _client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (X11; Linux x86_64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/88.0.4324.96 Safari/537.36");
            
            var url = "https://jobs.dou.ua/" + CategoriesDou.GetCategory(cat);

            if(exp is not null)
            {
                url += CategoriesDou.GetExperience(exp);
            }

            HttpResponseMessage response = await _client.GetAsync(url);
            response.EnsureSuccessStatusCode(); // Throw if not a success code

            // Get the response content as a string
            string pageContent = await response.Content.ReadAsStringAsync();
                
            // Load the page content into an HtmlDocument
            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(pageContent);


            var nodes = document.DocumentNode.SelectNodes("//li[@class='l-vacancy']");

            var shortVacancy = new List<ShortVacancy>();
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
                var companyImgNode = node.SelectSingleNode(".//div[@class='title']/strong/a[@class='company']/img").Attributes["src"].Value;
                var companyImg = companyImgNode is null ? string.Empty : companyImgNode;
                var fullVacancy = await GetFullVacancy(link);

                CodeCleaner.ScrubHtml(ref title);
                CodeCleaner.ScrubHtml(ref location);
                CodeCleaner.ScrubHtml(ref shortDescription);
                CodeCleaner.ScrubHtml(ref company);
                CodeCleaner.ScrubHtml(ref salary);

                shortVacancy.Add(new ShortVacancy
                {
                    CreationDate = date,
                    Title = title,
                    Location = location,
                    ShortDescription = shortDescription,
                    Company = company,
                    Link = link,
                    Salary = salary,
                    CompanyImg = companyImg,
                    Description = fullVacancy.Description
                });
            }

            return shortVacancy;
        }

        private static async Task<Vacancy> GetFullVacancy(string vacancyLink)
        {
            
            _client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (X11; Linux x86_64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/88.0.4324.96 Safari/537.36");
            
            HttpResponseMessage response = await _client.GetAsync(vacancyLink);
            response.EnsureSuccessStatusCode(); // Throw if not a success code

            // Get the response content as a string
            string pageContent = await response.Content.ReadAsStringAsync();
                
            // Load the page content into an HtmlDocument
            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(pageContent);
            
            var fullPageNodes = document.DocumentNode.SelectNodes(".//div[@class='l-vacancy']");
            var descriptionNodes = document.DocumentNode.SelectNodes(".//div[@class='l-vacancy']/div[@class='b-typo vacancy-section']");

            Vacancy fullVacancy = new();
            
            foreach (var node in descriptionNodes)
            {
                var pNodes = node.SelectNodes(".//p");
                var h3nodes = node.SelectNodes(".//h3[@class='g-h3']");
                
                var description = "";
                
                if (descriptionNodes is not null)
                {
                    foreach (var p in pNodes)
                    {
                        description += p.InnerText + "\n";
                    }
                }
                
                //CodeCleaner.ScrubHtml(ref titleOfDescription);
                CodeCleaner.ScrubHtml(ref description);
                
                fullVacancy.Description += /*titleOfDescription + "\n" + */ description + "\n";
            }
            
            return fullVacancy;
        }
    }
}

