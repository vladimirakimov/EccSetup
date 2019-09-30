using ITG.Brix.EccSetup.Application.Bases;
using MediatR;
using System;

namespace ITG.Brix.EccSetup.Application.Cqs.Queries.Definitions
{
    public class GetFlowQuery : IRequest<Result>
    {
        public Guid Id { get; private set; }

        public GetFlowQuery() { }

        public GetFlowQuery(Guid id) : this()
        {
            Id = id;
        }
    }
}
