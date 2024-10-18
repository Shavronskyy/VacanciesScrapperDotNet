using VacanciesScrapper.Enums;
using VacanciesScrapper.Models;

namespace VacanciesScrapper.Services.Interfaces;

public interface IDouVacanciesService
{
    Task<IEnumerable<Vacancy>> GetAllVacanciesByCategory(Categories? cat, YearsOfExperience? exp);
}