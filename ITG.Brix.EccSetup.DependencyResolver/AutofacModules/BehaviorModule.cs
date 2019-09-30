using Autofac;
using ITG.Brix.EccSetup.Application.Behaviors;
using MediatR;

namespace ITG.Brix.EccSetup.DependencyResolver.AutofacModules
{
    public class BehaviorModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(ValidatorBehavior<,>)).As(typeof(IPipelineBehavior<,>));
        }
    }
}
