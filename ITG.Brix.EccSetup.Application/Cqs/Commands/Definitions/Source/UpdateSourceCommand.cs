using ITG.Brix.EccSetup.Application.Bases;
using ITG.Brix.EccSetup.Application.DataTypes;
using MediatR;
using System;
using System.Collections.Generic;

namespace ITG.Brix.EccSetup.Application.Cqs.Commands.Definitions
{
    public class UpdateSourceCommand : IRequest<Result>
    {
        public Guid Id { get; private set; }

        public Optional<string> Name { get; private set; }

        public Optional<string> Description { get; private set; }

        public Optional<IEnumerable<string>> SourceBusinessUnits { get; private set; }

        public int Version { get; private set; }

        public UpdateSourceCommand(Guid id,
                                   Optional<string> name,
                                   Optional<string> description,
                                   Optional<IEnumerable<string>> sourceBusinessUnits,
                                   int version)
        {
            Id = id;
            Name = name;
            Description = description;
            SourceBusinessUnits = sourceBusinessUnits;
            Version = version;
        }
    }
}
