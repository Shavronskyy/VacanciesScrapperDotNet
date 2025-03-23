using FluentResults;
using MediatR;
using VacanciesScrapper_BLL.Models;
using VacanciesScrapper_BLL.Services.Interfaces;
using VacanciesScrapper_BLL.Services.Logging;

namespace VacanciesScrapper_BLL.MediatR.AllVacancies
{
    public class GetAllVacanciesByCategoryHandler : IRequestHandler<GetAllVacanciesByCategoryQuery, Result<IEnumerable<VacancyDto>>>
    {
        private readonly IHomeVacanciesService _homeService;
        private readonly ILoggerService _logger;

        public GetAllVacanciesByCategoryHandler(IHomeVacanciesService homeService, ILoggerService logger)
        {
            _homeService = homeService;
            _logger = logger;
        }

        public async Task<Result<IEnumerable<VacancyDto>>> Handle(GetAllVacanciesByCategoryQuery request, CancellationToken cancellationToken)
        {
            var vacancies = await _homeService.GetAllVacanciesByCategory(request.cat, request.exp);

            if (!vacancies.Any())
            {
                const string errorMsg = "Cannot find any vacancies";
                _logger.LogError(request, errorMsg);
                return Result.Ok(Enumerable.Empty<VacancyDto>());
            }

            return Result.Ok(vacancies);
        }
    }
}