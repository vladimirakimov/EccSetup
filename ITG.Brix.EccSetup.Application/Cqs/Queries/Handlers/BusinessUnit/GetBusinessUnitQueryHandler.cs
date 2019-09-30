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
    public class GetBusinessUnitQueryHandler : IRequestHandler<GetBusinessUnitQuery, Result>
    {
        private readonly IMapper _mapper;
        private readonly IBusinessUnitReadRepository _businessUnitReadRepository;

        public GetBusinessUnitQueryHandler(IMapper mapper,
                                           IBusinessUnitReadRepository businessUnitReadRepository)
        {
            _businessUnitReadRepository = businessUnitReadRepository ?? throw Error.ArgumentNull(nameof(businessUnitReadRepository));
            _mapper = mapper ?? throw Error.ArgumentNull(nameof(mapper));
        }

        public async Task<Result> Handle(GetBusinessUnitQuery request, CancellationToken cancellationToken)
        {
            Result result;

            try
            {
                var businessUnit = await _businessUnitReadRepository.GetAsync(request.Id);
                var businessUnitModel = _mapper.Map<BusinessUnitModel>(businessUnit);

                result = Result.Ok(businessUnitModel, businessUnit.Version);
            }
            catch (EntityNotFoundDbException)
            {
                result = Result.Fail(new System.Collections.Generic.List<Failure>() {
                                        new HandlerFault(){
                                            Code = HandlerFaultCode.NotFound.Name,
                                            Message = string.Format(HandlerFailures.NotFound, "BusinessUnit"),
                                            Target = "id"}
                                        }
                );
            }
            catch
            {
                result = Result.Fail(CustomFailures.GetBusinessUnitFailure);
            }

            return result;
        }
    }
}
