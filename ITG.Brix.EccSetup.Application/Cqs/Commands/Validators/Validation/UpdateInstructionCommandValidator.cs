using FluentValidation;
using ITG.Brix.EccSetup.Application.Bases;
using ITG.Brix.EccSetup.Application.Cqs.Commands.Definitions;
using ITG.Brix.EccSetup.Application.Extensions;
using ITG.Brix.EccSetup.Application.Resources;

namespace ITG.Brix.EccSetup.Application.Cqs.Commands.Validators
{
    public class UpdateValidationCommandValidator : AbstractValidator<UpdateValidationCommand>
    {
        public UpdateValidationCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().CustomFault(CustomFaultCode.NotFound, CustomFailures.ValidationNotFound);
            RuleFor(x => x.Name).NotEmpty().ValidationError(ValidationFailures.ValidationNameMandatory);
            RuleForEach(x => x.Tags).NotEmpty().ValidationError(ValidationFailures.ValidationTagMandatory);
        }
    }
}
