using Microsoft.AspNetCore.Mvc;
using VacanciesScrapper_Utils.Enums;
using VacanciesScrapper_BLL.MediatR.AllVacancies;
using VacanciesScrapper_WebApi.Controllers.Base;

namespace VacanciesScrapper_WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AllVacanciesController : BaseApiController
    {
        [HttpGet(Name = "GetAllVacanciesByCategory")]
        public async Task<IActionResult> GetAllVacanciesByCategory(Categories? cat, YearsOfExperience? exp)
        {
            var query = new GetAllVacanciesByCategoryQuery(cat, exp);
            var vacanciesResult = await Mediator.Send(query);

            return HandleResult(vacanciesResult);
        }
    }
}

