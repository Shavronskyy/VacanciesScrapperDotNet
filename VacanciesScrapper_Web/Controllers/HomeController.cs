using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using VacanciesScrapper_Utils.Enums;
using VacanciesScrapper_Web.Config;
using VacanciesScrapper_Web.Models;

namespace VacanciesScrapper_Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UrlsOptions _options;
        private readonly HttpClient _client;

        public HomeController(ILogger<HomeController> logger, HttpClient client, IOptions<UrlsOptions> options)
        {
            _options = options.Value;
            _logger = logger;
            _client = client;
            _client.BaseAddress = new Uri(_options.BaseApiUrl);
        }

        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] Categories cat, YearsOfExperience exp )
        {
            var requestUri = _options.GetAllVacanciesUrl;
            var response = _client.GetAsync(requestUri).Result;

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<List<VacancyViewModel>>();
                return View(result);
            }
            
            _logger.LogInformation("Vacancies not found");
            return View(new List<VacancyViewModel>());
        }
    }
}
