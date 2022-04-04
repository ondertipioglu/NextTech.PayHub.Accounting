using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NextTech.PayHub.Accounting.Application.Helper;

namespace NextTech.PayHub.Accounting.Application
{
    public static class ApplicationConfig
    {
        public static void AddApplicationModule(this IServiceCollection services, IConfiguration config)
        {
            services.AddScoped<ICardTypeParser, CardTypeParser>();
            services.AddAutoMapper(typeof(AccountDtoMap));
            services.AddAccounting();
        }
    }
}
