using HtmlAgilityPack;
using VacanciesScrapper.Enums;
using VacanciesScrapper.Switches;

namespace VacanciesScrapper.Services
{
	public class DjinniVacancies
	{
		public DjinniVacancies()
		{
			
		}

		public async static Task<IEnumerable<string>> GetAllVacancies(Categories cat, YearsOfExperience? exp)
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



			var nodes = document.DocumentNode.SelectNodes("//a[@class='job-item__title-link']");

			var result = new List<string>();
			foreach(var n in nodes)
			{
				result.Add(n.InnerText.Trim() + " (Djinni)");
			}
            return result;
        }
	}
}

