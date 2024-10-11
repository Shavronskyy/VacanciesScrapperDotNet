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
    public class DouController : ControllerBase
    {

        private readonly ILogger<DjinniController> _logger;

        public DouController(ILogger<DjinniController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetAllDouVacanciesByCategory")]
        public async Task<IEnumerable<ShortVacancy>> GetAllVacanciesByCategory(Categories cat, YearsOfExperience exp)
        {
            return await DouVacancies.GetShortVacanciesByCategory(cat, exp);
        }
    }
}

