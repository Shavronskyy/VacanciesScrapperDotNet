namespace VacanciesScrapper_Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            
            Environment.SetEnvironmentVariable("BaseUrl", builder.Configuration.GetSection("URLs").GetSection("BaseApiUrl").Value);
            
            builder.Services.AddHttpClient("defaultClient", client =>
            {
                client.BaseAddress = new Uri(Environment.GetEnvironmentVariable("BaseUrl"));
                client.DefaultRequestHeaders.Add("Accept", "application/json");
            });
            
            builder.Services.AddControllersWithViews();

            
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
