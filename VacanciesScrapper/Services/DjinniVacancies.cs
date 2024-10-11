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



			var nodes = document.DocumentNode.SelectNodes("//ul[@class='list-unstyled list-jobs mb-4']/li[@class='mb-4']");

            var result = new List<Vacancy>();
            foreach (var node in nodes)
            {
	            var salaryNode = node.SelectSingleNode(".//h3[@class='mb-2']/strong[@class='text-success']/span[@class='public-salary-item']");
	            var salary = salaryNode is null ? string.Empty : salaryNode.InnerText;
                var title = node.SelectSingleNode(".//h3[@class='mb-2']/a[@class='job-item__title-link']").InnerText;
                var location = node.SelectSingleNode(".//span[@class='location-text']").InnerText;
                var shortDescription = node.SelectSingleNode(".//span[@class='js-truncated-text']").InnerText.Trim();
                var company = node.SelectSingleNode(".//a[@class='text-body']").InnerText.Trim();
                var link = "https://djinni.co" + node.SelectSingleNode(".//h3[@class='mb-2']/a").Attributes["href"].Value;
                var companyImgNode = node.SelectSingleNode(".//img[@class='userpic-image userpic-image_img']").Attributes["src"].Value;
                var companyImg = companyImgNode is null ? string.Empty : companyImgNode; 
                //var fullDescription = await GetFullDescription(link);

                CodeCleaner.ScrubHtml(ref title);
                CodeCleaner.ScrubHtml(ref location);
                CodeCleaner.ScrubHtml(ref shortDescription);
                CodeCleaner.ScrubHtml(ref company);
                CodeCleaner.ScrubHtml(ref salary);

                result.Add(new Vacancy
                {
                    Title = title,
                    Location = location,
                    shortDescription = shortDescription,
                    Company = company,
                    Link = link,
                    Salary = salary,
                    CompanyImg = companyImg,
                    //Description = fullDescription
                });
            }

            return result;
        }

		private static async Task<string> GetFullDescription(string vacancyLink)
		{
			HttpClient client = new();
			
			client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (X11; Linux x86_64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/88.0.4324.96 Safari/537.36");
			HttpResponseMessage response = await client.GetAsync(vacancyLink);
			response.EnsureSuccessStatusCode(); // Throw if not a success code

			// Get the response content as a string
			string pageContent = await response.Content.ReadAsStringAsync();
                
			// Load the page content into an HtmlDocument
			HtmlDocument document = new HtmlDocument();
			document.LoadHtml(pageContent);
            
			var vacancyNodes = document.DocumentNode.SelectSingleNode(".//div[@class='page-content']/div/div[@class='row']");
			var descriptionNodes = vacancyNodes.SelectNodes("./div").Take(2);
			
			var description = string.Empty;
			foreach (var p in descriptionNodes)
			{
				var innerTexts = p.Descendants()
					.Where(n => n.NodeType == HtmlNodeType.Text && !string.IsNullOrWhiteSpace(n.InnerText))
					.Select(n => n.InnerText.Trim());

				foreach (var text in innerTexts)
				{
					description += text + "\n";
				}
            
				CodeCleaner.ScrubHtml(ref description);	
			}
            
			return description;
		}
	}
}

