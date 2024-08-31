using Microsoft.AspNetCore.Mvc;
using VacanciesScrapper.Enums;
using VacanciesScrapper.Models;
using VacanciesScrapper.Services;

namespace VacanciesScrapper.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class DjinniController : ControllerBase
{
    
    private readonly ILogger<DjinniController> _logger;

    public DjinniController(ILogger<DjinniController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetAllDjinniVacanciesByCategory")]
    public async Task<IEnumerable<Vacancy>> GetAllVacanciesByCategory(Categories cat, YearsOfExperience exp)
    {
        return await DjinniVacancies.GetAllVacancies(cat, exp);
    }
}

