using Microsoft.AspNetCore.Mvc;
using VacanciesScrapper_Utils.Enums;
using VacanciesScrapper_BLL.MediatR.JobSites.Djinni;
using VacanciesScrapper_BLL.Services.Interfaces;
using VacanciesScrapper_WebApi.Controllers.Base;

namespace VacanciesScrapper_WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class DjinniController : BaseApiController
    {
    
        private readonly ILogger<DjinniController> _logger;
        private IDjinniVacanciesService _djinniService;

        public DjinniController(ILogger<DjinniController> logger, IDjinniVacanciesService djinniService)
    {
        _logger = logger;
        _djinniService = djinniService;
    }

        [HttpGet(Name = "GetAllDjinniVacanciesByCategory")]
        public async Task<IActionResult> GetAllVacanciesByCategory(Categories? cat, YearsOfExperience? exp)
    {
        return HandleResult(await Mediator.Send(new GetAllDjinniVacanciesByCategoryQuery(cat, exp)));
    }
    }
}

