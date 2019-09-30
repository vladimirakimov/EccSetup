using ITG.Brix.EccSetup.Application.Bases;
using ITG.Brix.EccSetup.Application.Cqs.Commands.Definitions;
using ITG.Brix.EccSetup.Application.Resources;
using ITG.Brix.EccSetup.Domain;
using ITG.Brix.EccSetup.Domain.Repositories;
using ITG.Brix.EccSetup.Infrastructure.Exceptions;
using ITG.Brix.EccSetup.Infrastructure.Providers;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ITG.Brix.EccSetup.Application.Cqs.Commands.Handlers
{
    public class CreateTransportTypeCommandHandler : IRequestHandler<CreateTransportTypeCommand, Result>
    {
        private readonly ITransportTypeWriteRepository _transportTypeWriteRepository;
        private readonly IIdentifierProvider _identifierProvider;
        private readonly IVersionProvider _versionProvider;

        public CreateTransportTypeCommandHandler(ITransportTypeWriteRepository transportTypeWriteRepository, IIdentifierProvider identifierProvider, IVersionProvider versionProvider)
        {
            _transportTypeWriteRepository = transportTypeWriteRepository ?? throw new ArgumentNullException(nameof(transportTypeWriteRepository));
            _identifierProvider = identifierProvider ?? throw new ArgumentNullException(nameof(identifierProvider));
            _versionProvider = versionProvider ?? throw new ArgumentNullException(nameof(versionProvider));
        }

        public async Task<Result> Handle(CreateTransportTypeCommand request, CancellationToken cancellationToken)
        {
            Result result;
            var id = _identifierProvider.Generate();

            var transportTypeToCreate = new TransportType(id, request.Code, request.Name, request.Source);

            transportTypeToCreate.Version = _versionProvider.Generate();

            try
            {
                await _transportTypeWriteRepository.CreateAsync(transportTypeToCreate);
                result = Result.Ok(id, transportTypeToCreate.Version);
            }
            catch (UniqueKeyException)
            {
                result = Result.Fail(new System.Collections.Generic.List<Failure>() {
                                        new HandlerFault(){
                                            Code = HandlerFaultCode.Conflict.Name,
                                            Message = HandlerFailures.CodeNameSourceConflict,
                                            Target = "code-name-source"}
                                        }
                );
            }
            catch
            {
                result = Result.Fail(CustomFailures.CreateTransportTypeFailure);
            }

            return result;
        }
    }
}
