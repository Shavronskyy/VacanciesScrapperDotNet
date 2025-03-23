using System.ComponentModel.DataAnnotations;

namespace VacanciesScrapper_DAL.Entities
{
	public class Company
	{
		[Key]
		public int Id { get; set; }
		[Required]
        public string? Title { get; set; }
		public List<string> Locations { get; set; } = new();
		public string? CompanyImg { get; set; }
		public List<Vacancy> Vacancies { get; set; } = new();
    }
}

