using HtmlAgilityPack;
using VacanciesScrapper_Utils.Enums;
using VacanciesScrapper_BLL.Services.Interfaces;

namespace VacanciesScrapper_BLL.Services.Realizations
{
    public class ScrapperService : IScrapperService
    {
        private static HttpClient _client;

        public ScrapperService(HttpClient client)
        {
            _client = client;
            _client.DefaultRequestHeaders.UserAgent.ParseAdd(
                "Mozilla/5.0 (X11; Linux x86_64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/88.0.4324.96 Safari/537.36");
        }

        public async Task<HtmlDocument> GetHtml(string url)
        {
            var response = await _client.GetAsync(url);
            response.EnsureSuccessStatusCode(); // Throw if not a success code

            // Get the response content as a string
            var pageContent = await response.Content.ReadAsStringAsync();

            // Load the page content into an HtmlDocument
            var document = new HtmlDocument();
            document.LoadHtml(pageContent);

            return document;
        }
    }
}