using AutoMapper;
using ITG.Brix.EccSetup.API.Context.Providers;
using ITG.Brix.EccSetup.API.Context.Providers.Impl;
using ITG.Brix.EccSetup.API.Context.Services;
using ITG.Brix.EccSetup.API.Context.Services.Arrangements;
using ITG.Brix.EccSetup.API.Context.Services.Arrangements.Impl;
using ITG.Brix.EccSetup.API.Context.Services.Impl;
using ITG.Brix.EccSetup.API.Context.Services.Requests;
using ITG.Brix.EccSetup.API.Context.Services.Requests.Impl;
using ITG.Brix.EccSetup.API.Context.Services.Requests.Mappers;
using ITG.Brix.EccSetup.API.Context.Services.Requests.Mappers.Impl;
using ITG.Brix.EccSetup.API.Context.Services.Requests.Validators;
using ITG.Brix.EccSetup.API.Context.Services.Requests.Validators.Components;
using ITG.Brix.EccSetup.API.Context.Services.Requests.Validators.Components.Impl;
using ITG.Brix.EccSetup.API.Context.Services.Requests.Validators.Impl;
using ITG.Brix.EccSetup.API.Context.Services.Requests.Validators.Impl.File;
using ITG.Brix.EccSetup.API.Context.Services.Requests.Validators.Impl.OperatorActivity;
using ITG.Brix.EccSetup.API.Context.Services.Responses;
using ITG.Brix.EccSetup.API.Context.Services.Responses.Impl;
using ITG.Brix.EccSetup.API.Context.Services.Responses.Mappers;
using ITG.Brix.EccSetup.API.Context.Services.Responses.Mappers.Impl;
using ITG.Brix.EccSetup.Application.MappingProfiles;
using ITG.Brix.EccSetup.Application.Services.Json;
using ITG.Brix.EccSetup.Application.Services.Json.Impl;
using ITG.Brix.EccSetup.Domain.Repositories;
using ITG.Brix.EccSetup.Domain.Repositories.Customers;
using ITG.Brix.EccSetup.Domain.Repositories.OperationalDepartments;
using ITG.Brix.EccSetup.Domain.Repositories.OperatorActivities;
using ITG.Brix.EccSetup.Infrastructure.ClassMaps;
using ITG.Brix.EccSetup.Infrastructure.Configurations;
using ITG.Brix.EccSetup.Infrastructure.Configurations.Impl;
using ITG.Brix.EccSetup.Infrastructure.MappingProfiles;
using ITG.Brix.EccSetup.Infrastructure.Providers;
using ITG.Brix.EccSetup.Infrastructure.Providers.Impl;
using ITG.Brix.EccSetup.Infrastructure.Repositories;
using ITG.Brix.EccSetup.Infrastructure.Repositories.Configurations.External;
using ITG.Brix.EccSetup.Infrastructure.Repositories.OperatorActivities;
using ITG.Brix.EccSetup.Infrastructure.Storage;
using ITG.Brix.EccSetup.Infrastructure.Storage.Impl;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Reflection;

namespace ITG.Brix.EccSetup.DependencyResolver
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AutoMapper(this IServiceCollection services)
        {
            var assemblyDomain = Assembly.GetAssembly(typeof(DomainProfile));
            var assemblyInfrastructure = Assembly.GetAssembly(typeof(ClassToDomainProfile));
            var assemblies = new List<Assembly>();

            assemblies.Add(assemblyDomain);
            assemblies.Add(assemblyInfrastructure);
            services.AddAutoMapper(assemblies);

            return services;
        }

        public static IServiceCollection Persistence(this IServiceCollection services, string connectionString)
        {
            ClassMapsRegistrator.RegisterMaps();

            services.AddScoped<IPersistenceConfiguration>(x => new PersistenceConfiguration(connectionString));

            //Configurations
            services.AddScoped<ISourceReadRepository, SourceReadRepository>();
            services.AddScoped<ISourceWriteRepository, SourceWriteRepository>();
            services.AddScoped<IIconReadRepository, IconReadRepository>();
            services.AddScoped<IIconWriteRepository, IconWriteRepository>();
            services.AddScoped<IOperationReadRepository, OperationReadRepository>();
            services.AddScoped<IOperationWriteRepository, OperationWriteRepository>();
            services.AddScoped<IBusinessUnitReadRepository, BusinessUnitReadRepository>();
            services.AddScoped<IBusinessUnitWriteRepository, BusinessUnitWriteRepository>();

            services.AddScoped<IStorageStatusReadRepository, StorageStatusReadRepository>();
            services.AddScoped<IStorageStatusWriteRepository, StorageStatusWriteRepository>();
            services.AddScoped<IDamageCodeReadRepository, DamageCodeReadRepository>();
            services.AddScoped<IDamageCodeWriteRepository, DamageCodeWriteRepository>();
            services.AddScoped<ILocationReadRepository, LocationReadRepository>();
            services.AddScoped<ILocationWriteRepository, LocationWriteRepository>();
            services.AddScoped<IOperationalDepartmentWriteRepository, OperationalDepartmentWriteRepository>();
            services.AddScoped<IOperationalDepartmentReadRepository, OperationalDepartmentReadRepository>();
            services.AddScoped<IOperationalDepartmentWriteRepository, OperationalDepartmentWriteRepository>();
            services.AddScoped<ICustomerWriteRepository, CustomerWriteRepository>();
            services.AddScoped<ICustomerReadRepository, CustomerReadRepository>();
            services.AddScoped<ICustomerWriteRepository, CustomerWriteRepository>();
            services.AddScoped<ISiteReadRepository, SiteReadRepository>();
            services.AddScoped<ISiteWriteRepository, SiteWriteRepository>();
            services.AddScoped<IProductionSiteReadRepository, ProductionSiteReadRepository>();
            services.AddScoped<IProductionSiteWriteRepository, ProductionSiteWriteRepository>();
            services.AddScoped<ITransportTypeReadRepository, TransportTypeReadRepository>();
            services.AddScoped<ITransportTypeWriteRepository, TransportTypeWriteRepository>();
            services.AddScoped<ITypePlanningReadRepository, TypePlanningReadRepository>();
            services.AddScoped<ITypePlanningWriteRepository, TypePlanningWriteRepository>();
            services.AddScoped<ILayoutReadRepository, LayoutReadRepository>();
            services.AddScoped<ILayoutWriteRepository, LayoutWriteRepository>();
            services.AddScoped<IScreenReadRepository, ScreenReadRepository>();
            services.AddScoped<IScreenWriteRepository, ScreenWriteRepository>();
            services.AddScoped<IScreenElementReadRepository, ScreenElementReadRepository>();
            services.AddScoped<IScreenElementWriteRepository, ScreenElementWriteRepository>();
            services.AddScoped<IWorkOrderButtonReadRepository, WorkOrderButtonReadRepository>();
            services.AddScoped<IWorkOrderButtonWriteRepository, WorkOrderButtonWriteRepository>();
            services.AddScoped<IScreenFilterReadRepository, ScreenFilterReadRepository>();
            services.AddScoped<IScreenFilterWriteRepository, ScreenFilterWriteRepository>();
            services.AddScoped<IChecklistReadRepository, ChecklistReadRepository>();
            services.AddScoped<IChecklistWriteRepository, ChecklistWriteRepository>();
            services.AddScoped<IInstructionReadRepository, InstructionReadRepository>();
            services.AddScoped<IInstructionWriteRepository, InstructionWriteRepository>();
            services.AddScoped<IInputReadRepository, InputReadRepository>();
            services.AddScoped<IInputWriteRepository, InputWriteRepository>();
            services.AddScoped<IValidationReadRepository, ValidationReadRepository>();
            services.AddScoped<IValidationWriteRepository, ValidationWriteRepository>();
            services.AddScoped<IFlowReadRepository, FlowReadRepository>();
            services.AddScoped<IFlowWriteRepository, FlowWriteRepository>();

            services.AddScoped<IInformationReadRepository, InformationReadRepository>();
            services.AddScoped<IInformationWriteRepository, InformationWriteRepository>();

            services.AddScoped<IOperatorActivityReadRepository, OperatorActivityReadRepository>();
            services.AddScoped<IOperatorActivityWriteRepository, OperatorActivityWriteRepository>();

            services.AddScoped<IJsonService<object>, JsonService>();

            services.AddScoped<IRemarkReadRepository, RemarkReadRepository>();
            services.AddScoped<IRemarkWriteRepository, RemarkWriteRepository>();
            services.AddScoped<DataContext, DataContext>();

            return services;
        }

        public static IServiceCollection Providers(this IServiceCollection services)
        {
            services.AddScoped<IStorageStatusOdataProvider, StorageStatusOdataProvider>();
            services.AddScoped<IDamageCodeOdataProvider, DamageCodeOdataProvider>();
            services.AddScoped<ITypePlanningOdataProvider, TypePlanningOdataProvider>();
            services.AddScoped<IProductionSiteOdataProvider, ProductionSiteOdataProvider>();
            services.AddScoped<ISiteOdataProvider, SiteOdataProvider>();
            services.AddScoped<ITransportTypeOdataProvider, TransportTypeOdataProvider>();
            services.AddScoped<ILocationOdataProvider, LocationOdataProvider>();
            services.AddScoped<IOperationalDepartmentOdataProvider, OperationalDepartmentOdataProvider>();
            services.AddScoped<ICustomerOdataProvider, CustomerOdataProvider>();
            services.AddScoped<IIdentifierProvider, IdentifierProvider>();
            services.AddScoped<IVersionProvider, VersionProvider>();
            services.AddScoped<IBusinessUnitOdataProvider, BusinessUnitOdataProvider>();
            services.AddScoped<IFlowOdataProvider, FlowOdataProvider>();
            services.AddScoped<IIconOdataProvider, IconOdataProvider>();
            services.AddScoped<ILayoutOdataProvider, LayoutOdataProvider>();
            services.AddScoped<ISourceOdataProvider, SourceOdataProvider>();
            services.AddScoped<IOperationOdataProvider, OperationOdataProvider>();
            services.AddScoped<IFileNameProvider, FileNameProvider>();
            services.AddScoped<IOperatorActivityOdataProvider, OperatorActivityOdataProvider>();

            return services;
        }

        public static IServiceCollection ApiContextProviders(this IServiceCollection services)
        {
            services.AddScoped<IJsonProvider, JsonProvider>();

            return services;
        }

        public static IServiceCollection ApiContextServices(this IServiceCollection services)
        {
            services.AddScoped<IRequestComponentValidator, RequestComponentValidator>();

            services.AddScoped<IRequestValidator, GetBusinessUnitRequestValidator>();
            services.AddScoped<IRequestValidator, ListBusinessUnitRequestValidator>();
            services.AddScoped<IRequestValidator, CreateBusinessUnitRequestValidator>();
            services.AddScoped<IRequestValidator, UpdateBusinessUnitRequestValidator>();
            services.AddScoped<IRequestValidator, DeleteBusinessUnitRequestValidator>();

            services.AddScoped<IRequestValidator, GetFlowRequestValidator>();
            services.AddScoped<IRequestValidator, ListFlowRequestValidator>();
            services.AddScoped<IRequestValidator, CreateFlowRequestValidator>();
            services.AddScoped<IRequestValidator, UpdateFlowRequestValidator>();
            services.AddScoped<IRequestValidator, DeleteFlowRequestValidator>();


            services.AddScoped<IRequestValidator, GetIconRequestValidator>();
            services.AddScoped<IRequestValidator, ListIconRequestValidator>();
            services.AddScoped<IRequestValidator, CreateIconRequestValidator>();
            services.AddScoped<IRequestValidator, UpdateIconRequestValidator>();
            services.AddScoped<IRequestValidator, DeleteIconRequestValidator>();

            services.AddScoped<IRequestValidator, GetLayoutRequestValidator>();
            services.AddScoped<IRequestValidator, ListLayoutRequestValidator>();
            services.AddScoped<IRequestValidator, CreateLayoutRequestValidator>();
            services.AddScoped<IRequestValidator, UpdateLayoutRequestValidator>();
            services.AddScoped<IRequestValidator, DeleteLayoutRequestValidator>();

            services.AddScoped<IRequestValidator, GetOperationRequestValidator>();
            services.AddScoped<IRequestValidator, ListOperationRequestValidator>();
            services.AddScoped<IRequestValidator, CreateOperationRequestValidator>();
            services.AddScoped<IRequestValidator, UpdateOperationRequestValidator>();
            services.AddScoped<IRequestValidator, DeleteOperationRequestValidator>();

            services.AddScoped<IRequestValidator, GetSourceRequestValidator>();
            services.AddScoped<IRequestValidator, ListSourceRequestValidator>();
            services.AddScoped<IRequestValidator, CreateSourceRequestValidator>();
            services.AddScoped<IRequestValidator, UpdateSourceRequestValidator>();
            services.AddScoped<IRequestValidator, DeleteSourceRequestValidator>();

            services.AddScoped<IRequestValidator, CreateOperatorActivityRequestValidator>();
            services.AddScoped<IRequestValidator, ListOperatorActivityRequestValidator>();

            services.AddScoped<IRequestValidator, CreateOperationalDepartmentRequestValidator>();
            services.AddScoped<IRequestValidator, ListOperationalDepartmentRequestValidator>();
            services.AddScoped<IRequestValidator, CreateCustomerRequestValidator>();
            services.AddScoped<IRequestValidator, ListCustomerRequestValidator>();
            services.AddScoped<IRequestValidator, CreateLocationRequestValidator>();
            services.AddScoped<IRequestValidator, ListLocationRequestValidator>();
            services.AddScoped<IRequestValidator, CreateTransportTypeRequestValidator>();
            services.AddScoped<IRequestValidator, ListTransportTypeRequestValidator>();

            services.AddScoped<IRequestValidator, CreateSiteRequestValidator>();
            services.AddScoped<IRequestValidator, ListSiteRequestValidator>();
            services.AddScoped<IRequestValidator, CreateProductionSiteRequestValidator>();
            services.AddScoped<IRequestValidator, ListProductionSiteRequestValidator>();
            services.AddScoped<IRequestValidator, CreateTypePlanningRequestValidator>();
            services.AddScoped<IRequestValidator, ListTypePlanningRequestValidator>();
            services.AddScoped<IRequestValidator, CreateDamageCodeRequestValidator>();
            services.AddScoped<IRequestValidator, ListDamageCodeRequestValidator>();
            services.AddScoped<IRequestValidator, CreateStorageStatusRequestValidator>();
            services.AddScoped<IRequestValidator, ListStorageStatusRequestValidator>();

            services.AddScoped<IRequestValidator, UploadFileRequestValidator>();
            services.AddScoped<IRequestValidator, DownloadFileRequestValidator>();


            services.AddScoped<IApiRequest, ApiRequest>();
            services.AddScoped<ICqsMapper, CqsMapper>();

            services.AddScoped<IErrorMapper, ErrorMapper>();
            services.AddScoped<IHttpStatusCodeMapper, HttpStatusCodeMapper>();
            services.AddScoped<IApiResponse, ApiResponse>();


            services.AddScoped<IValidationArrangement, ValidationArrangement>();
            services.AddScoped<IOperationArrangement, OperationArrangement>();
            services.AddScoped<IApiResult, ApiResult>();

            return services;
        }

        public static IServiceCollection Storage(this IServiceCollection services)
        {
            services.AddScoped<IBlobStorage, BlobStorage>();

            return services;
        }
    }
}
