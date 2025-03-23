using Microsoft.EntityFrameworkCore;
using VacanciesScrapper_BLL.Services.Interfaces;
using VacanciesScrapper_BLL.Services.Logging;
using VacanciesScrapper_BLL.Services.Realizations;
using VacanciesScrapper_Utils.Options;
using VacanciesScrapper_Web.Config;
using VacanciesScrapper_DAL.Database;

namespace VacanciesScrapper_WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddHttpClient();

            builder.Configuration.AddUserSecrets<Program>();
            builder.Services.Configure<UrlsOptions>(builder.Configuration.GetSection(UrlsOptions.Key));
            builder.Services.Configure<JobSitesUrlsOptions>(builder.Configuration.GetSection(JobSitesUrlsOptions.Key));

            builder.Services.AddDbContext<VacanciesScrapperDbContext>(options =>
            options.UseSqlServer(builder.Configuration["ConnectionStrings:DefaultConnection"]));

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddLogging();

            var currentAssemblies = AppDomain.CurrentDomain.GetAssemblies();

            builder.Services.AddTransient<IHomeVacanciesService, HomeVacanciesService>();
            builder.Services.AddTransient<IDjinniVacanciesService, DjinniVacanciesService>();
            builder.Services.AddTransient<IDouVacanciesService, DouVacanciesService>();
            builder.Services.AddTransient<IScrapperService, ScrapperService>();
            builder.Services.AddTransient<ILoggerService, LoggerService>();

            builder.Services.AddMediatR(conf =>
            {
                conf.RegisterServicesFromAssemblies(currentAssemblies);
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}

