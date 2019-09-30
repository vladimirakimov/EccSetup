using AutoMapper;
using ITG.Brix.EccSetup.Application.Bases;
using ITG.Brix.EccSetup.Application.Cqs.Queries.Definitions;
using ITG.Brix.EccSetup.Application.Cqs.Queries.Models.BuildingBlocks.Remark;
using ITG.Brix.EccSetup.Application.Internal;
using ITG.Brix.EccSetup.Application.Resources;
using ITG.Brix.EccSetup.Domain.Repositories;
using ITG.Brix.EccSetup.Infrastructure.Exceptions;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ITG.Brix.EccSetup.Application.Cqs.Queries.Handlers
{
    public class GetRemarkQueryHandler : IRequestHandler<GetRemarkQuery, Result>
    {
        private readonly IMapper _mapper;
        private readonly IRemarkReadRepository _remarkReadRepository;

        public GetRemarkQueryHandler(IMapper mapper, IRemarkReadRepository remarkReadRepository)
        {
            _mapper = mapper ?? throw Error.ArgumentNull(nameof(mapper));
            _remarkReadRepository = remarkReadRepository ?? throw Error.ArgumentNull(nameof(remarkReadRepository));
        }

        public async Task<Result> Handle(GetRemarkQuery request, CancellationToken cancellationToken)
        {
            Result result;

            try
            {
                var remark = await _remarkReadRepository.GetAsync(request.Id);
                var remarkModel = _mapper.Map<RemarkModel>(remark);

                result = Result.Ok(remarkModel, remark.Version);
            }
            catch (EntityNotFoundDbException)
            {
                result = Result.Fail(new System.Collections.Generic.List<Failure>() {
                                        new HandlerFault(){
                                            Code = HandlerFaultCode.NotFound.Name,
                                            Message = string.Format(HandlerFailures.NotFound, "Remark"),
                                            Target = "id"}
                                        }
                );
            }
            catch
            {
                result = Result.Fail(CustomFailures.GetRemarkFailure);
            }

            return result;
        }
    }
}
