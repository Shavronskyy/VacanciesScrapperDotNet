using Microsoft.AspNetCore.Mvc;
using VacanciesScrapper_Utils.Enums;
using VacanciesScrapper_BLL.MediatR.JobSites.AllVacancies;
using VacanciesScrapper_BLL.Services.Interfaces;
using VacanciesScrapper_WebApi.Controllers.Base;

namespace VacanciesScrapper_WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AllVacanciesController : BaseApiController
    {
        private readonly ILogger<AllVacanciesController> _logger;

        public AllVacanciesController(ILogger<AllVacanciesController> logger, IHomeVacanciesService homeService)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetAllVacanciesByCategory")]
        public async Task<IActionResult> GetAllVacanciesByCategory(Categories? cat, YearsOfExperience? exp)
        {
            return HandleResult(await Mediator.Send(new GetAllVacanciesByCategoryQuery(cat, exp)));
        }
    }
}

