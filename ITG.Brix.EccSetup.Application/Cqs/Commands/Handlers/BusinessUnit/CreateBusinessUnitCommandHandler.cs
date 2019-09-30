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
    public class CreateBusinessUnitCommandHandler : IRequestHandler<CreateBusinessUnitCommand, Result>
    {
        private readonly IBusinessUnitWriteRepository _businessUnitWriteRepository;
        private readonly IIdentifierProvider _identifierProvider;
        private readonly IVersionProvider _versionProvider;

        public CreateBusinessUnitCommandHandler(IBusinessUnitWriteRepository businessUnitWriteRepository,
                                                IIdentifierProvider identifierProvider,
                                                IVersionProvider versionProvider)
        {
            _businessUnitWriteRepository = businessUnitWriteRepository ?? throw Error.ArgumentNull(nameof(businessUnitWriteRepository));
            _identifierProvider = identifierProvider ?? throw Error.ArgumentNull(nameof(identifierProvider));
            _versionProvider = versionProvider ?? throw Error.ArgumentNull(nameof(versionProvider));
        }

        public async Task<Result> Handle(CreateBusinessUnitCommand request, CancellationToken cancellationToken)
        {
            var id = _identifierProvider.Generate();

            var businessUnitToCreate = new BusinessUnit(id, request.Name);

            businessUnitToCreate.Version = _versionProvider.Generate();

            Result result;

            try
            {
                await _businessUnitWriteRepository.CreateAsync(businessUnitToCreate);
                result = Result.Ok(id, businessUnitToCreate.Version);
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
                result = Result.Fail(CustomFailures.CreateBusinessUnitFailure);
            }

            return result;
        }
    }
}
