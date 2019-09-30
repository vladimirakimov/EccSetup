﻿using Autofac;
using FluentValidation;
using ITG.Brix.EccSetup.Application.Cqs.Commands.Validators;
using System.Reflection;

namespace ITG.Brix.EccSetup.DependencyResolver.AutofacModules
{
    public class ValidatorModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterAssemblyTypes(typeof(CreateSourceCommandValidator).GetTypeInfo().Assembly)
                .Where(t => t.IsClosedTypeOf(typeof(IValidator<>)))
                .AsImplementedInterfaces();
        }
    }
}
