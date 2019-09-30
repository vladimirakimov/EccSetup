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
    public class CreateOperationalDepartmentCommandHandler : IRequestHandler<CreateOperationalDepartmentCommand, Result>
    {
        private readonly IOperationalDepartmentWriteRepository _operationalDepartmentWriteRepository;
        private readonly IIdentifierProvider _identifierProvider;
        private readonly IVersionProvider _versionProvider;

        public CreateOperationalDepartmentCommandHandler(IOperationalDepartmentWriteRepository operationalDepartmentWriteRepository, IIdentifierProvider identifierProvider, IVersionProvider versionProvider)
        {
            _operationalDepartmentWriteRepository = operationalDepartmentWriteRepository ?? throw new ArgumentNullException(nameof(operationalDepartmentWriteRepository));
            _identifierProvider = identifierProvider ?? throw new ArgumentNullException(nameof(identifierProvider));
            _versionProvider = versionProvider ?? throw new ArgumentNullException(nameof(versionProvider));
        }

        public async Task<Result> Handle(CreateOperationalDepartmentCommand request, CancellationToken cancellationToken)
        {
            Result result;
            var id = _identifierProvider.Generate();

            var _operationalDepartmentToCreate = new OperationalDepartment(id, request.Code, request.Name, request.Site, request.Source);

            _operationalDepartmentToCreate.Version = _versionProvider.Generate();

            try
            {
                await _operationalDepartmentWriteRepository.CreateAsync(_operationalDepartmentToCreate);
                result = Result.Ok(id, _operationalDepartmentToCreate.Version);
            }
            catch (UniqueKeyException)
            {
                result = Result.Fail(new System.Collections.Generic.List<Failure>() {
                                        new HandlerFault(){
                                            Code = HandlerFaultCode.Conflict.Name,
                                            Message = HandlerFailures.CodeSourceSiteConflict,
                                            Target = "code-source-site"}
                                        }
                );
            }
            catch
            {
                result = Result.Fail(CustomFailures.CreateOperationalDepartmentFailure);
            }

            return result;
        }
    }
}
