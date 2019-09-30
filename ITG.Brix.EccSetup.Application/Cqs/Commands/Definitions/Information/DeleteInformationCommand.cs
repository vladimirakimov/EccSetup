using ITG.Brix.EccSetup.Application.Bases;
using MediatR;
using System;

namespace ITG.Brix.EccSetup.Application.Cqs.Commands.Definitions
{
    public class DeleteInformationCommand : IRequest<Result>
    {
        public Guid Id { get; private set; }
        public int Version { get; private set; }

        public DeleteInformationCommand(Guid id,
                                        int version)
        {
            Id = id;
            Version = version;
        }
    }
}
