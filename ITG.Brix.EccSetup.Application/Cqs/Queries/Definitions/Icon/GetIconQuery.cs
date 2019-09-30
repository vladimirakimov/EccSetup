using ITG.Brix.EccSetup.Application.Bases;
using MediatR;
using System;

namespace ITG.Brix.EccSetup.Application.Cqs.Queries.Definitions
{
    public class GetIconQuery : IRequest<Result>
    {
        public Guid Id { get; private set; }

        public GetIconQuery() { }

        public GetIconQuery(Guid id) : this()
        {
            Id = id;
        }
    }
}
