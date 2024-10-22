using VacanciesScrapper_BLL.Enums;
using VacanciesScrapper_BLL.Models;

namespace VacanciesScrapper_BLL.Services.Interfaces
{
    public interface IDouVacanciesService
    {
        Task<IEnumerable<Vacancy>> GetAllDouVacanciesByCategory(Categories? cat, YearsOfExperience? exp);
    }
}