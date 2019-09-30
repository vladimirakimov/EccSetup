using Autofac;
using ITG.Brix.EccSetup.Application.Cqs.Commands.Definitions;
using MediatR;
using System.Reflection;

namespace ITG.Brix.EccSetup.DependencyResolver.AutofacModules
{
    public class CommandHandlerModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(CreateSourceCommand).GetTypeInfo().Assembly).AsClosedTypesOf(typeof(IRequestHandler<,>));
        }
    }
}
