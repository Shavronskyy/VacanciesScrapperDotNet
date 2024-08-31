using HtmlAgilityPack;
using VacanciesScrapper.Enums;
using VacanciesScrapper.Models;
using VacanciesScrapper.Switches;
using VacanciesScrapper.Utils;

namespace VacanciesScrapper.Services
{
	public class DjinniVacancies
	{
		public DjinniVacancies()
		{
			
		}

		public async static Task<IEnumerable<Vacancy>> GetAllVacancies(Categories cat, YearsOfExperience? exp)
		{

			HttpClient client = new();

			client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (X11; Linux x86_64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/88.0.4324.96 Safari/537.36");

			var url = "https://djinni.co/jobs/" + CategoriesDjinni.GetCategory(cat);

            if (exp is not null)
            {
                url += CategoriesDjinni.GetExperience(exp);
            }

            HttpResponseMessage response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode(); // Throw if not a success code

            // Get the response content as a string
            string pageContent = await response.Content.ReadAsStringAsync();

            // Load the page content into an HtmlDocument
            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(pageContent);



			var nodes = document.DocumentNode.SelectNodes("//ul[@class='list-unstyled list-jobs mb-4']/li[@class='mb-5']");

            var result = new List<Vacancy>();
            foreach (var node in nodes)
            {
                //var salary = node.SelectSingleNode(".//h3[@class='mb-2']/strong[@class='text-success']/span[@class='public-salary-item']").InnerText;
                var unTrimTitle = node.SelectSingleNode(".//h3[@class='mb-2']/a[@class='job-item__title-link']").InnerText;
                var unTrimLocation = node.SelectSingleNode(".//span[@class='location-text']").InnerText;
                var unTrimShortDescription = node.SelectSingleNode(".//span[@class='js-truncated-text']").InnerText.Trim();
                var unTrimCompany = node.SelectSingleNode(".//a[@class='text-body']").InnerText.Trim();

                var title = CodeCleaner.ScrubHtml(unTrimTitle);
                var location = CodeCleaner.ScrubHtml(unTrimLocation);
                var shortDescription = CodeCleaner.ScrubHtml(unTrimShortDescription);
                var company = CodeCleaner.ScrubHtml(unTrimCompany);

                result.Add(new Vacancy
                {
                    Title = title,
                    Location = location,
                    ShortDescription = shortDescription,
                    Company = company,
                    //Salary = salary is null ? salary : "?",
                });
            }

            return result;
        }
	}
}

