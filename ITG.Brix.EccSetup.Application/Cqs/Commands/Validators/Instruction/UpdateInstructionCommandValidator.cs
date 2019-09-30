using FluentValidation;
using FluentValidation.Results;
using ITG.Brix.EccSetup.Application.Bases;
using ITG.Brix.EccSetup.Application.Cqs.Commands.Definitions;
using ITG.Brix.EccSetup.Application.Enums;
using ITG.Brix.EccSetup.Application.Extensions;
using ITG.Brix.EccSetup.Application.Resources;
namespace ITG.Brix.EccSetup.Application.Cqs.Commands.Validators
{
    public class UpdateInstructionCommandValidator : AbstractValidator<UpdateInstructionCommand>
    {
        public UpdateInstructionCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().CustomFault(CustomFaultCode.NotFound, CustomFailures.InstructionNotFound);
            RuleFor(x => x.Name).NotEmpty().ValidationError(ValidationFailures.InstructionNameMandatory);
            RuleFor(x => x.Name).Custom((elem, context) =>
            {
                if (!string.IsNullOrWhiteSpace(elem))
                {
                    var result = elem.NotStartsAndNotEndsWithWhiteSpace();
                    if (!result)
                    {
                        context.AddFailure(new ValidationFailure("Name", ValidationFailures.InstructionNameCannotStartOrEndWithWhiteSpace) { ErrorCode = ErrorType.ValidationError.ToString() });
                    }
                }
            });
            RuleFor(x => x.Content).NotEmpty().ValidationError(ValidationFailures.InstructionContentMandatory);
            RuleForEach(x => x.Tags).NotEmpty().ValidationError(ValidationFailures.InstructionTagMandatory);
        }
    }
}
