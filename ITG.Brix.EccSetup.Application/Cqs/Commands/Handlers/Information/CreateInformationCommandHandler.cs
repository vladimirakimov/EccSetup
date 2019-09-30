using ITG.Brix.EccSetup.Application.Bases;
using ITG.Brix.EccSetup.Application.Cqs.Commands.Definitions;
using ITG.Brix.EccSetup.Application.Internal;
using ITG.Brix.EccSetup.Application.Resources;
using ITG.Brix.EccSetup.Domain;
using ITG.Brix.EccSetup.Domain.Repositories;
using ITG.Brix.EccSetup.Infrastructure.Exceptions;
using ITG.Brix.EccSetup.Infrastructure.Providers;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ITG.Brix.EccSetup.Application.Cqs.Commands.Handlers
{
    public class CreateInformationCommandHandler : IRequestHandler<CreateInformationCommand, Result>
    {
        private readonly IInformationWriteRepository _informationWriteRepository;
        private readonly IIdentifierProvider _identifierProvider;
        private readonly IVersionProvider _versionProvider;

        public CreateInformationCommandHandler(IInformationWriteRepository informationWriteRepository,
                                               IIdentifierProvider identifierProvider,
                                               IVersionProvider versionProvider)
        {
            _informationWriteRepository = informationWriteRepository ?? throw Error.ArgumentNull(nameof(informationWriteRepository));
            _identifierProvider = identifierProvider ?? throw Error.ArgumentNull(nameof(identifierProvider));
            _versionProvider = versionProvider ?? throw Error.ArgumentNull(nameof(versionProvider));
        }

        public async Task<Result> Handle(CreateInformationCommand request, CancellationToken cancellationToken)
        {
            var id = _identifierProvider.Generate();

            var infoToCreate = new Information(id, request.Name, request.NameOnApplication, request.Description, request.Icon);
            request.Tags.ToList().ForEach(x => infoToCreate.AddTag(new Tag(x)));

            infoToCreate.Version = _versionProvider.Generate();

            Result result;

            try
            {
                await _informationWriteRepository.CreateAsync(infoToCreate);
                result = Result.Ok(id, infoToCreate.Version);
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
                result = Result.Fail(CustomFailures.CreateInformationFailure);
            }

            return result;
        }
    }
}
