using AutoMapper;
using ITG.Brix.EccSetup.Application.Bases;
using ITG.Brix.EccSetup.Application.Cqs.Queries.Definitions;
using ITG.Brix.EccSetup.Application.Cqs.Queries.Models;
using ITG.Brix.EccSetup.Application.Internal;
using ITG.Brix.EccSetup.Application.Resources;
using ITG.Brix.EccSetup.Domain.Repositories;
using ITG.Brix.EccSetup.Infrastructure.Exceptions;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ITG.Brix.EccSetup.Application.Cqs.Queries.Handlers
{
    public class GetLayoutQueryHandler : IRequestHandler<GetLayoutQuery, Result>
    {
        private readonly IMapper _mapper;
        private readonly ILayoutReadRepository _layoutReadRepository;

        public GetLayoutQueryHandler(IMapper mapper, ILayoutReadRepository layoutReadRepository)
        {
            _mapper = mapper ?? throw Error.ArgumentNull(nameof(mapper));
            _layoutReadRepository = layoutReadRepository ?? throw Error.ArgumentNull(nameof(layoutReadRepository));
        }

        public async Task<Result> Handle(GetLayoutQuery request, CancellationToken cancellationToken)
        {
            Result result;

            try
            {
                var layout = await _layoutReadRepository.GetAsync(request.Id);
                var layoutModel = _mapper.Map<LayoutModel>(layout);


                result = Result.Ok(layoutModel, layout.Version);
            }
            catch (EntityNotFoundDbException)
            {
                result = Result.Fail(new System.Collections.Generic.List<Failure>() {
                                        new HandlerFault(){
                                            Code = HandlerFaultCode.NotFound.Name,
                                            Message = string.Format(HandlerFailures.NotFound, "Layout"),
                                            Target = "id"}
                                        }
                );
            }
            catch
            {
                result = Result.Fail(CustomFailures.GetLayoutFailure);
            }

            return result;
        }
    }
}
