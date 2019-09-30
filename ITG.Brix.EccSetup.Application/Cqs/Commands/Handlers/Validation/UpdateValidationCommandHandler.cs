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
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ITG.Brix.EccSetup.Application.Cqs.Commands.Handlers
{
    public class UpdateValidationCommandHandler : IRequestHandler<UpdateValidationCommand, Result>
    {
        private readonly IValidationReadRepository _validationReadRepository;
        private readonly IValidationWriteRepository _validationWriteRepository;
        private readonly IVersionProvider _versionProvider;

        public UpdateValidationCommandHandler(IValidationWriteRepository validationWriteRepository,
                                              IValidationReadRepository validationReadRepository,
                                              IVersionProvider versionProvider)
        {
            _validationWriteRepository = validationWriteRepository ?? throw Error.ArgumentNull(nameof(validationWriteRepository));
            _validationReadRepository = validationReadRepository ?? throw Error.ArgumentNull(nameof(validationReadRepository));
            _versionProvider = versionProvider ?? throw Error.ArgumentNull(nameof(versionProvider));
        }

        public async Task<Result> Handle(UpdateValidationCommand request, CancellationToken cancellationToken)
        {
            Result result;

            try
            {
                var validation = await _validationReadRepository.GetAsync(request.Id);
                if (validation.Version != request.Version)
                {
                    throw new CommandVersionException();
                }

                var tags = new List<Tag>();
                foreach (var tag in request.Tags)
                {
                    tags.Add(new Tag(tag));
                }
                var images = new List<Image>();
                foreach (var image in request.Images)
                {
                    images.Add(new Image(image.Name, "some image url"));
                }
                var videos = new List<Video>();
                foreach (var video in request.Videos)
                {
                    videos.Add(new Video(video.Name, "some image url"));
                }

                var icon = new BuildingBlockIcon(request.Icon.Id);


                var updatedValidation = new Validation(request.Id,
                                                     request.Name,
                                                     request.NameOnApplication,
                                                     request.Description,
                                                     request.Instruction,
                                                     icon);

                foreach (var tag in tags)
                {
                    updatedValidation.AddTag(tag);
                }
                foreach (var image in images)
                {
                    updatedValidation.AddImage(image);
                }
                foreach (var video in videos)
                {
                    updatedValidation.AddVideo(video);
                }

                updatedValidation.Version = _versionProvider.Generate();


                await _validationWriteRepository.UpdateAsync(updatedValidation);
                result = Result.Ok(updatedValidation.Version);

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
                result = Result.Fail(CustomFailures.UpdateValidationFailure);
            }

            return result;
        }
    }
}
