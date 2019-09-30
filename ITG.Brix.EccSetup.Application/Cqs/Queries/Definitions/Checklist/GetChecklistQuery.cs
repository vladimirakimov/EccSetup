using ITG.Brix.EccSetup.Application.Bases;
using MediatR;
using System;

namespace ITG.Brix.EccSetup.Application.Cqs.Queries.Definitions
{
    public class GetChecklistQuery : IRequest<Result>
    {
        public Guid Id { get; private set; }

        public GetChecklistQuery() { }

        public GetChecklistQuery(Guid id) : this()
        {
            Id = id;
        }
    }
}
