
namespace VacanciesScrapper;

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

