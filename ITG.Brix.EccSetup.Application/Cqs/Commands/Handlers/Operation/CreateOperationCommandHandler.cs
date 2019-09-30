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
    public class CreateOperationCommandHandler : IRequestHandler<CreateOperationCommand, Result>
    {
        private readonly IOperationWriteRepository _operationWriteRepository;
        private readonly IIdentifierProvider _identifierProvider;
        private readonly IVersionProvider _versionProvider;

        public CreateOperationCommandHandler(IOperationWriteRepository operationWriteRepository,
                                             IIdentifierProvider identifierProvider,
                                             IVersionProvider versionProvider)
        {
            _operationWriteRepository = operationWriteRepository ?? throw Error.ArgumentNull(nameof(operationWriteRepository));
            _identifierProvider = identifierProvider ?? throw Error.ArgumentNull(nameof(identifierProvider));
            _versionProvider = versionProvider ?? throw Error.ArgumentNull(nameof(versionProvider));
        }

        public async Task<Result> Handle(CreateOperationCommand request, CancellationToken cancellationToken)
        {
            var id = _identifierProvider.Generate();

            var operationToCreate = new Operation(id, request.Name);

            if (!string.IsNullOrWhiteSpace(request.Description))
            {
                operationToCreate.SetDescription(request.Description);
            }
            if (request.Icon != null)
            {
                var coloredIcon = new ColoredIcon(request.Icon.IconId, request.Icon.FillColor);
                operationToCreate.SetIcon(coloredIcon);
            }
            foreach (var tag in request.Tags)
            {
                operationToCreate.AddTag(new Tag(tag));
            }

            operationToCreate.Version = _versionProvider.Generate();


            Result result;

            try
            {
                await _operationWriteRepository.CreateAsync(operationToCreate);
                result = Result.Ok(id, operationToCreate.Version);
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
                result = Result.Fail(CustomFailures.CreateOperationFailure);
            }

            return result;
        }
    }
}
