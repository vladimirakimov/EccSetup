using ITG.Brix.EccSetup.Application.Bases;
using MediatR;
using System;

namespace ITG.Brix.EccSetup.Application.Cqs.Commands.Definitions
{
    public class DeleteBusinessUnitCommand : IRequest<Result>
    {
        public Guid Id { get; private set; }
        public int Version { get; private set; }

        public DeleteBusinessUnitCommand(Guid id,
                                         int version)
        {
            Id = id;
            Version = version;
        }
    }
}
