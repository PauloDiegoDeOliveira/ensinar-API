using Microsoft.EntityFrameworkCore;
using MinhaPrimeiraAPI.Context;

namespace MinhaPrimeiraAPI.Configuration
{
    public static class DataBaseConfig
    {
        public static void AddDatabaseConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            // Usando um banco de dados na memória
            // services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase(databaseName: "DbFake"));

            // SQL SERVER
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("Connection")));
        }

        public static void UseDatabaseConfiguration(this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
            using var context = serviceScope.ServiceProvider.GetService<AppDbContext>();
        }
    }
}