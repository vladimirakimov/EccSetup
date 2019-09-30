using ITG.Brix.EccSetup.Application.Bases;
using ITG.Brix.EccSetup.Application.DataTypes;
using MediatR;
using System;

namespace ITG.Brix.EccSetup.Application.Cqs.Commands.Definitions
{
    public class UpdateLayoutCommand : IRequest<Result>
    {
        public Guid Id { get; private set; }
        public Optional<string> Name { get; private set; }
        public Optional<string> Description { get; private set; }
        public Optional<string> Image { get; private set; }
        public Optional<string> Diagram { get; private set; }
        public int Version { get; private set; }

        public UpdateLayoutCommand(Guid id,
                                   Optional<string> name,
                                   Optional<string> description,
                                   Optional<string> image,
                                   Optional<string> diagram,
                                   int version)
        {
            Id = id;
            Name = name;
            Description = description;
            Image = image;
            Diagram = diagram;
            Version = version;
        }
    }
}
