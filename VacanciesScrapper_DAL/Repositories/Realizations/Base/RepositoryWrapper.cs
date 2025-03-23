
using Microsoft.Extensions.DependencyInjection;
using System.Transactions;
using VacanciesScrapper_DAL.Database;
using VacanciesScrapper_DAL.Repositories.Interfaces.Base;
using VacanciesScrapper_DAL.Repositories.Interfaces.Companies;
using VacanciesScrapper_DAL.Repositories.Interfaces.Vacancies;

namespace VacanciesScrapper_DAL.Repositories.Realizations.Base
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private readonly IServiceProvider _serviceProvider;  
        private readonly VacanciesScrapperDbContext _dbContext;

        public RepositoryWrapper(IServiceProvider serviceProvider, VacanciesScrapperDbContext dbContext)
        {
            _serviceProvider = serviceProvider;
            _dbContext = dbContext;
        }

        public IVacanciesRepository VacanciesRepository => GetRepository<IVacanciesRepository>();
        public ICompaniesRepository CompaniesRepository => GetRepository<ICompaniesRepository>();

        public int SaveChanges()
        {
            return _dbContext.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public TransactionScope BeginTransaction()
        {
            return new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        }

        private T GetRepository<T>()
            where T : class
        {
            return _serviceProvider.GetRequiredService<T>();
        }
    }
}
