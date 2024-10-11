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

		public async static Task<IEnumerable<ShortVacancy>> GetAllVacancies(Categories cat, YearsOfExperience? exp)
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



			var nodes = document.DocumentNode.SelectNodes("//ul[@class='list-unstyled list-jobs mb-4']/li[@class='mb-4']");

            var result = new List<ShortVacancy>();
            foreach (var node in nodes)
            {
	            var salaryNode = node.SelectSingleNode(".//h3[@class='mb-2']/strong[@class='text-success']/span[@class='public-salary-item']");
	            var salary = salaryNode is null ? string.Empty : salaryNode.InnerText;
                var title = node.SelectSingleNode(".//h3[@class='mb-2']/a[@class='job-item__title-link']").InnerText;
                var location = node.SelectSingleNode(".//span[@class='location-text']").InnerText;
                var shortDescription = node.SelectSingleNode(".//span[@class='js-truncated-text']").InnerText.Trim();
                var company = node.SelectSingleNode(".//a[@class='text-body']").InnerText.Trim();
                var link = node.SelectSingleNode(".//h3[@class='mb-2']/a").Attributes["href"].Value;
                var companyImgNode = node.SelectSingleNode(".//img[@class='userpic-image userpic-image_img']").Attributes["src"].Value;
                var companyImg = companyImgNode is null ? string.Empty : companyImgNode;

                CodeCleaner.ScrubHtml(ref title);
                CodeCleaner.ScrubHtml(ref location);
                CodeCleaner.ScrubHtml(ref shortDescription);
                CodeCleaner.ScrubHtml(ref company);
                CodeCleaner.ScrubHtml(ref salary);

                result.Add(new ShortVacancy
                {
                    Title = title,
                    Location = location,
                    ShortDescription = shortDescription,
                    Company = company,
                    Link = "djinni.co" + link,
                    Salary = salary,
                    CompanyImg = companyImg
                });
            }

            return result;
        }
	}
}

