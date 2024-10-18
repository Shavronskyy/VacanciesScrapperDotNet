using Microsoft.AspNetCore.Mvc;
using VacanciesScrapper.Enums;
using VacanciesScrapper.Models;
using VacanciesScrapper.Services;
using VacanciesScrapper.Services.Interfaces;

namespace VacanciesScrapper.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class DjinniController : ControllerBase
{
    
    private readonly ILogger<DjinniController> _logger;
    private IDjinniVacanciesService _djinniService;

    public DjinniController(ILogger<DjinniController> logger, IDjinniVacanciesService djinniService)
    {
        _logger = logger;
        _djinniService = djinniService;
    }

    [HttpGet(Name = "GetAllDjinniVacanciesByCategory")]
    public async Task<IEnumerable<Vacancy>> GetAllVacanciesByCategory(Categories? cat, YearsOfExperience? exp)
    {
        return await _djinniService.GetAllVacanciesByCategory(cat, exp);
    }
}

