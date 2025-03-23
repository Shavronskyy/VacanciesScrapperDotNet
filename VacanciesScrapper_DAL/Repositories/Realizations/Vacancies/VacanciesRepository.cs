using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VacanciesScrapper_DAL.Database;
using VacanciesScrapper_DAL.Entities;
using VacanciesScrapper_DAL.Repositories.Interfaces.Vacancies;
using VacanciesScrapper_DAL.Repositories.Realizations.Base;

namespace VacanciesScrapper_DAL.Repositories.Realizations.Vacancies
{
    public class VacanciesRepository : RepositoryBase<Vacancy>, IVacanciesRepository
    {
        protected VacanciesRepository(VacanciesScrapperDbContext dbContext) : base(dbContext)
        {
        }
    }
}
