using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
 
namespace NextTech.PayHub.Accounting.Infrastructure.EF.Context
{

    public class AccountingDBContextFactory : IDesignTimeDbContextFactory<AccountingDBContext>
    {
        public AccountingDBContext CreateDbContext(params string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AccountingDBContext>();

            if (optionsBuilder.IsConfigured)
                return new AccountingDBContext(optionsBuilder.Options);

            var environmentName = Environment.GetEnvironmentVariable("EnvironmentName") ?? "Development";

            var connectionString =
                new ConfigurationBuilder()
                    .SetBasePath(AppContext.BaseDirectory)
                    .AddJsonFile("appsettings.json")
                    .AddJsonFile($"appsettings.{environmentName}.json", optional: true, reloadOnChange: false)
                    .AddEnvironmentVariables()
                    .Build()
                    .GetConnectionString("AccountingDB");

            optionsBuilder.UseNpgsql(connectionString);

            return new AccountingDBContext(optionsBuilder.Options);
        }

        public static AccountingDBContext Create() => new AccountingDBContextFactory().CreateDbContext();
    }
}
