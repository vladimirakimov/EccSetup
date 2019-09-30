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
    public class GetFlowQueryHandler : IRequestHandler<GetFlowQuery, Result>
    {
        private readonly IMapper _mapper;
        private readonly IFlowReadRepository _flowReadRepository;

        public GetFlowQueryHandler(IMapper mapper, IFlowReadRepository flowReadRepository)
        {
            _mapper = mapper ?? throw Error.ArgumentNull(nameof(mapper));
            _flowReadRepository = flowReadRepository ?? throw Error.ArgumentNull(nameof(flowReadRepository));
        }

        public async Task<Result> Handle(GetFlowQuery request, CancellationToken cancellationToken)
        {
            Result result;

            try
            {
                var flow = await _flowReadRepository.GetAsync(request.Id);
                var flowModel = _mapper.Map<FlowModel>(flow);

                result = Result.Ok(flowModel, flow.Version);
            }
            catch (EntityNotFoundDbException)
            {
                result = Result.Fail(new System.Collections.Generic.List<Failure>() {
                                        new HandlerFault(){
                                            Code = HandlerFaultCode.NotFound.Name,
                                            Message = string.Format(HandlerFailures.NotFound, "Flow"),
                                            Target = "id"}
                                        }
                );
            }
            catch
            {
                result = Result.Fail(CustomFailures.GetFlowFailure);
            }

            return result;
        }
    }
}
