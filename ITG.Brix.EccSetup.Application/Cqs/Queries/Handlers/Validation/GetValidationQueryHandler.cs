using AutoMapper;
using ITG.Brix.EccSetup.Application.Bases;
using ITG.Brix.EccSetup.Application.Cqs.Queries.Definitions;
using ITG.Brix.EccSetup.Application.Cqs.Queries.Models.BuildingBlocks.Validations;
using ITG.Brix.EccSetup.Application.Internal;
using ITG.Brix.EccSetup.Application.Resources;
using ITG.Brix.EccSetup.Domain.Repositories;
using ITG.Brix.EccSetup.Infrastructure.Exceptions;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ITG.Brix.EccSetup.Application.Cqs.Queries.Handlers
{
    public class GetValidationQueryHandler : IRequestHandler<GetValidationQuery, Result>
    {
        private readonly IMapper _mapper;
        private readonly IValidationReadRepository _validationReadRepository;

        public GetValidationQueryHandler(IMapper mapper,
                                         IValidationReadRepository validationReadRepository)
        {
            _mapper = mapper ?? throw Error.ArgumentNull(nameof(mapper));
            _validationReadRepository = validationReadRepository ?? throw Error.ArgumentNull(nameof(validationReadRepository));
        }

        public async Task<Result> Handle(GetValidationQuery request, CancellationToken cancellationToken)
        {
            Result result;

            try
            {
                var validation = await _validationReadRepository.GetAsync(request.Id);
                var validationModel = _mapper.Map<ValidationModel>(validation);

                result = Result.Ok(validationModel, validation.Version);
            }
            catch (EntityNotFoundDbException)
            {
                result = Result.Fail(new System.Collections.Generic.List<Failure>() {
                                        new HandlerFault(){
                                            Code = HandlerFaultCode.NotFound.Name,
                                            Message = string.Format(HandlerFailures.NotFound, "Validation"),
                                            Target = "id"}
                                        }
                );
            }
            catch
            {
                result = Result.Fail(CustomFailures.GetValidationFailure);
            }

            return result;
        }
    }
}
