using System;
using VacanciesScrapper.Enums;
using VacanciesScrapper.Models;

namespace VacanciesScrapper.Services
{
	public class HomeVacancies
	{
		public HomeVacancies()
		{
			
		}

		public static async Task<IEnumerable<Vacancy>> GetAllVacaniesByCategory(Categories cat, YearsOfExperience exp)
		{
			var douVacancies = await DouVacancies.GetShortVacanciesByCategory(cat, exp);
			var djinniVacancies = await DjinniVacancies.GetAllVacancies(cat, exp);
			
			return douVacancies.Concat(djinniVacancies);
		}
	}
}

