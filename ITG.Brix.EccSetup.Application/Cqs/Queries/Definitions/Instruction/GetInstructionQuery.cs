using ITG.Brix.EccSetup.Application.Bases;
using MediatR;
using System;

namespace ITG.Brix.EccSetup.Application.Cqs.Queries.Definitions
{
    public class GetInstructionQuery : IRequest<Result>
    {
        public Guid Id { get; private set; }
        public GetInstructionQuery() { }

        public GetInstructionQuery(Guid id) : this()
        {
            Id = id;
        }
    }
}
