using Microsoft.AspNetCore.Mvc;
using VacanciesScrapper_BLL.Enums;
using VacanciesScrapper_BLL.Models;
using VacanciesScrapper_BLL.Services.Interfaces;

namespace VacanciesScrapper_WebApi.Controllers;

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

