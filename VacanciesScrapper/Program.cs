﻿using VacanciesScrapper_BLL.Services.Interfaces;
using VacanciesScrapper_BLL.Services.Logging;
using VacanciesScrapper_BLL.Services.Realizations;
using VacanciesScrapper_BLL.Options;

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
        builder.Services.Configure<AIOptions>(builder.Configuration.GetSection(AIOptions.Key));
        
        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddLogging();

        var currentAssemblies = AppDomain.CurrentDomain.GetAssemblies();
        
        builder.Services.AddTransient<IHomeVacanciesService, HomeVacanciesService>();
        builder.Services.AddTransient<IDjinniVacanciesService, DjinniVacanciesService>();
        builder.Services.AddTransient<IDouVacanciesService, DouVacanciesService>();
        builder.Services.AddTransient<IAIAnalyzerService, AIAnalyzerService>();
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

