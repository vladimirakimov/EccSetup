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
    public class CreateProductionSiteCommandHandler : IRequestHandler<CreateProductionSiteCommand, Result>
    {
        private readonly IProductionSiteWriteRepository _productionSiteWriteRepository;
        private readonly IIdentifierProvider _identifierProvider;
        private readonly IVersionProvider _versionProvider;

        public CreateProductionSiteCommandHandler(IProductionSiteWriteRepository productionSiteWriteRepository, IIdentifierProvider identifierProvider, IVersionProvider versionProvider)
        {
            _productionSiteWriteRepository = productionSiteWriteRepository ?? throw new ArgumentNullException(nameof(productionSiteWriteRepository));
            _identifierProvider = identifierProvider ?? throw new ArgumentNullException(nameof(identifierProvider));
            _versionProvider = versionProvider ?? throw new ArgumentNullException(nameof(versionProvider));
        }

        public async Task<Result> Handle(CreateProductionSiteCommand request, CancellationToken cancellationToken)
        {
            Result result;
            var id = _identifierProvider.Generate();

            var productionSiteToCreate = new ProductionSite(id, request.Code, request.Name, request.Source);

            productionSiteToCreate.Version = _versionProvider.Generate();

            try
            {
                await _productionSiteWriteRepository.CreateAsync(productionSiteToCreate);
                result = Result.Ok(id, productionSiteToCreate.Version);
            }
            catch (UniqueKeyException)
            {
                result = Result.Fail(new System.Collections.Generic.List<Failure>() {
                                        new HandlerFault(){
                                            Code = HandlerFaultCode.Conflict.Name,
                                            Message = HandlerFailures.CodeSourceConflict,
                                            Target = "code-source"}
                                        }
                );
            }
            catch
            {
                result = Result.Fail(CustomFailures.CreateProductionSiteFailure);
            }

            return result;
        }
    }
}
