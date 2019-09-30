using ITG.Brix.EccSetup.Application.Bases;
using ITG.Brix.EccSetup.Application.Cqs.Commands.Definitions;
using ITG.Brix.EccSetup.Application.Exceptions;
using ITG.Brix.EccSetup.Application.Internal;
using ITG.Brix.EccSetup.Application.Resources;
using ITG.Brix.EccSetup.Domain.Repositories;
using ITG.Brix.EccSetup.Infrastructure.Exceptions;
using ITG.Brix.EccSetup.Infrastructure.Providers;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ITG.Brix.EccSetup.Application.Cqs.Commands.Handlers
{
    public class UpdateBusinessUnitCommandHandler : IRequestHandler<UpdateBusinessUnitCommand, Result>
    {
        private readonly IBusinessUnitWriteRepository _businessUnitWriteRepository;
        private readonly IBusinessUnitReadRepository _businessUnitReadRepository;
        private readonly IVersionProvider _versionProvider;

        public UpdateBusinessUnitCommandHandler(IBusinessUnitWriteRepository businessUnitWriteRepository,
                                                IBusinessUnitReadRepository businessUnitReadRepository,
                                                IVersionProvider versionProvider)
        {
            _businessUnitWriteRepository = businessUnitWriteRepository ?? throw Error.ArgumentNull(nameof(businessUnitWriteRepository));
            _businessUnitReadRepository = businessUnitReadRepository ?? throw Error.ArgumentNull(nameof(businessUnitReadRepository));
            _versionProvider = versionProvider ?? throw Error.ArgumentNull(nameof(versionProvider));
        }

        public async Task<Result> Handle(UpdateBusinessUnitCommand request, CancellationToken cancellationToken)
        {
            Result result;

            try
            {
                var businessUnit = await _businessUnitReadRepository.GetAsync(request.Id);
                if (businessUnit.Version != request.Version)
                {
                    throw new CommandVersionException();
                }

                if (request.Name.HasValue)
                {
                    businessUnit.ChangeName(request.Name.Value);
                }

                businessUnit.Version = _versionProvider.Generate();
                await _businessUnitWriteRepository.UpdateAsync(businessUnit);
                result = Result.Ok(businessUnit.Version);

            }
            catch (EntityNotFoundDbException)
            {
                result = Result.Fail(new System.Collections.Generic.List<Failure>() {
                                        new HandlerFault(){
                                            Code = HandlerFaultCode.NotFound.Name,
                                            Message = string.Format(HandlerFailures.NotFound, "BusinessUnit"),
                                            Target = "id"}
                                        }
                );
            }
            catch (CommandVersionException)
            {
                result = Result.Fail(new System.Collections.Generic.List<Failure>() {
                                        new HandlerFault(){
                                            Code = HandlerFaultCode.NotMet.Name,
                                            Message = HandlerFailures.NotMet,
                                            Target = "version"}
                                        }
                );
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
                result = Result.Fail(CustomFailures.UpdateBusinessUnitFailure);
            }

            return result;
        }
    }
}
