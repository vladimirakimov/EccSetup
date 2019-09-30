using ITG.Brix.EccSetup.Application.Bases;
using MediatR;

namespace ITG.Brix.EccSetup.Application.Cqs.Commands.Definitions
{
    public class CreateStorageStatusCommand : IRequest<Result>
    {
        public CreateStorageStatusCommand(string code, string name, string @default, string source)
        {
            Code = code;
            Name = name;
            Default = @default;
            Source = source;
        }

        public string Code { get; private set; }
        public string Name { get; private set; }
        public string Default { get; private set; }
        public string Source { get; private set; }
    }
}
