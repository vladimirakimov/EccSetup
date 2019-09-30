using FluentValidation;
using FluentValidation.Results;
using ITG.Brix.EccSetup.Application.Bases;
using ITG.Brix.EccSetup.Application.Cqs.Commands.Definitions;
using ITG.Brix.EccSetup.Application.Enums;
using ITG.Brix.EccSetup.Application.Extensions;
using ITG.Brix.EccSetup.Application.Resources;

namespace ITG.Brix.EccSetup.Application.Cqs.Commands.Validators
{
    public class UpdateIconCommandValidator : AbstractValidator<UpdateIconCommand>
    {
        public UpdateIconCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().CustomFault(CustomFaultCode.NotFound, CustomFailures.IconNotFound);
            RuleFor(x => x.Name).Custom((elem, context) =>
            {
                if (elem.HasValue && string.IsNullOrWhiteSpace(elem.Value))
                {
                    context.AddFailure(new ValidationFailure("Name", ValidationFailures.IconNameMandatory) { ErrorCode = ErrorType.ValidationError.ToString() });
                }
            });
            RuleFor(x => x.Name).Custom((elem, context) =>
            {
                if (elem.HasValue && !string.IsNullOrWhiteSpace(elem.Value))
                {
                    var result = elem.Value.NotStartsAndNotEndsWithWhiteSpace();
                    if (!result)
                    {
                        context.AddFailure(new ValidationFailure("Name", ValidationFailures.IconNameCannotStartOrEndWithWhiteSpace) { ErrorCode = ErrorType.ValidationError.ToString() });
                    }
                }
            });

            RuleFor(x => x.DataPath).Custom((elem, context) =>
            {
                if (elem.HasValue && string.IsNullOrWhiteSpace(elem.Value))
                {
                    context.AddFailure(new ValidationFailure("DataPath", ValidationFailures.IconDataPathCannotBeEmpty) { ErrorCode = ErrorType.ValidationError.ToString() });
                }
            });
        }
    }
}
