
using System.Transactions;
using VacanciesScrapper_DAL.Repositories.Interfaces.Companies;
using VacanciesScrapper_DAL.Repositories.Interfaces.Vacancies;

namespace VacanciesScrapper_DAL.Repositories.Interfaces.Base
{
    public interface IRepositoryWrapper
    {
        IVacanciesRepository VacanciesRepository { get; }
        ICompaniesRepository CompaniesRepository { get; }

        public int SaveChanges();

        public Task<int> SaveChangesAsync();

        public TransactionScope BeginTransaction();
    }
}
