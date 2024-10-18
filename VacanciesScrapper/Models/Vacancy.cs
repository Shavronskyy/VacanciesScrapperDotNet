﻿namespace VacanciesScrapper.Models;

public class Vacancy
{
    public string Title { get; set; }

    public string Description { get; set; }
    
    public string shortDescription { get; set; }
    
    public string Company { get; set; }
		
    public string CompanyImg { get; set; }

    public string Salary { get; set; }

    public string Location { get; set; }

    public string CreationDate { get; set; }

    public string Link { get; set; }

    public int fitByCv { get; set; }
}