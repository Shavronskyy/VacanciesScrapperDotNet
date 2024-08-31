using System;
namespace VacanciesScrapper.Models
{
	public class Vacancy
	{
		public string Title { get; set; }

		public string Description { get; set; }

		public string ShortDescription { get; set; }

		public string Company { get; set; }

		public int? Salary { get; set; }

		public string Location { get; set; }

		public string CreationDate { get; set; }
	}
}

