using ITG.Brix.EccSetup.Application.Bases;
using ITG.Brix.EccSetup.Application.Cqs.Queries.Definitions;
using ITG.Brix.EccSetup.Domain;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ITG.Brix.EccSetup.Application.Cqs.Queries.Handlers
{
    public class ListBlockTypeQueryHandler : IRequestHandler<ListBlockTypeQuery, Result>
    {
        public async Task<Result> Handle(ListBlockTypeQuery request, CancellationToken cancellationToken)
        {
            var result = BlockType.List().Select(x => x.Name);

            return await Task.FromResult(Result.Ok(result));
        }
    }
}
