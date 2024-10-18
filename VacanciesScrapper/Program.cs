﻿using System.Reflection;
using MediatR;
using VacanciesScrapper_BLL.MediatR.JobSites.Djinni;
using VacanciesScrapper_BLL.Services.Interfaces;
using VacanciesScrapper_BLL.Services.Realizations;

namespace VacanciesScrapper_WebApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        Environment.SetEnvironmentVariable("GROQ_APIKEY", builder.Configuration.GetSection("Groq").GetSection("APIKEY").Value);
        Environment.SetEnvironmentVariable("GROQ_MODEL", builder.Configuration.GetSection("Groq").GetSection("Model").Value);
        
        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var currentAssemblies = AppDomain.CurrentDomain.GetAssemblies();

        // TODO: refactor that to one command

        builder.Services.AddMediatR(conf =>
        {
            conf.RegisterServicesFromAssembly(typeof(GetAllVacanciesByCategoryQuery).Assembly);
            conf.RegisterServicesFromAssembly(typeof(GetAllVacanciesByCategoryQuery).Assembly);
        }); 

        builder.Services.AddTransient<IHomeVacanciesService, HomeVacanciesService>();
        builder.Services.AddTransient<IDjinniVacanciesService, DjinniVacanciesService>();
        builder.Services.AddTransient<IDouVacanciesService, DouVacanciesService>();
        builder.Services.AddTransient<IAIAnalyzerService, AIAnalyzerService>();
        builder.Services.AddTransient<IScrapperService, ScrapperService>();

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

