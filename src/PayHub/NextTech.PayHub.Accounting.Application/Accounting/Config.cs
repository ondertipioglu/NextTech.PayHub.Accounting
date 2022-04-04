using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NextTech.Core.MediatR.Queries;


namespace NextTech.PayHub.Accounting.Application
{
    internal static class AccountingConfig
    {
        internal static void AddAccounting(this IServiceCollection services)
        {
            AddQueryHandlers(services);
        }       
        private static void AddQueryHandlers(IServiceCollection services)
        {
            services.AddScoped<IRequestHandler<AuthorizeCardQuery, AuthorizeCardQueryResponse>, AuthorizeCardQueryHandler>();
        }       
    }
}
