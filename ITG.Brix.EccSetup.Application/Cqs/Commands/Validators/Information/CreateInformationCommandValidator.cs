using FluentValidation;
using FluentValidation.Results;
using ITG.Brix.EccSetup.Application.Cqs.Commands.Definitions;
using ITG.Brix.EccSetup.Application.Enums;
using ITG.Brix.EccSetup.Application.Extensions;
using ITG.Brix.EccSetup.Application.Resources;

namespace ITG.Brix.EccSetup.Application.Cqs.Commands.Validators
{
    public class CreateInformationCommandValidator : AbstractValidator<CreateInformationCommand>
    {
        public CreateInformationCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().ValidationError(ValidationFailures.InformationNameMandatory);
            RuleFor(x => x.Name).Custom((elem, context) =>
            {
                if (!string.IsNullOrWhiteSpace(elem))
                {
                    var result = elem.NotStartsAndNotEndsWithWhiteSpace();
                    if (!result)
                    {
                        context.AddFailure(new ValidationFailure("Name", ValidationFailures.InformationNameCannotStartOrEndWithWhiteSpace) { ErrorCode = ErrorType.ValidationError.ToString() });
                    }
                }
            });
            RuleForEach(x => x.Tags).NotEmpty().ValidationError(ValidationFailures.InformationTagMandatory);
        }
    }
}
