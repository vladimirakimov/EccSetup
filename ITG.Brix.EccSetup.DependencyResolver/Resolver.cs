using Autofac;
using Autofac.Extensions.DependencyInjection;
using ITG.Brix.EccSetup.DependencyResolver.AutofacModules;
using Microsoft.Extensions.DependencyInjection;

namespace ITG.Brix.EccSetup.DependencyResolver
{
    public static class Resolver
    {
        public static AutofacServiceProvider BuildServiceProvider(IServiceCollection services, string connectionString)
        {
            services
                .AutoMapper()
                .Persistence(connectionString)
                .Providers()
                .ApiContextProviders()
                .ApiContextServices()
                .Storage();

            var containerBuilder = BuildContainer(services);

            var container = containerBuilder.Build();

            return new AutofacServiceProvider(container);
        }

        private static ContainerBuilder BuildContainer(IServiceCollection services)
        {
            var containerBuilder = new ContainerBuilder();
            containerBuilder.Populate(services);
            containerBuilder
                .RegisterModule(new MediatorModule())
                .RegisterModule(new CommandHandlerModule())
                .RegisterModule(new ValidatorModule())
                .RegisterModule(new BehaviorModule());

            return containerBuilder;
        }
    }
}
