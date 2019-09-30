using FluentValidation;
using ITG.Brix.EccSetup.Application.Cqs.Commands.Definitions;
using ITG.Brix.EccSetup.Application.Extensions;
using ITG.Brix.EccSetup.Application.Resources;

namespace ITG.Brix.EccSetup.Application.Cqs.Commands.Validators
{
    public class CreateProductionSiteCommandValidator : AbstractValidator<CreateProductionSiteCommand>
    {
        public CreateProductionSiteCommandValidator()
        {
            RuleFor(x => x.Code).NotEmpty().ValidationError(ValidationFailures.ProductionSiteCodeMandatory);
            RuleFor(x => x.Name).NotEmpty().ValidationError(ValidationFailures.ProductionSiteNameMandatory);
            RuleFor(x => x.Source).NotEmpty().ValidationError(ValidationFailures.ProductionSiteSourceMandatory);
        }
    }
}
