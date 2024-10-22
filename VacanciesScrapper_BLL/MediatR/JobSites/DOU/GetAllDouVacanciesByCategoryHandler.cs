using FluentResults;
using MediatR;
using VacanciesScrapper_BLL.MediatR.JobSites.DOU;
using VacanciesScrapper_BLL.Models;
using VacanciesScrapper_BLL.Services.Interfaces;
using VacanciesScrapper_BLL.Services.Logging;

namespace VacanciesScrapper_BLL.MediatR.JobSites.DOU;

public class GetAllDouVacanciesByCategoryHandler : IRequestHandler<GetAllDouVacanciesByCategoryQuery, Result<IEnumerable<Vacancy>>>
{
    private IDouVacanciesService _douService;
    private ILoggerService _logger;

    public GetAllDouVacanciesByCategoryHandler(IDouVacanciesService douService, ILoggerService logger)
    {
        _douService = douService;
        _logger = logger;
    }

    public async Task<Result<IEnumerable<Vacancy>>> Handle(GetAllDouVacanciesByCategoryQuery request, CancellationToken cancellationToken)
    {
        var vacancies = await _douService.GetAllDouVacanciesByCategory(request.cat, request.exp);

        if (!vacancies.Any())
        {
            const string errorMsg = $"Cannot find any vacancies";
            _logger.LogError(request, errorMsg);
            return Result.Fail(new Error(errorMsg));
        }

        return Result.Ok(vacancies);
    }
}