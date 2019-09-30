using FluentValidation;
using ITG.Brix.EccSetup.Application.Cqs.Commands.Definitions;
using ITG.Brix.EccSetup.Application.Extensions;
using ITG.Brix.EccSetup.Application.Resources;

namespace ITG.Brix.EccSetup.Application.Cqs.Commands.Validators.OperationalDepartment
{
    public class CreateOperationalDepartmentCommandValidator : AbstractValidator<CreateOperationalDepartmentCommand>
    {
        public CreateOperationalDepartmentCommandValidator()
        {
            RuleFor(x => x.Code).NotEmpty().ValidationError(ValidationFailures.OperationalDepartmentCodeMandatory);
            RuleFor(x => x.Site).NotEmpty().ValidationError(ValidationFailures.OperationalDepartmentSiteMandatory);
            RuleFor(x => x.Source).NotEmpty().ValidationError(ValidationFailures.OperationalDepartmentSourceMandatory);
        }
    }
}
