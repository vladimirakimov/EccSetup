using ITG.Brix.EccSetup.Application.Bases;
using MediatR;
using System;

namespace ITG.Brix.EccSetup.Application.Cqs.Queries.Definitions
{
    public class GetInputQuery : IRequest<Result>
    {
        public Guid Id { get; private set; }

        public GetInputQuery() { }

        public GetInputQuery(Guid id) : this()
        {
            Id = id;
        }
    }
}
