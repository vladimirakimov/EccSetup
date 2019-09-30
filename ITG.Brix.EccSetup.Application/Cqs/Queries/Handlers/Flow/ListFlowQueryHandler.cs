using AutoMapper;
using ITG.Brix.EccSetup.Application.Bases;
using ITG.Brix.EccSetup.Application.Cqs.Queries.Definitions;
using ITG.Brix.EccSetup.Application.Cqs.Queries.Models;
using ITG.Brix.EccSetup.Application.Extensions;
using ITG.Brix.EccSetup.Application.Internal;
using ITG.Brix.EccSetup.Application.Resources;
using ITG.Brix.EccSetup.Domain.Repositories;
using ITG.Brix.EccSetup.Infrastructure.Exceptions;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ITG.Brix.EccSetup.Application.Cqs.Queries.Handlers
{
    public class ListFlowQueryHandler : IRequestHandler<ListFlowQuery, Result>
    {
        private readonly IMapper _mapper;
        private readonly IFlowReadRepository _flowReadRepository;

        public ListFlowQueryHandler(IMapper mapper,
                                    IFlowReadRepository flowReadRepository)
        {
            _mapper = mapper ?? throw Error.ArgumentNull(nameof(mapper));
            _flowReadRepository = flowReadRepository ?? throw Error.ArgumentNull(nameof(flowReadRepository));
        }

        public async Task<Result> Handle(ListFlowQuery request, CancellationToken cancellationToken)
        {
            Result result;
            try
            {
                int? skip = request.Skip.ToNullableInt();
                int? top = request.Top.ToNullableInt();

                var flowDomains = await _flowReadRepository.ListAsync(request.Filter, skip, top);
                var flowModels = _mapper.Map<IEnumerable<FlowModel>>(flowDomains);
                var count = flowModels.Count();
                var sourcesModel = new FlowsModel { Value = flowModels, Count = count, NextLink = null };

                result = Result.Ok(sourcesModel);
            }
            catch (FilterODataException)
            {
                result = Result.Fail(new System.Collections.Generic.List<Failure>() {
                                        new HandlerFault(){
                                            Code = HandlerFaultCode.InvalidQueryFilter.Name,
                                            Message = HandlerFailures.InvalidQueryFilter,
                                            Target = "$filter"}
                                        }
                );
            }
            catch
            {
                result = Result.Fail(CustomFailures.ListFlowFailure);
            }
            return result;
        }
    }
}
