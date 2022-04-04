using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NextTech.PayHub.Accounting.Application;
using NextTech.PayHub.Accounting.Infrastructure.EF.Repositories;
using System.Threading.Tasks;

namespace NextTech.PayHub.Accounting.Infrastructure.EF.Config
{
    public static class EFConfig
    {
        public static IServiceCollection AddEFModule(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddEFDataService<AccountingDBContext>(configuration);
        }
        private static IServiceCollection AddEFDataService<TContext>(this IServiceCollection services, IConfiguration configuration) where TContext : DbContext
        {
            var connectionString = configuration.GetConnectionString("AccountingDB");
            services.AddDbContext<TContext>(options =>
            {
                options.UseNpgsql(connectionString).EnableSensitiveDataLogging();
            });
            services.AddScoped<DbContext, TContext>();
            services.AddScoped<ICardRepository, CardRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<DataMigrator>();

            return services;
        }

        public static async Task UseDataInitializer(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetService<AccountingDBContext>();
                dbContext.Database.Migrate();

                var initializer = scope.ServiceProvider.GetService<DataMigrator>();
                await initializer.Seed();
            }
        }
    }
}
