using System;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;
using VacanciesScrapper.Enums;
using VacanciesScrapper.Services;
using VacanciesScrapper.Models;
using VacanciesScrapper.Services.Interfaces;

namespace VacanciesScrapper.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AllVacanciesController : ControllerBase
    {

        private readonly ILogger<AllVacanciesController> _logger;
        private IHomeVacanciesService _homeService;

        public AllVacanciesController(ILogger<AllVacanciesController> logger, IHomeVacanciesService homeService)
        {
            _logger = logger;
            _homeService = homeService;
        }

        [HttpGet(Name = "GetAllVacanciesByCategory")]
        public async Task<IEnumerable<Vacancy>> GetAllVacanciesByCategory(Categories cat, YearsOfExperience exp)
        {
            return await _homeService.GetAllVacaniesByCategory(cat, exp);
        }
    }
}

