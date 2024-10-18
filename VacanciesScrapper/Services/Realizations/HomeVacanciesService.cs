using System;
using VacanciesScrapper.Enums;
using VacanciesScrapper.Models;
using VacanciesScrapper.Services.Interfaces;

namespace VacanciesScrapper.Services
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

		public async Task<IEnumerable<Vacancy>> GetAllVacaniesByCategory(Categories cat, YearsOfExperience exp)
		{
			var douVacancies = await _douService.GetAllVacanciesByCategory(cat, exp);
			var djinniVacancies = await _djinniService.GetAllVacanciesByCategory(cat, exp);
			
			return douVacancies.Concat(djinniVacancies);
		}
	}
}

