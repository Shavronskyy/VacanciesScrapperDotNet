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
    public class DouController : ControllerBase
    {

        private readonly ILogger<DouController> _logger;
        private IDouVacanciesService _douService;

        public DouController(ILogger<DouController> logger, IDouVacanciesService douService)
        {
            _logger = logger;
            _douService = douService;
        }

        [HttpGet(Name = "GetAllDouVacanciesByCategory")]
        public async Task<IEnumerable<Vacancy>> GetAllVacanciesByCategory(Categories? cat, YearsOfExperience? exp)
        {
            return await _douService.GetAllVacanciesByCategory(cat, exp);
        }
    }
}

