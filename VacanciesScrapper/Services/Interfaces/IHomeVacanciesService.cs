using VacanciesScrapper.Enums;
using VacanciesScrapper.Models;

namespace VacanciesScrapper.Services.Interfaces;

public interface IHomeVacanciesService
{
    Task<IEnumerable<Vacancy>> GetAllVacaniesByCategory(Categories cat, YearsOfExperience exp);
}