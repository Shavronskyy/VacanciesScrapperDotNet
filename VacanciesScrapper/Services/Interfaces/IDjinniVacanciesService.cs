using VacanciesScrapper.Enums;
using VacanciesScrapper.Models;

namespace VacanciesScrapper.Services.Interfaces;

public interface IDjinniVacanciesService
{
    Task<IEnumerable<Vacancy>> GetAllVacanciesByCategory(Categories? cat, YearsOfExperience? exp);
}