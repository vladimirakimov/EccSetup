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
    public class CreateInputCommandHandler : IRequestHandler<CreateInputCommand, Result>
    {
        private readonly IInputWriteRepository _inputWriteRepository;
        private readonly IIdentifierProvider _identifierProvider;
        private readonly IVersionProvider _versionProvider;

        public CreateInputCommandHandler(IInputWriteRepository inputWriteRepository,
                                         IIdentifierProvider identifierProvider,
                                         IVersionProvider versionProvider)
        {
            _inputWriteRepository = inputWriteRepository ?? throw Error.ArgumentNull(nameof(inputWriteRepository));
            _identifierProvider = identifierProvider ?? throw Error.ArgumentNull(nameof(identifierProvider));
            _versionProvider = versionProvider ?? throw Error.ArgumentNull(nameof(versionProvider));
        }

        public async Task<Result> Handle(CreateInputCommand request, CancellationToken cancellationToken)
        {
            var id = _identifierProvider.Generate();

            var inputToCreate = new Input(id, request.Name, request.Description, request.Icon, request.Instruction);
            foreach (var image in request.Images)
            {
                inputToCreate.AddImage(new Image(image.Name, "some image url"));
            }
            foreach (var video in request.Videos)
            {
                inputToCreate.AddVideo(new Video(video.Name, "some image url"));
            }
            foreach (var tag in request.Tags)
            {
                inputToCreate.AddTag(new Tag(tag));
            }

            inputToCreate.Version = _versionProvider.Generate();

            Result result;

            try
            {
                await _inputWriteRepository.CreateAsync(inputToCreate);
                result = Result.Ok(id, inputToCreate.Version);
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
