using ITG.Brix.EccSetup.Application.Bases;
using MediatR;
using System;

namespace ITG.Brix.EccSetup.Application.Cqs.Queries.Definitions
{
    public class GetSourceQuery : IRequest<Result>
    {
        public Guid Id { get; private set; }

        public GetSourceQuery() { }

        public GetSourceQuery(Guid id) : this()
        {
            Id = id;
        }
    }
}
