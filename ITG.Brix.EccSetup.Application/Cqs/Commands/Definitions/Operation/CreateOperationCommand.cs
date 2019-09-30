using ITG.Brix.EccSetup.Application.Bases;
using ITG.Brix.EccSetup.Application.Cqs.Commands.Dtos;
using MediatR;
using System.Collections.Generic;

namespace ITG.Brix.EccSetup.Application.Cqs.Commands.Definitions
{
    public class CreateOperationCommand : IRequest<Result>
    {
        public string Name { get; private set; }

        public string Description { get; private set; }

        public ColoredIconDto Icon { get; private set; }

        public IEnumerable<string> Tags { get; private set; }


        public CreateOperationCommand(string name,
                                      string description,
                                      ColoredIconDto icon,
                                      IEnumerable<string> tags)
        {
            Name = name;
            Description = description;
            Icon = icon;
            Tags = tags ?? new List<string>();
        }
    }
}
