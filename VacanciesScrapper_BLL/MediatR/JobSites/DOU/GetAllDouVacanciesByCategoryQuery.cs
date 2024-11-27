using System;
using FluentResults;
using MediatR;
using VacanciesScrapper_Utils.Enums;
using VacanciesScrapper_BLL.Models;

namespace VacanciesScrapper_BLL.MediatR.JobSites.DOU
{
    public record GetAllDouVacanciesByCategoryQuery(Categories? cat, YearsOfExperience? exp) : IRequest<Result<IEnumerable<Vacancy>>>;
}