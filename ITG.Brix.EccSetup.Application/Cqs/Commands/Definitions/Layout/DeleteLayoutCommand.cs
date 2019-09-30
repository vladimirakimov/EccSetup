using ITG.Brix.EccSetup.Application.Bases;
using MediatR;
using System;

namespace ITG.Brix.EccSetup.Application.Cqs.Commands.Definitions
{
    public class DeleteLayoutCommand : IRequest<Result>
    {
        public Guid Id { get; private set; }

        public int Version { get; private set; }

        public DeleteLayoutCommand() { }

        public DeleteLayoutCommand(Guid id,
                                   int version) : this()
        {
            Id = id;
            Version = version;
        }
    }
}
