using ITG.Brix.EccSetup.Application.Bases;
using MediatR;
using System;

namespace ITG.Brix.EccSetup.Application.Cqs.Queries.Definitions
{
    public class GetInformationQuery : IRequest<Result>
    {
        public Guid Id { get; set; }

        public GetInformationQuery() { }

        public GetInformationQuery(Guid id) : this()
        {
            Id = id;
        }
    }
}
