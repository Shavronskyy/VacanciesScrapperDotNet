using Microsoft.EntityFrameworkCore;
using VacanciesScrapper_DAL.Entities;

namespace VacanciesScrapper_DAL.Database
{
    public class VacanciesScrapperDbContext : DbContext
    {
        public VacanciesScrapperDbContext(DbContextOptions<VacanciesScrapperDbContext> options) : base(options)
        {

        }
        
        public DbSet<Company> Companies { get; set; }
        public DbSet<Vacancy> Vacancies { get; set; }
    }
}
