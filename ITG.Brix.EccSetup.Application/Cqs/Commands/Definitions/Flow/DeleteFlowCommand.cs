using ITG.Brix.EccSetup.Application.Bases;
using MediatR;
using System;

namespace ITG.Brix.EccSetup.Application.Cqs.Commands.Definitions
{
    public class DeleteFlowCommand : IRequest<Result>
    {
        public Guid Id { get; private set; }

        public int Version { get; private set; }

        public DeleteFlowCommand() { }

        public DeleteFlowCommand(Guid id,
                                 int version) : this()
        {
            Id = id;
            Version = version;
        }
    }
}
