﻿namespace VacanciesScrapper_BLL.Models
{
    public class VacancyDto
    {
        public string Title { get; set; }

        public string Description { get; set; }
    
        public string ShortDescription { get; set; }
    
        public string Company { get; set; }
		
        public string CompanyImg { get; set; }

        public string Salary { get; set; }

        public string Location { get; set; }

        public string CreationDate { get; set; }

        public string Link { get; set; }

        public int FitByCv { get; set; }
    }
}