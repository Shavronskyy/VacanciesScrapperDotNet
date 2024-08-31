using System;
using VacanciesScrapper.Enums;

namespace VacanciesScrapper.Services
{
	public class HomeVacancies
	{
		public HomeVacancies()
		{
			
		}

		public async Task<IEnumerable<string>> GetAllVacaniesByCategory(Categories cat, YearsOfExperience exp)
		{
			//var douVacancies = await DouVacancies.GetAllVacanciesByCategory(cat, exp);
			//var djinniVacancies = await DjinniVacancies.GetAllVacancies(cat, exp);

			return default;
		}
	}
}

