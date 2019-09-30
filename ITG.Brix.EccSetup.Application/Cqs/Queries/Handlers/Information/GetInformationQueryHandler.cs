using AutoMapper;
using ITG.Brix.EccSetup.Application.Bases;
using ITG.Brix.EccSetup.Application.Cqs.Queries.Definitions;
using ITG.Brix.EccSetup.Application.Cqs.Queries.Models.BuildingBlocks;
using ITG.Brix.EccSetup.Application.Internal;
using ITG.Brix.EccSetup.Application.Resources;
using ITG.Brix.EccSetup.Domain.Repositories;
using ITG.Brix.EccSetup.Infrastructure.Exceptions;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ITG.Brix.EccSetup.Application.Cqs.Queries.Handlers
{
    public class GetInformationQueryHandler : IRequestHandler<GetInformationQuery, Result>
    {
        private readonly IMapper _mapper;
        private readonly IInformationReadRepository _informationReadRepository;

        public GetInformationQueryHandler(IMapper mapper,
                                          IInformationReadRepository informationReadRepository)
        {
            _mapper = mapper ?? throw Error.ArgumentNull(nameof(mapper));
            _informationReadRepository = informationReadRepository ?? throw Error.ArgumentNull(nameof(informationReadRepository));
        }

        public async Task<Result> Handle(GetInformationQuery request, CancellationToken cancellationToken)
        {
            Result result;

            try
            {
                var information = await _informationReadRepository.GetAsync(request.Id);
                var informationModel = _mapper.Map<InformationModel>(information);

                result = Result.Ok(informationModel, information.Version);
            }
            catch (EntityNotFoundDbException)
            {
                result = Result.Fail(new System.Collections.Generic.List<Failure>() {
                                        new HandlerFault(){
                                            Code = HandlerFaultCode.NotFound.Name,
                                            Message = string.Format(HandlerFailures.NotFound, "Information"),
                                            Target = "id"}
                                        }
                );
            }
            catch
            {
                result = Result.Fail(CustomFailures.GetInformationFailure);
            }

            return result;
        }
    }
}
