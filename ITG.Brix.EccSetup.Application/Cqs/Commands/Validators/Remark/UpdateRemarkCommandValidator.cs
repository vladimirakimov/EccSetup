using FluentValidation;
using FluentValidation.Results;
using ITG.Brix.EccSetup.Application.Bases;
using ITG.Brix.EccSetup.Application.Cqs.Commands.Definitions;
using ITG.Brix.EccSetup.Application.Enums;
using ITG.Brix.EccSetup.Application.Extensions;
using ITG.Brix.EccSetup.Application.Resources;

namespace ITG.Brix.EccSetup.Application.Cqs.Commands.Validators
{
    public class UpdateRemarkCommandValidator : AbstractValidator<UpdateRemarkCommand>
    {
        public UpdateRemarkCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().CustomFault(CustomFaultCode.NotFound, CustomFailures.RemarkNotFound);
            RuleFor(x => x.Name).NotEmpty().ValidationError(ValidationFailures.RemarkNameMandatory);
            RuleFor(x => x.Name).Custom((elem, context) =>
            {
                if (!string.IsNullOrWhiteSpace(elem))
                {
                    var result = elem.NotStartsAndNotEndsWithWhiteSpace();
                    if (!result)
                    {
                        context.AddFailure(new ValidationFailure("Name", ValidationFailures.RemarkNameCannotStartOrEndWithWhiteSpace) { ErrorCode = ErrorType.ValidationError.ToString() });
                    }
                }
            });
        }
    }
}
