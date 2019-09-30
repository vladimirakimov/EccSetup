using ITG.Brix.EccSetup.Application.Bases;
using ITG.Brix.EccSetup.Application.DataTypes;
using MediatR;
using System;

namespace ITG.Brix.EccSetup.Application.Cqs.Commands.Definitions
{
    public class UpdateBusinessUnitCommand : IRequest<Result>
    {
        public Guid Id { get; private set; }
        public Optional<string> Name { get; private set; }
        public int Version { get; private set; }

        public UpdateBusinessUnitCommand(Guid id,
                                         Optional<string> name,
                                         int version)
        {
            Id = id;
            Name = name;
            Version = version;
        }
    }
}
