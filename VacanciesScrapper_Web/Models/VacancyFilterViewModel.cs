using VacanciesScrapper_BLL.Models;
using VacanciesScrapper_Utils.Enums;

namespace VacanciesScrapper_Web.Models
{
    public class VacancyFilterViewModel
    {
        public Categories Category { get; set; }
        public YearsOfExperience ExperienceLevel { get; set; }
        public IEnumerable<VacancyViewModel> Vacancies { get; set; }
    }
}
