using FluentValidation;
using ITG.Brix.EccSetup.Application.Bases;
using ITG.Brix.EccSetup.Application.Cqs.Queries.Definitions;
using ITG.Brix.EccSetup.Application.Extensions;
using ITG.Brix.EccSetup.Application.Resources;

namespace ITG.Brix.EccSetup.Application.Cqs.Queries.Validators
{
    public class GetBusinessUnitQueryValidator : AbstractValidator<GetBusinessUnitQuery>
    {
        public GetBusinessUnitQueryValidator()
        {
            RuleFor(x => x.Id).NotEmpty().CustomFault(CustomFaultCode.NotFound, CustomFailures.BusinessUnitNotFound);
        }
    }
}
