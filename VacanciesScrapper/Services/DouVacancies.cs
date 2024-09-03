using System;
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
		public DouVacancies()
		{
		}

        public async static Task<IEnumerable<Vacancy>> GetAllVacanciesByCategory(Categories cat, YearsOfExperience? exp)
        {

            HttpClient client = new();

            client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (X11; Linux x86_64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/88.0.4324.96 Safari/537.36");

            var url = "https://jobs.dou.ua/" + CategoriesDou.GetCategory(cat);

            if(exp is not null)
            {
                url += CategoriesDou.GetExperience(exp);
            }

            HttpResponseMessage response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode(); // Throw if not a success code

            // Get the response content as a string
            string pageContent = await response.Content.ReadAsStringAsync();
                
            // Load the page content into an HtmlDocument
            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(pageContent);


            var nodes = document.DocumentNode.SelectNodes("//li[@class='l-vacancy']");

            var result = new List<Vacancy>();
            foreach (var node in nodes)
            {
                var date = node.SelectSingleNode(".//div[@class='date']").InnerText;
                var title = node.SelectSingleNode(".//div[@class='title']/a[@class='vt']").InnerText;
                var location = node.SelectSingleNode(".//div[@class='title']/span[@class='cities']").InnerText;
                var shortDescription = node.SelectSingleNode(".//div[@class='sh-info']").InnerText.Trim();
                var company = node.SelectSingleNode(".//div[@class='title']/strong/a[@class='company']").InnerText.Trim();
                var link = node.SelectSingleNode(".//div[@class='title']/a[@class='vt']").Attributes["href"].Value.Trim();

                CodeCleaner.ScrubHtml(ref title);
                CodeCleaner.ScrubHtml(ref location);
                CodeCleaner.ScrubHtml(ref shortDescription);
                CodeCleaner.ScrubHtml(ref company);

                result.Add(new Vacancy
                {
                    CreationDate = date,
                    Title = title,
                    Location = location,
                    ShortDescription = shortDescription,
                    Company = company,
                    Link = link
                });
            }

            return result;
        }
    }
}

