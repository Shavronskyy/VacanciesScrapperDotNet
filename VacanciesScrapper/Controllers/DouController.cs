using Microsoft.AspNetCore.Mvc;
using VacanciesScrapper_BLL.Enums;
using VacanciesScrapper_BLL.MediatR.JobSites.DOU;
using VacanciesScrapper_WebApi.Controllers.Base;

namespace VacanciesScrapper_WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class DouController : BaseApiController
    {

        private readonly ILogger<DouController> _logger;

        public DouController(ILogger<DouController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetAllDouVacanciesByCategory")]
        public async Task<IActionResult> GetAllVacanciesByCategory(Categories? cat, YearsOfExperience? exp)
        {
            return HandleResult(await Mediator.Send(new GetAllVacanciesByCategoryQuery(cat, exp)));
        }
    }
}

