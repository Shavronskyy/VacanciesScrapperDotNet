using HtmlAgilityPack;
using VacanciesScrapper.Enums;
using VacanciesScrapper.Models;
using VacanciesScrapper.Services.Interfaces;
using VacanciesScrapper.Switches;
using VacanciesScrapper.Utils;

namespace VacanciesScrapper.Services
{
	public class DjinniVacanciesService : IDjinniVacanciesService
	{
		private IScrapperService _scrapperService;
		
		public DjinniVacanciesService(IScrapperService scrapperService)
		{
			_scrapperService = scrapperService;
		}

		public async Task<IEnumerable<Vacancy>> GetAllVacanciesByCategory(Categories? cat, YearsOfExperience? exp)
		{
			var url = "https://djinni.co/jobs/" + CategoriesDjinni.GetCategory(cat) + CategoriesDjinni.GetExperience(exp);

			var document = await _scrapperService.GetHtml(url);
			
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
				var companyImgNode = node.SelectSingleNode(".//img[@class='userpic-image userpic-image_img']");
                var companyImg = companyImgNode is null ? "https://ui-avatars.com/api/?name=" + company : companyImgNode.Attributes["src"].Value; 
                //var date = await GetVacancyCreationDate(link);
                var fullDescription = await GetFullDescription(link);
				var fit = await AnalyzingVacancyByAI(fullDescription);

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
					fitByCv = fit,
                    //CreationDate = date,
                    //Description = fullDescription
                });
            }

            return result;
        }

		private static async Task<int> AnalyzingVacancyByAI(string fullDescription)
		{
			return await AIAnalyzerService.AnalyzeVacancyAnswerInPrecents(fullDescription);
		}

		public async Task<string> GetFullDescription(string url)
		{
			var document = await _scrapperService.GetHtml(url);
			
			var descriptionNodes = document.DocumentNode.SelectSingleNode(".//div[@class='mb-4 job-post__description']");
			
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

