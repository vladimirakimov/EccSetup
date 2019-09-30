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
    public class CreateSourceCommandHandler : IRequestHandler<CreateSourceCommand, Result>
    {
        private readonly ISourceWriteRepository _sourceWriteRepository;
        private readonly IIdentifierProvider _identifierProvider;
        private readonly IVersionProvider _versionProvider;

        public CreateSourceCommandHandler(ISourceWriteRepository sourceWriteRepository,
                                          IIdentifierProvider identifierProvider,
                                          IVersionProvider versionProvider)
        {
            _sourceWriteRepository = sourceWriteRepository ?? throw Error.ArgumentNull(nameof(sourceWriteRepository));
            _identifierProvider = identifierProvider ?? throw Error.ArgumentNull(nameof(identifierProvider));
            _versionProvider = versionProvider ?? throw Error.ArgumentNull(nameof(versionProvider));
        }

        public async Task<Result> Handle(CreateSourceCommand request, CancellationToken cancellationToken)
        {
            var id = _identifierProvider.Generate();
            var sourceToCreate = new Source(id, request.Name, request.Description);

            foreach (var sourceBusinessUnit in request.SourceBusinessUnits)
            {
                sourceToCreate.AddSourceBusinessUnit(new SourceBusinessUnit(sourceBusinessUnit));
            }
            sourceToCreate.Version = _versionProvider.Generate();

            Result result;

            try
            {
                await _sourceWriteRepository.CreateAsync(sourceToCreate);
                result = Result.Ok(id, sourceToCreate.Version);
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
                result = Result.Fail(CustomFailures.CreateSourceFailure);
            }

            return result;
        }
    }
}
