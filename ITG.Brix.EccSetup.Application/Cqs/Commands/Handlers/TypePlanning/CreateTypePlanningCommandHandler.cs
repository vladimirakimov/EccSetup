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
    public class CreateTypePlanningCommandHandler : IRequestHandler<CreateTypePlanningCommand, Result>
    {
        private readonly ITypePlanningWriteRepository _typePlanningWriteRepository;
        private readonly IIdentifierProvider _identifierProvider;
        private readonly IVersionProvider _versionProvider;

        public CreateTypePlanningCommandHandler(ITypePlanningWriteRepository typePlanningWriteRepository, IIdentifierProvider identifierProvider, IVersionProvider versionProvider)
        {
            _typePlanningWriteRepository = typePlanningWriteRepository ?? throw new ArgumentNullException(nameof(typePlanningWriteRepository));
            _identifierProvider = identifierProvider ?? throw new ArgumentNullException(nameof(identifierProvider));
            _versionProvider = versionProvider ?? throw new ArgumentNullException(nameof(versionProvider));
        }

        public async Task<Result> Handle(CreateTypePlanningCommand request, CancellationToken cancellationToken)
        {

            Result result;
            var id = _identifierProvider.Generate();

            var typePlanningToCreate = new TypePlanning(id, request.Code, request.Name, request.Source);

            typePlanningToCreate.Version = _versionProvider.Generate();

            try
            {
                await _typePlanningWriteRepository.CreateAsync(typePlanningToCreate);
                result = Result.Ok(id, typePlanningToCreate.Version);
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
                result = Result.Fail(CustomFailures.CreateTypePlanningFailure);
            }

            return result;
        }
    }
}
