using ITG.Brix.EccSetup.API.Constants;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.ApplicationInsights.ServiceFabric;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.ServiceFabric.Services.Communication.AspNetCore;
using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.ServiceFabric.Services.Runtime;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Fabric;
using System.IO;

namespace ITG.Brix.EccSetup.API
{
    /// <summary>
    /// The FabricRuntime creates an instance of this class for each service type instance. 
    /// </summary>
    [ExcludeFromCodeCoverage]
    internal sealed class ApiStatelessService : StatelessService
    {
        public ApiStatelessService(StatelessServiceContext context)
            : base(context)
        { }

        /// <summary>
        /// Optional override to create listeners (like tcp, http) for this service instance.
        /// </summary>
        /// <returns>The collection of listeners.</returns>
        protected override IEnumerable<ServiceInstanceListener> CreateServiceInstanceListeners()
        {
            return new ServiceInstanceListener[]
            {
                new ServiceInstanceListener(serviceContext =>
                    new KestrelCommunicationListener(serviceContext, "ApiServiceEndpoint", (url, listener) =>
                    {
                        ServiceEventSource.Current.ServiceMessage(serviceContext, $"Starting Kestrel on {url}");

                        return new WebHostBuilder()
                                    .UseKestrel()
                                    .ConfigureServices(
                                        services => services
                                            .AddSingleton<StatelessServiceContext>(serviceContext)
                                            .AddSingleton<ITelemetryInitializer>((serviceProvider) => new FabricTelemetryInitializer()))
                                    .UseContentRoot(Directory.GetCurrentDirectory())
                                    .UseStartup<Startup>()
                                    .UseApplicationInsights()
                                    .UseEnvironment(GetEnvironment())
                                    .UseServiceFabricIntegration(listener, ServiceFabricIntegrationOptions.None)
                                    .UseUrls(url)
                                    .UseConfiguration(GetConfig())
                                    .Build();
                    }))
            };
        }

        private string GetEnvironment()
        {
            var result = FabricRuntime.GetActivationContext()?
                                .GetConfigurationPackageObject(Consts.Config.ConfigurationPackageObject)?
                                .Settings.Sections[Consts.Config.Environment.Section]?
                                .Parameters[Consts.Config.Environment.Param]?.Value;
            return result;
        }

        private IConfiguration GetConfig()
        {

            var connectionString = FabricRuntime.GetActivationContext()?
                .GetConfigurationPackageObject(Consts.Config.ConfigurationPackageObject)?
                .Settings.Sections[Consts.Config.Database.Section]?
                .Parameters[Consts.Config.Database.ConnectionString]?.Value;

            var storageConnectionString = FabricRuntime.GetActivationContext()?
                .GetConfigurationPackageObject(Consts.Config.ConfigurationPackageObject)?
                .Settings.Sections[Consts.Config.Storage.Section]?
                .Parameters[Consts.Config.Storage.ConnectionString]?.Value;

            Environment.SetEnvironmentVariable(Consts.Configuration.Id + Consts.Configuration.ConnectionString, connectionString, EnvironmentVariableTarget.Process);
            Environment.SetEnvironmentVariable(Consts.Configuration.Id + Consts.Configuration.StorageConnectionString, storageConnectionString, EnvironmentVariableTarget.Process);
            var result = new ConfigurationBuilder().AddEnvironmentVariables(Consts.Configuration.Id).Build();
            return result;
        }
    }
}
