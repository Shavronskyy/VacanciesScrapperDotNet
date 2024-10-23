namespace VacanciesScrapper_BLL.Services.Interfaces
{
    public interface IAIAnalyzerService
    {
        Task<int> AnalyzeVacancyAnswerInPrecents(string description);
    }
}