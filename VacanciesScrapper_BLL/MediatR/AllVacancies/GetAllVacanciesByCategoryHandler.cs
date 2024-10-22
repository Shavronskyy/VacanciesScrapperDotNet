using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;
using VacanciesScrapper_BLL.Models;
using VacanciesScrapper_BLL.Services.Interfaces;
using VacanciesScrapper_BLL.Services.Logging;

namespace VacanciesScrapper_BLL.MediatR.JobSites.AllVacancies;

public class GetAllVacanciesByCategoryHandler : IRequestHandler<GetAllVacanciesByCategoryQuery, Result<IEnumerable<Vacancy>>>
{
    private IHomeVacanciesService _homeService;
    private ILoggerService _logger;
    
    public GetAllVacanciesByCategoryHandler(IHomeVacanciesService homeService, ILoggerService logger)
    {
        _homeService = homeService;
        _logger = logger;
    }

    public async Task<Result<IEnumerable<Vacancy>>> Handle(GetAllVacanciesByCategoryQuery request, CancellationToken cancellationToken)
    {
        var vacancies = await _homeService.GetAllVacanciesByCategory(request.cat, request.exp);

        if (!vacancies.Any())
        {
            const string errorMsg = $"Cannot find any vacancies";
            _logger.LogError(request, errorMsg);
            return Result.Fail(new Error(errorMsg));
        }

        return Result.Ok(vacancies);
    }
}