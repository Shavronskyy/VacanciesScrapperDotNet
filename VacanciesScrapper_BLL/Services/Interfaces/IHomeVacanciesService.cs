using VacanciesScrapper_Utils.Enums;
using VacanciesScrapper_BLL.Models;

namespace VacanciesScrapper_BLL.Services.Interfaces
{
    public interface IHomeVacanciesService
    {
        Task<IEnumerable<VacancyDto>> GetAllVacanciesByCategory(Categories? cat, YearsOfExperience? exp);
    }
}