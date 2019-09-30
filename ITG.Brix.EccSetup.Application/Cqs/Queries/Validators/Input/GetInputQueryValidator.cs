﻿using FluentValidation;
using ITG.Brix.EccSetup.Application.Bases;
using ITG.Brix.EccSetup.Application.Cqs.Queries.Definitions;
using ITG.Brix.EccSetup.Application.Extensions;
using ITG.Brix.EccSetup.Application.Resources;

namespace ITG.Brix.EccSetup.Application.Cqs.Queries.Validators
{
    public class GetInputQueryValidator : AbstractValidator<GetInputQuery>
    {
        public GetInputQueryValidator()
        {
            RuleFor(x => x.Id).NotEmpty().CustomFault(CustomFaultCode.NotFound, CustomFailures.InputNotFound);
        }
    }
}
