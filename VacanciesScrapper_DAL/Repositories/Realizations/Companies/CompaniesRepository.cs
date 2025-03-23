using VacanciesScrapper_DAL.Repositories.Interfaces.Companies;
using VacanciesScrapper_DAL.Repositories.Realizations.Base;
using VacanciesScrapper_DAL.Entities;
using VacanciesScrapper_DAL.Database;

namespace VacanciesScrapper_DAL.Repositories.Realizations.Companies
{
    public class CompaniesRepository : RepositoryBase<Company>, ICompaniesRepository
    {
        protected CompaniesRepository(VacanciesScrapperDbContext dbContext) : base(dbContext)
        {
        }
    }
}
