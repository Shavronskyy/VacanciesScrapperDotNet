﻿using VacanciesScrapper_BLL.Enums;
using VacanciesScrapper_BLL.Models;

namespace VacanciesScrapper_BLL.Services.Interfaces
{
    public interface IDjinniVacanciesService
    {
        Task<IEnumerable<Vacancy>> GetAllDjinniVacanciesByCategory(Categories? cat, YearsOfExperience? exp);
    }
}