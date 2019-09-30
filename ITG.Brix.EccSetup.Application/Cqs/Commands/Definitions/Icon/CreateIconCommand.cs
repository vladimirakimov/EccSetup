using ITG.Brix.EccSetup.Application.Bases;
using MediatR;

namespace ITG.Brix.EccSetup.Application.Cqs.Commands.Definitions
{
    public class CreateIconCommand : IRequest<Result>
    {
        public string Name { get; private set; }

        public string DataPath { get; private set; }


        public CreateIconCommand(string name,
                                 string dataPath)
        {
            Name = name;
            DataPath = dataPath;
        }
    }
}
