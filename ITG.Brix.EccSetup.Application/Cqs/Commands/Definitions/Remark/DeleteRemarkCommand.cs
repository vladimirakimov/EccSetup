using ITG.Brix.EccSetup.Application.Bases;
using MediatR;
using System;

namespace ITG.Brix.EccSetup.Application.Cqs.Commands.Definitions
{
    public class DeleteRemarkCommand : IRequest<Result>
    {
        public Guid Id { get; private set; }

        public int Version { get; set; }

        public DeleteRemarkCommand() { }

        public DeleteRemarkCommand(Guid id,
                                   int version) : this()
        {
            Id = id;
            Version = version;
        }
    }
}
