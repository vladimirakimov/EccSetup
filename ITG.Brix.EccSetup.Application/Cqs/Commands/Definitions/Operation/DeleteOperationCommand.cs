using ITG.Brix.EccSetup.Application.Bases;
using MediatR;
using System;

namespace ITG.Brix.EccSetup.Application.Cqs.Commands.Definitions
{
    public class DeleteOperationCommand : IRequest<Result>
    {
        public Guid Id { get; private set; }

        public int Version { get; private set; }

        public DeleteOperationCommand() { }

        public DeleteOperationCommand(Guid id,
                                      int version) : this()
        {
            Id = id;
            Version = version;
        }
    }
}
