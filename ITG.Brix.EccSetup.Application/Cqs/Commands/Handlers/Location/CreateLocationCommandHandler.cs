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
    public class CreateLocationCommandHandler : IRequestHandler<CreateLocationCommand, Result>
    {
        private readonly ILocationWriteRepository _locationWriteRepository;
        private readonly IIdentifierProvider _identifierProvider;
        private readonly IVersionProvider _versionProvider;

        public CreateLocationCommandHandler(ILocationWriteRepository locationWriteRepository, IIdentifierProvider identifierProvider, IVersionProvider versionProvider)
        {
            _locationWriteRepository = locationWriteRepository ?? throw new ArgumentNullException(nameof(locationWriteRepository));
            _identifierProvider = identifierProvider ?? throw new ArgumentNullException(nameof(identifierProvider));
            _versionProvider = versionProvider ?? throw new ArgumentNullException(nameof(versionProvider));
        }

        public async Task<Result> Handle(CreateLocationCommand request, CancellationToken cancellationToken)
        {
            Result result;
            var id = _identifierProvider.Generate();

            var locationToCreate = new Location(id, request.Source, request.Site, request.Warehouse, request.Gate, request.Row, request.Position, request.Type, bool.Parse(request.IsRack));

            locationToCreate.Version = _versionProvider.Generate();

            try
            {
                await _locationWriteRepository.CreateAsync(locationToCreate);
                result = Result.Ok(id, locationToCreate.Version);
            }
            catch (UniqueKeyException)
            {
                result = Result.Fail(new System.Collections.Generic.List<Failure>() {
                                        new HandlerFault(){
                                            Code = HandlerFaultCode.Conflict.Name,
                                            Message = HandlerFailures.SourceSiteWarehouseGateRowPositionConflict,
                                            Target = "source-site-warehouse-gate-row-position"}
                                        }
                );
            }
            catch
            {
                result = Result.Fail(CustomFailures.CreateLocationFailure);
            }

            return result;
        }
    }
}
