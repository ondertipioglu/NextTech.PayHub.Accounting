using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using NextTech.Core.Serilog.Config;

namespace NextTech.Gateway
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
             Host.CreateDefaultBuilder(args)
                 .UseSerilog("api-gateway-nexttech")
                 .ConfigureAppConfiguration((host, config) =>
                 {
                     config.AddJsonFile($"ocelot.{host.HostingEnvironment.EnvironmentName}.json");
                 })
                 .ConfigureWebHostDefaults(webBuilder =>
                 {
                     webBuilder.UseStartup<Startup>();
                 });
    }
}
