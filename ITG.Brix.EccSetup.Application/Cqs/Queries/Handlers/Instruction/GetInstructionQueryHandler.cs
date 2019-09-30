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
    public class GetInstructionQueryHandler : IRequestHandler<GetInstructionQuery, Result>
    {
        private readonly IInstructionReadRepository _instructionReadRepository;
        private readonly IMapper _mapper;

        public GetInstructionQueryHandler(IMapper mapper, IInstructionReadRepository instructionReadRepository)
        {
            _mapper = mapper ?? throw Error.ArgumentNull(nameof(mapper));
            _instructionReadRepository = instructionReadRepository ?? throw Error.ArgumentNull(nameof(instructionReadRepository));
        }

        public async Task<Result> Handle(GetInstructionQuery request, CancellationToken cancellationToken)
        {
            Result result;

            try
            {
                var instruction = await _instructionReadRepository.GetAsync(request.Id);
                var instructionModel = _mapper.Map<InstructionModel>(instruction);

                result = Result.Ok(instructionModel, instruction.Version);
            }
            catch (EntityNotFoundDbException)
            {
                result = Result.Fail(new System.Collections.Generic.List<Failure>() {
                                        new HandlerFault(){
                                            Code = HandlerFaultCode.NotFound.Name,
                                            Message = string.Format(HandlerFailures.NotFound, "Instruction"),
                                            Target = "id"}
                                        }
                );
            }
            catch
            {
                result = Result.Fail(CustomFailures.GetInstructionFailure);
            }

            return result;
        }
    }
}
