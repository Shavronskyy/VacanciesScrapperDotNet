using FluentResults;
using MediatR;
using VacanciesScrapper_BLL.Enums;
using VacanciesScrapper_BLL.Models;

namespace VacanciesScrapper_BLL.MediatR.JobSites.AllVacancies
{
    public record GetAllVacanciesByCategoryQuery(Categories? cat, YearsOfExperience? exp) : IRequest<Result<IEnumerable<Vacancy>>>;
}