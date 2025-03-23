using VacanciesScrapper_Utils.Enums;
using VacanciesScrapper_BLL.Models;

namespace VacanciesScrapper_BLL.Services.Interfaces
{
    public interface IDouVacanciesService
    {
        Task<IEnumerable<VacancyDto>> GetAllDouVacanciesByCategory(Categories? cat, YearsOfExperience? exp);
    }
}