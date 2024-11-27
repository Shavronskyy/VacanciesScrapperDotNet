using Microsoft.AspNetCore.Mvc;
using VacanciesScrapper_Utils.Enums;
using VacanciesScrapper_BLL.MediatR.JobSites.DOU;
using VacanciesScrapper_WebApi.Controllers.Base;

namespace VacanciesScrapper_WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class DouController : BaseApiController
    {
        [HttpGet(Name = "GetAllDouVacanciesByCategory")]
        public async Task<IActionResult> GetAllVacanciesByCategory(Categories? cat, YearsOfExperience? exp)
        {
            var query = new GetAllDouVacanciesByCategoryQuery(cat, exp);
            var vacanciesResult = await Mediator.Send(query);

            return HandleResult(vacanciesResult);
        }
    }
}

