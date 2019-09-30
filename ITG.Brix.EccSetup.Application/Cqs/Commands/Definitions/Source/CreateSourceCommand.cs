using ITG.Brix.EccSetup.Application.Bases;
using MediatR;
using System.Collections.Generic;

namespace ITG.Brix.EccSetup.Application.Cqs.Commands.Definitions
{
    public class CreateSourceCommand : IRequest<Result>
    {
        public string Name { get; private set; }

        public string Description { get; private set; }

        public IEnumerable<string> SourceBusinessUnits { get; private set; }

        public CreateSourceCommand(string name,
                                   string description,
                                   IEnumerable<string> sourceBusinessUnits)
        {
            Name = name;
            Description = description;
            SourceBusinessUnits = sourceBusinessUnits ?? new List<string>();
        }
    }
}
