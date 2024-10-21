using Microsoft.AspNetCore.Mvc;
using VacanciesScrapper_BLL.Enums;
using VacanciesScrapper_Web.Models;

namespace VacanciesScrapper_Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HttpClient _client;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _client = new HttpClient();
            _client.BaseAddress = new Uri(Environment.GetEnvironmentVariable("BaseUrl"));
        }

        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] Categories cat, YearsOfExperience exp )
        {
            var requestUri = Environment.GetEnvironmentVariable("GetAllVacancies");
            HttpResponseMessage response = _client.GetAsync(requestUri).Result;

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<List<VacancyViewModel>>();
                return View(result);
            }

            return View(new List<VacancyViewModel>());
        }
    }
}
