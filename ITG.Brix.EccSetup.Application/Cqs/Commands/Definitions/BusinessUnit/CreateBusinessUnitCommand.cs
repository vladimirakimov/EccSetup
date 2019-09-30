using ITG.Brix.EccSetup.Application.Bases;
using MediatR;

namespace ITG.Brix.EccSetup.Application.Cqs.Commands.Definitions
{
    public class CreateBusinessUnitCommand : IRequest<Result>
    {
        public string Name { get; private set; }

        public CreateBusinessUnitCommand(string name)
        {
            Name = name;
        }
    }
}
