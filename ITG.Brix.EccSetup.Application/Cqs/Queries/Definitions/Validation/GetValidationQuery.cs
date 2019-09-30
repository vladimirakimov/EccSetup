using ITG.Brix.EccSetup.Application.Bases;
using MediatR;
using System;

namespace ITG.Brix.EccSetup.Application.Cqs.Queries.Definitions
{
    public class GetValidationQuery : IRequest<Result>
    {
        public Guid Id { get; private set; }

        public GetValidationQuery(Guid id)
        {
            Id = id;
        }
    }
}
