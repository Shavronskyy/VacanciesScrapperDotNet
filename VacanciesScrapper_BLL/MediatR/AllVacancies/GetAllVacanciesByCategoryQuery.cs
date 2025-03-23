using FluentResults;
using MediatR;
using VacanciesScrapper_Utils.Enums;
using VacanciesScrapper_BLL.Models;

namespace VacanciesScrapper_BLL.MediatR.AllVacancies
{
    public record GetAllVacanciesByCategoryQuery(Categories? cat, YearsOfExperience? exp) : IRequest<Result<IEnumerable<VacancyDto>>>;
}