using FluentValidation;
using FluentValidation.Results;
using ITG.Brix.EccSetup.Application.Cqs.Commands.Definitions;
using ITG.Brix.EccSetup.Application.Enums;
using ITG.Brix.EccSetup.Application.Extensions;
using ITG.Brix.EccSetup.Application.Resources;

namespace ITG.Brix.EccSetup.Application.Cqs.Commands.Validators
{
    public class CreateDamageCodeCommandValidator : AbstractValidator<CreateDamageCodeCommand>
    {
        public CreateDamageCodeCommandValidator()
        {
            RuleFor(x => x.Code).NotEmpty().ValidationError(ValidationFailures.DamageCodeCodeMandatory);
            RuleFor(x => x.Source).NotEmpty().ValidationError(ValidationFailures.DamageCodeSourceMandatory);
            RuleFor(x => x.Name).NotEmpty().ValidationError(ValidationFailures.DamageCodeNameMandatory);
            RuleFor(x => x.DamagedQuantityRequired).NotEmpty().ValidationError(ValidationFailures.DamageCodeDamagedQuantityRequiredMandatory);
            RuleFor(x => x.DamagedQuantityRequired).Custom((elem, context) =>
            {
                if (!string.IsNullOrWhiteSpace(elem))
                {
                    if (!bool.TryParse(elem, out _))
                    {
                        context.AddFailure(new ValidationFailure("DamagedQuantityNotBoolean", ValidationFailures.DamageCodeIsNotBoolean) { ErrorCode = ErrorType.ValidationError.ToString() });
                    }
                }
            });
        }
    }
}
