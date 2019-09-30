using ITG.Brix.EccSetup.Application.Bases;
using MediatR;

namespace ITG.Brix.EccSetup.Application.Cqs.Commands.Definitions
{
    public class CreateOperationalDepartmentCommand : IRequest<Result>
    {
        public CreateOperationalDepartmentCommand(string code, string name, string site, string source)
        {
            Code = code;
            Name = name;
            Site = site;
            Source = source;
        }

        public string Code { get; private set; }
        public string Name { get; private set; }
        public string Site { get; private set; }
        public string Source { get; private set; }
    }
}
