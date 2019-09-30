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
    public class CreateIconCommandHandler : IRequestHandler<CreateIconCommand, Result>
    {
        private readonly IIconWriteRepository _iconWriteRepository;
        private readonly IIdentifierProvider _identifierProvider;
        private readonly IVersionProvider _versionProvider;

        public CreateIconCommandHandler(IIconWriteRepository iconWriteRepository,
                                        IIdentifierProvider identifierProvider,
                                        IVersionProvider versionProvider)
        {
            _iconWriteRepository = iconWriteRepository ?? throw Error.ArgumentNull(nameof(iconWriteRepository));
            _identifierProvider = identifierProvider ?? throw Error.ArgumentNull(nameof(identifierProvider));
            _versionProvider = versionProvider ?? throw Error.ArgumentNull(nameof(versionProvider));
        }

        public async Task<Result> Handle(CreateIconCommand request, CancellationToken cancellationToken)
        {
            var id = _identifierProvider.Generate();
            var iconToCreate = new Icon(id, request.Name, request.DataPath);

            iconToCreate.Version = _versionProvider.Generate();

            Result result;

            try
            {
                await _iconWriteRepository.CreateAsync(iconToCreate);
                result = Result.Ok(id, iconToCreate.Version);
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
                result = Result.Fail(CustomFailures.CreateIconFailure);
            }

            return result;
        }
    }
}
