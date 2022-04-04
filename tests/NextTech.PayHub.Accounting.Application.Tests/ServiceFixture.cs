using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace NextTech.PayHub.Accounting.Application.Tests
{
    public class ServiceFixture
    {
        public ServiceProvider ServiceProvider { get; private set; }
        public ServiceFixture()
        {
            ServiceProvider = Bootstrapper();
        }
        private ServiceProvider Bootstrapper()
        {

            List<Assembly> assemblies = new List<Assembly>();

            foreach (string assemblyPath in Directory.GetFiles(System.AppDomain.CurrentDomain.BaseDirectory, "*.dll", SearchOption.AllDirectories))
            {
                var assembly = System.Runtime.Loader.AssemblyLoadContext.Default.LoadFromAssemblyPath(assemblyPath);
                assemblies.Add(assembly);
            }
            var serviceProvider = new ServiceCollection();

            serviceProvider.AddScoped<IMediator, Mediator>()
                 //.AddScoped<IProductRepository, ProductRepository>()
                 .AddTransient<ServiceFactory>(sp => sp.GetService);


            var provider = serviceProvider.Scan(scan => scan
                .FromAssemblies(assemblies)
                .AddClasses()
                .AsImplementedInterfaces()
                .WithScopedLifetime())
                .BuildServiceProvider();
            return provider;
        }
    }
}
