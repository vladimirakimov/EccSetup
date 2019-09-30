using FluentValidation;
using FluentValidation.Results;
using ITG.Brix.EccSetup.Application.Cqs.Commands.Definitions;
using ITG.Brix.EccSetup.Application.Enums;
using ITG.Brix.EccSetup.Application.Extensions;
using ITG.Brix.EccSetup.Application.Resources;

namespace ITG.Brix.EccSetup.Application.Cqs.Commands.Validators
{
    public class CreateStorageStatusCommandValidator : AbstractValidator<CreateStorageStatusCommand>
    {
        public CreateStorageStatusCommandValidator()
        {
            RuleFor(x => x.Code).NotEmpty().ValidationError(ValidationFailures.StorageStatusCodeMandatory);
            RuleFor(x => x.Name).NotEmpty().ValidationError(ValidationFailures.StorageStatusNameMandatory);
            RuleFor(x => x.Source).NotEmpty().ValidationError(ValidationFailures.StorageStatusSourceMandatory);
            RuleFor(x => x.Default).NotEmpty().ValidationError(ValidationFailures.StorageStatusDefaultMandatory);
            RuleFor(x => x.Default).Custom((elem, context) =>
            {
                if (!string.IsNullOrWhiteSpace(elem))
                {
                    if (!bool.TryParse(elem, out _))
                    {
                        context.AddFailure(new ValidationFailure("Default", ValidationFailures.StorageStatusDefaultNotBoolean) { ErrorCode = ErrorType.ValidationError.ToString() });
                    }
                }
            });
        }
    }
}
