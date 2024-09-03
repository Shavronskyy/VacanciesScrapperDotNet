using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using VacanciesScrapper_Web.Models;

namespace VacanciesScrapper_Web.Controllers
{
    public class HomeController : Controller
    {
        Uri baseAdress = new Uri("https://localhost:44391/api");
        private readonly HttpClient _client;

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _client = new HttpClient();
            _client.BaseAddress = baseAdress;
        }

        [HttpGet]
        public IActionResult Index()
        {

            return View();
        }
    }
}
