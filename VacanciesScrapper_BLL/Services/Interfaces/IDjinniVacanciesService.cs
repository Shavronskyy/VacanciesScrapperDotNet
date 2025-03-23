using VacanciesScrapper_Utils.Enums;
using VacanciesScrapper_BLL.Models;

namespace VacanciesScrapper_BLL.Services.Interfaces
{
    public interface IDjinniVacanciesService
    {
        Task<IEnumerable<VacancyDto>> GetAllDjinniVacanciesByCategory(Categories? cat, YearsOfExperience? exp);
    }
}