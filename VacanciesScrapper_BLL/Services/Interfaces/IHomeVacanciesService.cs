using VacanciesScrapper_BLL.Enums;
using VacanciesScrapper_BLL.Models;

namespace VacanciesScrapper_BLL.Services.Interfaces;

public interface IHomeVacanciesService
{
    Task<IEnumerable<Vacancy>> GetAllVacaniesByCategory(Categories cat, YearsOfExperience exp);
}