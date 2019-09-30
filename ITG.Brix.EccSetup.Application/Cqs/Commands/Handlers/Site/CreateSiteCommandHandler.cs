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
    public class CreateSiteCommandHandler : IRequestHandler<CreateSiteCommand, Result>
    {
        private readonly ISiteWriteRepository _siteWriteRepository;
        private readonly IIdentifierProvider _identifierProvider;
        private readonly IVersionProvider _versionProvider;

        public CreateSiteCommandHandler(ISiteWriteRepository siteWriteRepository, IIdentifierProvider identifierProvider, IVersionProvider versionProvider)
        {
            _siteWriteRepository = siteWriteRepository ?? throw new ArgumentNullException(nameof(siteWriteRepository));
            _identifierProvider = identifierProvider ?? throw new ArgumentNullException(nameof(identifierProvider));
            _versionProvider = versionProvider ?? throw new ArgumentNullException(nameof(versionProvider));
        }

        public async Task<Result> Handle(CreateSiteCommand request, CancellationToken cancellationToken)
        {
            Result result;
            var id = _identifierProvider.Generate();

            var siteToCreate = new Site(id, request.Code, request.Name, request.Source);

            siteToCreate.Version = _versionProvider.Generate();

            try
            {
                await _siteWriteRepository.CreateAsync(siteToCreate);
                result = Result.Ok(id, siteToCreate.Version);
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
                result = Result.Fail(CustomFailures.CreateSiteFailure);
            }

            return result;
        }
    }
}
