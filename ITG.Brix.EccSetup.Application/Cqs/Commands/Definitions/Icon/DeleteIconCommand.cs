using ITG.Brix.EccSetup.Application.Bases;
using MediatR;
using System;

namespace ITG.Brix.EccSetup.Application.Cqs.Commands.Definitions
{
    public class DeleteIconCommand : IRequest<Result>
    {
        public Guid Id { get; private set; }

        public int Version { get; private set; }

        public DeleteIconCommand() { }

        public DeleteIconCommand(Guid id,
                                 int version) : this()
        {
            Id = id;
            Version = version;
        }
    }
}
