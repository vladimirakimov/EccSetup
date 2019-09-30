using FluentValidation.AspNetCore;
using ITG.Brix.EccSetup.API.Constants;
using ITG.Brix.EccSetup.DependencyResolver;
using ITG.Brix.EccSetup.Infrastructure.Storage.Config;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace ITG.Brix.EccSetup.API
{
    [ExcludeFromCodeCoverage]
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(config =>
                     {
                         // Add XML Content Negotiation
                         config.RespectBrowserAcceptHeader = true;
                         //config.InputFormatters.Add(new XmlSerializerInputFormatter());
                         //config.OutputFormatters.Add(new XmlSerializerOutputFormatter());
                     })
                    .AddJsonOptions(options =>
                     {
                         options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                     })
                    .AddFluentValidation();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "EccSetup API", Version = "v1" });
            });

            services.AddCors();

            services.Configure<StorageConfiguration>(Configuration);

            var connectionString = Configuration[Consts.Configuration.ConnectionString];

            var result = Resolver.BuildServiceProvider(services, connectionString);
            return result;
        }

        public void ConfigureDevelopment(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            Configure(app, env);
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseCors(builder => builder
                                   .AllowAnyOrigin()
                                   .AllowAnyMethod()
                                   .AllowAnyHeader()
                                   .AllowCredentials());

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "EccSetup API v1");
            });

            app.UseMvc();
            app.Run(context =>
            {
                context.Response.Redirect("swagger");
                return Task.CompletedTask;
            });
        }
    }
}
