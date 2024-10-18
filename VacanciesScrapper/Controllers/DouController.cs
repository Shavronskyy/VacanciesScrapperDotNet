using Microsoft.AspNetCore.Mvc;
using VacanciesScrapper_BLL.Enums;
using VacanciesScrapper_BLL.Models;
using VacanciesScrapper_BLL.Services.Interfaces;

namespace VacanciesScrapper_WebApi.Controllers
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

