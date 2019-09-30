using FluentValidation;
using ITG.Brix.EccSetup.Application.Cqs.Commands.Definitions;
using ITG.Brix.EccSetup.Application.Extensions;
using ITG.Brix.EccSetup.Application.Resources;

namespace ITG.Brix.EccSetup.Application.Cqs.Commands.Validators
{
    public class CreateTransportTypeCommandValidator : AbstractValidator<CreateTransportTypeCommand>
    {
        public CreateTransportTypeCommandValidator()
        {
            RuleFor(x => x.Code).NotEmpty().ValidationError(ValidationFailures.TransportTypeCodeMandatory);
            RuleFor(x => x.Name).NotEmpty().ValidationError(ValidationFailures.TransportTypeNameMandatory);
            RuleFor(x => x.Source).NotEmpty().ValidationError(ValidationFailures.TransportTypeSourceMandatory);
        }
    }
}
