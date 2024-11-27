namespace VacanciesScrapper_BLL.Services.Logging
{
    public interface ILoggerService
    {
        void LogError(object request, string errorMsg);
    }
}