using ITG.Brix.EccSetup.Application.Bases;
using MediatR;

namespace ITG.Brix.EccSetup.Application.Cqs.Queries.Definitions
{
    public class ListDamageCodeQuery : IRequest<Result>
    {
        public ListDamageCodeQuery(string filter, string top, string skip)
        {
            Filter = filter;
            Top = top;
            Skip = skip;
        }

        public string Filter { get; private set; }
        public string Top { get; private set; }
        public string Skip { get; private set; }
    }
}
