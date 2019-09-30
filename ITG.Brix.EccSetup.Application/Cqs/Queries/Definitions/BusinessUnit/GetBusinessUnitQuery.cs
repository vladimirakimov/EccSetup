using ITG.Brix.EccSetup.Application.Bases;
using MediatR;
using System;

namespace ITG.Brix.EccSetup.Application.Cqs.Queries.Definitions
{
    public class GetBusinessUnitQuery : IRequest<Result>
    {
        public Guid Id { get; private set; }

        public GetBusinessUnitQuery() { }

        public GetBusinessUnitQuery(Guid id) : this()
        {
            Id = id;
        }
    }
}
