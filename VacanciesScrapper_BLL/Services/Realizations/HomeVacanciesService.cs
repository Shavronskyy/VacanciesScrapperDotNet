using VacanciesScrapper_BLL.Enums;
using VacanciesScrapper_BLL.Models;
using VacanciesScrapper_BLL.Services.Interfaces;

namespace VacanciesScrapper_BLL.Services.Realizations
{
	public class HomeVacanciesService : IHomeVacanciesService
	{
		public IDouVacanciesService _douService;
		public IDjinniVacanciesService _djinniService;
		
		public HomeVacanciesService(IDouVacanciesService douService, IDjinniVacanciesService djinniService)
		{
			_douService = douService;
			_djinniService = djinniService;
		}

		public async Task<IEnumerable<Vacancy>> GetAllVacanciesByCategory(Categories? cat, YearsOfExperience? exp)
		{
			var douVacancies = await _douService.GetAllDouVacanciesByCategory(cat, exp);
			var djinniVacancies = await _djinniService.GetAllDjinniVacanciesByCategory(cat, exp);
			
			return douVacancies.Concat(djinniVacancies);
		}
	}
}

