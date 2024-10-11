using System;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;
using VacanciesScrapper.Enums;
using VacanciesScrapper.Services;
using VacanciesScrapper.Models;

namespace VacanciesScrapper.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AllVacanciesController : ControllerBase
    {

        private readonly ILogger<DjinniController> _logger;

        public AllVacanciesController(ILogger<DjinniController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetAllVacanciesByCategory")]
        public async Task<IEnumerable<Vacancy>> GetAllVacanciesByCategory(Categories cat, YearsOfExperience exp)
        {
            return await HomeVacancies.GetAllVacaniesByCategory(cat, exp);
        }
    }
}

