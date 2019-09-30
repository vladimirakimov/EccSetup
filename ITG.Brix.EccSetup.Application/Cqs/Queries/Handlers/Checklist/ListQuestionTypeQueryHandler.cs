using ITG.Brix.EccSetup.Application.Bases;
using ITG.Brix.EccSetup.Application.Cqs.Queries.Definitions;
using ITG.Brix.EccSetup.Domain;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ITG.Brix.EccSetup.Application.Cqs.Queries.Handlers
{
    public class ListQuestionTypeQueryHandler : IRequestHandler<ListQuestionTypeQuery, Result>
    {
        public async Task<Result> Handle(ListQuestionTypeQuery request, CancellationToken cancellationToken)
        {
            var result = QuestionType.List().Select(x => x.Name);

            return await Task.FromResult(Result.Ok(result));
        }
    }
}
