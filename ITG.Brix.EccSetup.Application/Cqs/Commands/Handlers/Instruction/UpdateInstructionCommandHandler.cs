using ITG.Brix.EccSetup.Application.Bases;
using ITG.Brix.EccSetup.Application.Cqs.Commands.Definitions;
using ITG.Brix.EccSetup.Application.Exceptions;
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
    public class UpdateInstructionCommandHandler : IRequestHandler<UpdateInstructionCommand, Result>
    {
        private readonly IInstructionWriteRepository _instructionWriteRepository;
        private readonly IInstructionReadRepository _instructionReadRepository;
        private readonly IVersionProvider _versionProvider;

        public UpdateInstructionCommandHandler(IInstructionWriteRepository instructionWriteRepository,
                                               IInstructionReadRepository instructionReadRepository,
                                               IVersionProvider versionProvider)
        {
            _instructionWriteRepository = instructionWriteRepository ?? throw Error.ArgumentNull(nameof(instructionWriteRepository));
            _instructionReadRepository = instructionReadRepository ?? throw Error.ArgumentNull(nameof(instructionReadRepository));
            _versionProvider = versionProvider ?? throw Error.ArgumentNull(nameof(versionProvider));
        }
        public async Task<Result> Handle(UpdateInstructionCommand request, CancellationToken cancellationToken)
        {
            Result result;
            try
            {
                var instruction = await _instructionReadRepository.GetAsync(request.Id);
                if (instruction.Version != request.Version)
                {
                    throw new CommandVersionException();
                }

                var updatedInstruction = new Instruction(request.Id, request.Name, request.Description, request.Icon, request.Content, request.Image, request.Video);
                foreach (var tag in request.Tags)
                {
                    updatedInstruction.AddTag(new Tag(tag));
                }

                updatedInstruction.Version = _versionProvider.Generate();
                await _instructionWriteRepository.UpdateAsync(updatedInstruction);
                result = Result.Ok(updatedInstruction.Version);

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
            catch (CommandVersionException)
            {
                result = Result.Fail(new System.Collections.Generic.List<Failure>() {
                                        new HandlerFault(){
                                            Code = HandlerFaultCode.NotMet.Name,
                                            Message = HandlerFailures.NotMet,
                                            Target = "version"}
                                        }
                );
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
                result = Result.Fail(CustomFailures.UpdateInstructionFailure);
            }

            return result;
        }
    }
}
