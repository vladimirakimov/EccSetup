using FluentValidation;
using FluentValidation.Results;
using ITG.Brix.EccSetup.Application.Cqs.Commands.Definitions;
using ITG.Brix.EccSetup.Application.Enums;
using ITG.Brix.EccSetup.Application.Extensions;
using ITG.Brix.EccSetup.Application.Resources;

namespace ITG.Brix.EccSetup.Application.Cqs.Commands.Validators.Location
{
    public class CreateLocationCommandValidator : AbstractValidator<CreateLocationCommand>
    {
        public CreateLocationCommandValidator()
        {
            RuleFor(x => x.Source).NotEmpty().ValidationError(ValidationFailures.LocationSourceMandatory);
            RuleFor(x => x.Site).NotEmpty().ValidationError(ValidationFailures.LocationSiteMandatory);
            RuleFor(x => x.Warehouse).NotEmpty().ValidationError(ValidationFailures.LocationWarehouseMandatory);
            RuleFor(x => x.Gate).NotEmpty().ValidationError(ValidationFailures.LocationGateMandatory);
            RuleFor(x => x.Row).NotEmpty().ValidationError(ValidationFailures.LocationRowMandatory);
            RuleFor(x => x.Position).NotEmpty().ValidationError(ValidationFailures.LocationPositionMandatory);
            RuleFor(x => x.Type).NotEmpty().ValidationError(ValidationFailures.LocationTypeMandatory);
            RuleFor(x => x.IsRack).NotEmpty().ValidationError(ValidationFailures.LocationIsRackMandatory);
            RuleFor(x => x.IsRack).Custom((elem, context) =>
            {
                if (!string.IsNullOrWhiteSpace(elem))
                {
                    if (!bool.TryParse(elem, out _))
                    {
                        context.AddFailure(new ValidationFailure("IsRack", ValidationFailures.LocationIsRackNotBoolean) { ErrorCode = ErrorType.ValidationError.ToString() });
                    }
                }
            });
        }
    }
}
