using FluentResults;
using MediatR;
using VacanciesScrapper_BLL.Models;
using VacanciesScrapper_BLL.Services.Interfaces;
using VacanciesScrapper_BLL.Services.Logging;

namespace VacanciesScrapper_BLL.MediatR.JobSites.Djinni
{
    public class GetAllDjinniVacanciesByCategoryHandler : IRequestHandler<GetAllDjinniVacanciesByCategoryQuery, Result<IEnumerable<Vacancy>>>
    {
        private readonly IDjinniVacanciesService _djinniService;
        private readonly ILoggerService _logger;

        public GetAllDjinniVacanciesByCategoryHandler(IDjinniVacanciesService djinniService, ILoggerService logger)
        {
            _djinniService = djinniService;
            _logger = logger;
        }

        public async Task<Result<IEnumerable<Vacancy>>> Handle(GetAllDjinniVacanciesByCategoryQuery request, CancellationToken cancellationToken)
        {
            var vacancies = await _djinniService.GetAllDjinniVacanciesByCategory(request.cat, request.exp);

            if (!vacancies.Any())
            {
                const string errorMsg = $"Cannot find any vacancies";
                _logger.LogError(request, errorMsg);
                return Result.Ok(Enumerable.Empty<Vacancy>());
            }
        
            return Result.Ok(vacancies);
        }
    }
}