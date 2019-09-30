using ITG.Brix.EccSetup.Application.Bases;
using MediatR;
using System;

namespace ITG.Brix.EccSetup.Application.Cqs.Queries.Definitions
{
    public class GetRemarkQuery : IRequest<Result>
    {
        public Guid Id { get; private set; }

        public GetRemarkQuery() { }

        public GetRemarkQuery(Guid id) : this()
        {
            Id = id;
        }
    }
}

