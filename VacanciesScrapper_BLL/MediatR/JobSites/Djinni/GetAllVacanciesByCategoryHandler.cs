using FluentResults;
using MediatR;
using VacanciesScrapper_BLL.Models;
using VacanciesScrapper_BLL.Services.Interfaces;

namespace VacanciesScrapper_BLL.MediatR.JobSites.Djinni;

public class GetAllVacanciesByCategoryHandler : IRequestHandler<GetAllVacanciesByCategoryQuery, Result<IEnumerable<Vacancy>>>
{
    private IDjinniVacanciesService _djinniService;

    public GetAllVacanciesByCategoryHandler(IDjinniVacanciesService djinniService)
    {
        _djinniService = djinniService;
    }

    public async Task<Result<IEnumerable<Vacancy>>> Handle(GetAllVacanciesByCategoryQuery request, CancellationToken cancellationToken)
    {
        return Result.Ok(await _djinniService.GetAllVacanciesByCategory(request.cat, request.exp));
    }
}