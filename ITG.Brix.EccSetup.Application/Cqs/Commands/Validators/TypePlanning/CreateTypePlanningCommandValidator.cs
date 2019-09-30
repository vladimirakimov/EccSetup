using FluentValidation;
using ITG.Brix.EccSetup.Application.Cqs.Commands.Definitions;
using ITG.Brix.EccSetup.Application.Extensions;
using ITG.Brix.EccSetup.Application.Resources;

namespace ITG.Brix.EccSetup.Application.Cqs.Commands.Validators
{
    public class CreateTypePlanningCommandValidator : AbstractValidator<CreateTypePlanningCommand>
    {
        public CreateTypePlanningCommandValidator()
        {
            RuleFor(x => x.Code).NotEmpty().ValidationError(ValidationFailures.TypePlanningCodeMandatory);
            RuleFor(x => x.Name).NotEmpty().ValidationError(ValidationFailures.TypePlanningNameMandatory);
            RuleFor(x => x.Source).NotEmpty().ValidationError(ValidationFailures.TypePlanningSourceMandatory);
        }
    }
}
