using FluentResults;
using MediatR;
using VacanciesScrapper_BLL.MediatR.JobSites.DOU;
using VacanciesScrapper_BLL.Models;
using VacanciesScrapper_BLL.Services.Interfaces;

namespace VacanciesScrapper_BLL.MediatR.JobSites.Dou;

public class GetAllVacanciesByCategoryHandler : IRequestHandler<GetAllVacanciesByCategoryQuery, Result<IEnumerable<Vacancy>>>
{
    private IDouVacanciesService _douService;

    public GetAllVacanciesByCategoryHandler(IDouVacanciesService douService)
    {
        _douService = douService;
    }

    public async Task<Result<IEnumerable<Vacancy>>> Handle(GetAllVacanciesByCategoryQuery request, CancellationToken cancellationToken)
    {
        return Result.Ok(await _douService.GetAllDouVacanciesByCategory(request.cat, request.exp));
    }
}