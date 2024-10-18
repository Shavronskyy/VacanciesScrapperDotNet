namespace VacanciesScrapper.Services.Interfaces;

public interface IAIAnalyzerService
{
    Task<int> AnalyzeVacancyAnswerInPrecents(string description);
}