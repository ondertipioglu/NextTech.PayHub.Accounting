using FluentValidation;
using Microsoft.Extensions.DependencyInjection;


namespace NextTech.PayHub.Accounting.Domain.Tests
{
    public class ServiceFixture
    {
        public ServiceProvider ServiceProvider { get; private set; }
        public ServiceFixture()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddSingleton<IValidator<AmericanExpress>, AmericanExpressValidator>();
            ServiceProvider = serviceCollection.BuildServiceProvider();
        }
    }
}
