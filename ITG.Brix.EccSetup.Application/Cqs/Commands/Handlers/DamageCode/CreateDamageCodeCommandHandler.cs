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
    public class CreateDamageCodeCommandHandler : IRequestHandler<CreateDamageCodeCommand, Result>
    {
        private readonly IDamageCodeWriteRepository _damageCodeWriteRepository;
        private readonly IIdentifierProvider _identifierProvider;
        private readonly IVersionProvider _versionProvider;

        public CreateDamageCodeCommandHandler(IDamageCodeWriteRepository damageCodeWriteRepository, IIdentifierProvider identifierProvider, IVersionProvider versionProvider)
        {
            _damageCodeWriteRepository = damageCodeWriteRepository ?? throw new ArgumentNullException(nameof(damageCodeWriteRepository));
            _identifierProvider = identifierProvider ?? throw new ArgumentNullException(nameof(identifierProvider));
            _versionProvider = versionProvider ?? throw new ArgumentNullException(nameof(versionProvider));
        }

        public async Task<Result> Handle(CreateDamageCodeCommand request, CancellationToken cancellationToken)
        {
            Result result;
            var id = _identifierProvider.Generate();

            var damageCodeToCreate = new DamageCode(id, request.Code, request.Name, bool.Parse(request.DamagedQuantityRequired), request.Source);

            damageCodeToCreate.Version = _versionProvider.Generate();

            try
            {
                await _damageCodeWriteRepository.CreateAsync(damageCodeToCreate);
                result = Result.Ok(id, damageCodeToCreate.Version);
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
                result = Result.Fail(CustomFailures.CreateDamageCodeFailure);
            }

            return result;
        }
    }
}
