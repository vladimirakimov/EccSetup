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
    public class UpdateInputCommandHandler : IRequestHandler<UpdateInputCommand, Result>
    {
        private readonly IInputWriteRepository _inputWriteRepository;
        private readonly IInputReadRepository _inputReadRepository;
        private readonly IVersionProvider _versionProvider;

        public UpdateInputCommandHandler(IInputWriteRepository inputWriteRepository,
                                         IInputReadRepository inputReadRepository,
                                         IVersionProvider versionProvider)
        {
            _inputWriteRepository = inputWriteRepository ?? throw Error.ArgumentNull(nameof(inputWriteRepository));
            _inputReadRepository = inputReadRepository ?? throw Error.ArgumentNull(nameof(inputReadRepository));
            _versionProvider = versionProvider ?? throw Error.ArgumentNull(nameof(versionProvider));
        }

        public async Task<Result> Handle(UpdateInputCommand request, CancellationToken cancellationToken)
        {
            Result result;
            try
            {
                var input = await _inputReadRepository.GetAsync(request.Id);
                if (input.Version != request.Version)
                {
                    throw new CommandVersionException();
                }

                var updatedInput = new Input(request.Id, request.Name, request.Description, request.Icon, request.Instruction);
                foreach (var image in request.Images)
                {
                    updatedInput.AddImage(new Image(image.Name, "some image url"));
                }
                foreach (var video in request.Videos)
                {
                    updatedInput.AddVideo(new Video(video.Name, "some image url"));
                }
                foreach (var tag in request.Tags)
                {
                    updatedInput.AddTag(new Tag(tag));
                }

                updatedInput.Version = _versionProvider.Generate();
                await _inputWriteRepository.UpdateAsync(updatedInput);
                result = Result.Ok(updatedInput.Version);

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
                result = Result.Fail(CustomFailures.UpdateInputFailure);
            }

            return result;
        }
    }
}
