using ITG.Brix.EccSetup.Application.Bases;
using MediatR;

namespace ITG.Brix.EccSetup.Application.Cqs.Commands.Definitions
{
    public class CreateDamageCodeCommand : IRequest<Result>
    {
        public CreateDamageCodeCommand(string code, string name, string damagedQuantityRequired, string source)
        {
            Code = code;
            Name = name;
            DamagedQuantityRequired = damagedQuantityRequired;
            Source = source;
        }

        public string Code { get; private set; }
        public string Name { get; private set; }
        public string DamagedQuantityRequired { get; private set; }
        public string Source { get; private set; }
    }
}
