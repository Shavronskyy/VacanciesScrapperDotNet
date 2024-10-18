using Microsoft.AspNetCore.Mvc;

namespace VacanciesScrapper_WebApi.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class AIAnalyzerController : ControllerBase
{
    private readonly ILogger<DjinniController> _logger;

    public AIAnalyzerController(ILogger<DjinniController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "AnalyzeVacancy")]
    public async Task<string> AnalyzerVacancy()
    {
        return default; //await AIAnalyzerService.AnalyzeVacancyAnswerInPrecents();
    }
}