using ITG.Brix.EccSetup.Application.Bases;
using ITG.Brix.EccSetup.Application.Cqs.Commands.Definitions;
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
    public class CreateValidationCommandHandler : IRequestHandler<CreateValidationCommand, Result>
    {
        private readonly IValidationWriteRepository _validationWriteRepository;
        private readonly IIdentifierProvider _identifierProvider;
        private readonly IVersionProvider _versionProvider;

        public CreateValidationCommandHandler(IValidationWriteRepository validationWriteRepository,
                                              IIdentifierProvider identifierProvider,
                                              IVersionProvider versionProvider)
        {
            _validationWriteRepository = validationWriteRepository ?? throw Error.ArgumentNull(nameof(validationWriteRepository));
            _identifierProvider = identifierProvider ?? throw Error.ArgumentNull(nameof(identifierProvider));
            _versionProvider = versionProvider ?? throw Error.ArgumentNull(nameof(versionProvider));
        }

        public async Task<Result> Handle(CreateValidationCommand request, CancellationToken cancellationToken)
        {
            var id = _identifierProvider.Generate();
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


            var validationBlockToCreate = new Validation(id,
                                                 request.Name,
                                                 request.NameOnApplication,
                                                 request.Description,
                                                 request.Instruction,
                                                 icon);

            foreach (var tag in tags)
            {
                validationBlockToCreate.AddTag(tag);
            }
            foreach (var image in images)
            {
                validationBlockToCreate.AddImage(image);
            }
            foreach (var video in videos)
            {
                validationBlockToCreate.AddVideo(video);
            }

            validationBlockToCreate.Version = _versionProvider.Generate();

            Result result;

            try
            {
                await _validationWriteRepository.CreateAsync(validationBlockToCreate);
                result = Result.Ok(id, validationBlockToCreate.Version);
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
                result = Result.Fail(CustomFailures.CreateValidationFailure);
            }

            return result;
        }
    }
}
