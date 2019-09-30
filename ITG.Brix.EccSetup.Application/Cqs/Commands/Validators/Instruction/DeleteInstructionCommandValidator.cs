using FluentValidation;
using ITG.Brix.EccSetup.Application.Bases;
using ITG.Brix.EccSetup.Application.Cqs.Commands.Definitions;
using ITG.Brix.EccSetup.Application.Extensions;
using ITG.Brix.EccSetup.Application.Resources;

namespace ITG.Brix.EccSetup.Application.Cqs.Commands.Validators
{
    public class DeleteInstructionCommandValidator : AbstractValidator<DeleteInstructionCommand>
    {
        public DeleteInstructionCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().CustomFault(CustomFaultCode.NotFound, CustomFailures.InstructionNotFound);
        }
    }
}
