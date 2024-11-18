using Microsoft.AspNetCore.Mvc;
using VacanciesScrapper_Utils.Enums;
using VacanciesScrapper_BLL.MediatR.JobSites.Djinni;
using VacanciesScrapper_BLL.Services.Interfaces;
using VacanciesScrapper_WebApi.Controllers.Base;
using VacanciesScrapper_BLL.MediatR.JobSites.DOU;

namespace VacanciesScrapper_WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class DjinniController : BaseApiController
    {
        [HttpGet(Name = "GeVacanciesByCategory")]
        public async Task<IActionResult> GetAllVacanciesByCategory(Categories? cat, YearsOfExperience? exp)
        {
            var query = new GetAllDjinniVacanciesByCategoryQuery(cat, exp);
            var vacanciesResult = await Mediator.Send(query);

            return HandleResult(vacanciesResult);
        }
    }
}

