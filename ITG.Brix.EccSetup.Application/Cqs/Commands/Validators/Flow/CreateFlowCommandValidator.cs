using FluentValidation;
using FluentValidation.Results;
using ITG.Brix.EccSetup.Application.Cqs.Commands.Definitions;
using ITG.Brix.EccSetup.Application.Enums;
using ITG.Brix.EccSetup.Application.Extensions;
using ITG.Brix.EccSetup.Application.Resources;

namespace ITG.Brix.EccSetup.Application.Cqs.Commands.Validators
{
    public class CreateFlowCommandValidator : AbstractValidator<CreateFlowCommand>
    {
        public CreateFlowCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().ValidationError(ValidationFailures.FlowNameMandatory);
            RuleFor(x => x.Name).Custom((elem, context) =>
            {
                if (!string.IsNullOrWhiteSpace(elem))
                {
                    var result = elem.NotStartsAndNotEndsWithWhiteSpace();
                    if (!result)
                    {
                        context.AddFailure(new ValidationFailure("Name", ValidationFailures.FlowNameCannotStartOrEndWithWhiteSpace) { ErrorCode = ErrorType.ValidationError.ToString() });
                    }
                }
            });
        }
    }
}
