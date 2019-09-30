using ITG.Brix.EccSetup.Application.Bases;
using ITG.Brix.EccSetup.Application.Cqs.Commands.Dtos;
using ITG.Brix.EccSetup.Application.DataTypes;
using MediatR;
using System;
using System.Collections.Generic;

namespace ITG.Brix.EccSetup.Application.Cqs.Commands.Definitions
{
    public class UpdateOperationCommand : IRequest<Result>
    {
        public Guid Id { get; private set; }

        public Optional<string> Name { get; private set; }

        public Optional<string> Description { get; private set; }

        public ColoredIconDto Icon { get; private set; }

        public Optional<IEnumerable<string>> Tags { get; private set; }
        public int Version { get; private set; }


        public UpdateOperationCommand(Guid id,
                                      Optional<string> name,
                                      Optional<string> description,
                                      ColoredIconDto icon,
                                      Optional<IEnumerable<string>> tags,
                                      int version)
        {
            Id = id;
            Name = name;
            Description = description;
            Icon = icon;
            Tags = tags;
            Version = version;
        }
    }
}
