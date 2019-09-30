using ITG.Brix.EccSetup.Application.Bases;
using MediatR;
using System;

namespace ITG.Brix.EccSetup.Application.Cqs.Queries.Definitions
{
    public class GetLayoutQuery : IRequest<Result>
    {
        public Guid Id { get; private set; }

        public GetLayoutQuery() { }

        public GetLayoutQuery(Guid id) : this()
        {
            Id = id;
        }
    }
}
