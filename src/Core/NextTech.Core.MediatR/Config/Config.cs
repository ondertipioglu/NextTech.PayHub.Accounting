using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NextTech.Core.MediatR.Commands;
using NextTech.Core.MediatR.Queries;

namespace NextTech.Core.MediatR.Config
{
    public static class MediatRConfig
    {
        public static IServiceCollection AddMediatRServices(this IServiceCollection services)
        {
            services.AddMediatR()
                .AddScoped<ICommandBus, CommandBus>()
                .AddScoped<IQueryBus, QueryBus>();
            return services;
        }

        private static IServiceCollection AddMediatR(this IServiceCollection services)
        {
            return services
                .AddScoped<IMediator, Mediator>()
                .AddTransient<ServiceFactory>(sp => sp.GetService);
        }
    }
}
