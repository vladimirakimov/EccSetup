using ITG.Brix.EccSetup.Application.Bases;
using MediatR;
using System;

namespace ITG.Brix.EccSetup.Application.Cqs.Commands.Definitions
{
    public class DeleteInstructionCommand : IRequest<Result>
    {
        public Guid Id { get; private set; }
        public int Version { get; private set; }

        public DeleteInstructionCommand(Guid id,
                                        int version)
        {
            Id = id;
            Version = version;
        }
    }
}
