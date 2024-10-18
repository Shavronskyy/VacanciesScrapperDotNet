using Microsoft.AspNetCore.Mvc;
using VacanciesScrapper_BLL.Enums;
using VacanciesScrapper_BLL.Models;
using VacanciesScrapper_BLL.Services.Interfaces;

namespace VacanciesScrapper_WebApi.Controllers
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

