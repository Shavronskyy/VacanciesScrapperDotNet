using FluentResults;
using MediatR;
using VacanciesScrapper_BLL.Models;
using VacanciesScrapper_BLL.Services.Interfaces;

namespace VacanciesScrapper_BLL.MediatR.JobSites.AllVacancies;

public class GetAllVacanciesByCategoryHandler : IRequestHandler<GetAllVacanciesByCategoryQuery, Result<IEnumerable<Vacancy>>>
{
    private IHomeVacanciesService _homeService;

    public GetAllVacanciesByCategoryHandler(IHomeVacanciesService homeService)
    {
        _homeService = homeService;
    }

    public async Task<Result<IEnumerable<Vacancy>>> Handle(GetAllVacanciesByCategoryQuery request, CancellationToken cancellationToken)
    {
        return Result.Ok(await _homeService.GetAllVacanciesByCategory(request.cat, request.exp));
    }
}