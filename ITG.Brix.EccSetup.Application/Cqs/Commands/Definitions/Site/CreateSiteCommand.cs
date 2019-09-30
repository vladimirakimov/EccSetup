using ITG.Brix.EccSetup.Application.Bases;
using MediatR;

namespace ITG.Brix.EccSetup.Application.Cqs.Commands.Definitions
{
    public class CreateSiteCommand : IRequest<Result>
    {
        public CreateSiteCommand(string code, string name, string source)
        {
            Code = code;
            Name = name;
            Source = source;
        }

        public string Code { get; private set; }
        public string Name { get; private set; }
        public string Source { get; private set; }
    }
}
