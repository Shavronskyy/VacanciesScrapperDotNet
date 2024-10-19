using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text.Json.Serialization;
using VacanciesScrapper_Web.Models;

namespace VacanciesScrapper_Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HttpClient _client;
        Uri baseUri = new Uri("https://localhost:7032/");

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _client = new HttpClient();
            _client.BaseAddress = baseUri;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            HttpResponseMessage response = _client.GetAsync("api/AllVacancies/GetAllVacanciesByCategory").Result;

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<List<VacancyViewModel>>();
                return View(result);
            }

            return View(new List<VacancyViewModel>());
        }
    }
}
