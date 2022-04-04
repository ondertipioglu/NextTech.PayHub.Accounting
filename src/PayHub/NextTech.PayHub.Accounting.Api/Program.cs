using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using NextTech.Core.Serilog.Config;

namespace NextTech.PayHub.Accounting.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
              .UseSerilog("accounting-api")
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
