using ITG.Brix.EccSetup.Application.Bases;
using ITG.Brix.EccSetup.Application.Cqs.Commands.Definitions;
using ITG.Brix.EccSetup.Application.Internal;
using ITG.Brix.EccSetup.Application.Resources;
using ITG.Brix.EccSetup.Domain;
using ITG.Brix.EccSetup.Domain.Repositories;
using ITG.Brix.EccSetup.Infrastructure.Exceptions;
using ITG.Brix.EccSetup.Infrastructure.Providers;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ITG.Brix.EccSetup.Application.Cqs.Commands.Handlers
{
    public class CreateInstructionCommandHandler : IRequestHandler<CreateInstructionCommand, Result>
    {
        private readonly IInstructionWriteRepository _instructionWriteRepository;
        private readonly IIdentifierProvider _identifierProvider;
        private readonly IVersionProvider _versionProvider;

        public CreateInstructionCommandHandler(IInstructionWriteRepository instructionWriteRepository,
                                               IIdentifierProvider identifierProvider,
                                               IVersionProvider versionProvider)
        {
            _instructionWriteRepository = instructionWriteRepository ?? throw Error.ArgumentNull(nameof(instructionWriteRepository));
            _identifierProvider = identifierProvider ?? throw Error.ArgumentNull(nameof(identifierProvider));
            _versionProvider = versionProvider ?? throw Error.ArgumentNull(nameof(versionProvider));
        }

        public async Task<Result> Handle(CreateInstructionCommand request, CancellationToken cancellationToken)
        {
            var id = _identifierProvider.Generate();
            var instructionToCreate = new Instruction(id, request.Name, request.Description, request.Icon, request.Content, request.Image, request.Video);
            foreach (var tag in request.Tags)
            {
                instructionToCreate.AddTag(new Tag(tag));
            }
            instructionToCreate.Version = _versionProvider.Generate();

            Result result;

            try
            {
                await _instructionWriteRepository.CreateAsync(instructionToCreate);
                result = Result.Ok(id, instructionToCreate.Version);
            }
            catch (UniqueKeyException)
            {
                result = Result.Fail(new System.Collections.Generic.List<Failure>() {
                                        new HandlerFault(){
                                            Code = HandlerFaultCode.Conflict.Name,
                                            Message = HandlerFailures.Conflict,
                                            Target = "name"}
                                        }
                );
            }
            catch
            {
                result = Result.Fail(CustomFailures.CreateInstructionFailure);
            }

            return result;
        }
    }
}
