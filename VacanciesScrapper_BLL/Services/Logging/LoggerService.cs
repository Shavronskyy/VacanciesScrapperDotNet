
using Microsoft.Extensions.Logging;

namespace VacanciesScrapper_BLL.Services.Logging
{
    public class LoggerService : ILoggerService
    {
        private readonly ILogger _logger;

        public void LogError(object request, string errorMsg)
        {
            var requestType = request.GetType().ToString();
            var requestClass = requestType.Substring(requestType.LastIndexOf('.') + 1);
            _logger.LogError("{RequestClass} handled with the error: {ErrorMsg}", requestClass, errorMsg);
        }
    }
}