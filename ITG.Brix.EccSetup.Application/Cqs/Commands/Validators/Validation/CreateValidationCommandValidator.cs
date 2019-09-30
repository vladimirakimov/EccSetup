using FluentValidation;
using ITG.Brix.EccSetup.Application.Cqs.Commands.Definitions;
using ITG.Brix.EccSetup.Application.Extensions;
using ITG.Brix.EccSetup.Application.Resources;

namespace ITG.Brix.EccSetup.Application.Cqs.Commands.Validators
{
    public class CreateValidationCommandValidator : AbstractValidator<CreateValidationCommand>
    {
        public CreateValidationCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().ValidationError(ValidationFailures.ValidationNameMandatory);
            RuleForEach(x => x.Tags).NotEmpty().ValidationError(ValidationFailures.ValidationTagMandatory);
        }
    }
}
