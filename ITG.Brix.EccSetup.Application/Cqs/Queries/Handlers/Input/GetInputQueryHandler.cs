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
    public class GetInputQueryHandler : IRequestHandler<GetInputQuery, Result>
    {
        private readonly IMapper _mapper;
        private readonly IInputReadRepository _inputReadRepository;

        public GetInputQueryHandler(IMapper mapper,
                                    IInputReadRepository inputReadRepository)
        {
            _mapper = mapper ?? throw Error.ArgumentNull(nameof(mapper));
            _inputReadRepository = inputReadRepository ?? throw Error.ArgumentNull(nameof(inputReadRepository));
        }

        public async Task<Result> Handle(GetInputQuery request, CancellationToken cancellationToken)
        {
            Result result;

            try
            {
                var input = await _inputReadRepository.GetAsync(request.Id);
                var inputModel = _mapper.Map<InputModel>(input);

                result = Result.Ok(inputModel, input.Version);
            }
            catch (EntityNotFoundDbException)
            {
                result = Result.Fail(new System.Collections.Generic.List<Failure>() {
                                        new HandlerFault(){
                                            Code = HandlerFaultCode.NotFound.Name,
                                            Message = string.Format(HandlerFailures.NotFound, "Input"),
                                            Target = "id"}
                                        }
                );
            }
            catch
            {
                result = Result.Fail(CustomFailures.GetInputFailure);
            }

            return result;
        }
    }
}
