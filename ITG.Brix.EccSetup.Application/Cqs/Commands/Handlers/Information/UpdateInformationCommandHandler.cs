using ITG.Brix.EccSetup.Application.Bases;
using ITG.Brix.EccSetup.Application.Cqs.Commands.Definitions;
using ITG.Brix.EccSetup.Application.Exceptions;
using ITG.Brix.EccSetup.Application.Internal;
using ITG.Brix.EccSetup.Application.Resources;
using ITG.Brix.EccSetup.Domain;
using ITG.Brix.EccSetup.Domain.Repositories;
using ITG.Brix.EccSetup.Infrastructure.Exceptions;
using ITG.Brix.EccSetup.Infrastructure.Providers;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ITG.Brix.EccSetup.Application.Cqs.Commands.Handlers
{
    public class UpdateInformationCommandHandler : IRequestHandler<UpdateInformationCommand, Result>
    {
        private readonly IInformationWriteRepository _informationWriteRepository;
        private readonly IInformationReadRepository _informationReadRepository;
        private readonly IVersionProvider _versionProvider;

        public UpdateInformationCommandHandler(IInformationWriteRepository informationWriteRepository,
                                               IInformationReadRepository informationReadRepository,
                                               IVersionProvider versionProvider)
        {
            _informationWriteRepository = informationWriteRepository ?? throw Error.ArgumentNull(nameof(informationWriteRepository));
            _informationReadRepository = informationReadRepository ?? throw Error.ArgumentNull(nameof(informationReadRepository));
            _versionProvider = versionProvider ?? throw Error.ArgumentNull(nameof(versionProvider));
        }

        public async Task<Result> Handle(UpdateInformationCommand request, CancellationToken cancellationToken)
        {
            Result result;
            try
            {
                var information = await _informationReadRepository.GetAsync(request.Id);
                if (information.Version != request.Version)
                {
                    throw new CommandVersionException();
                }


                var updatedInformation = new Information(request.Id, request.Name, request.NameOnApplication, request.Description, request.Icon);
                request.Tags.ToList().ForEach(x => updatedInformation.AddTag(new Tag(x)));

                updatedInformation.Version = _versionProvider.Generate();
                await _informationWriteRepository.UpdateAsync(updatedInformation);
                result = Result.Ok(updatedInformation.Version);

            }
            catch (EntityNotFoundDbException)
            {
                result = Result.Fail(new System.Collections.Generic.List<Failure>() {
                                        new HandlerFault(){
                                            Code = HandlerFaultCode.NotFound.Name,
                                            Message = string.Format(HandlerFailures.NotFound, "Information"),
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
                result = Result.Fail(CustomFailures.UpdateInformationFailure);
            }

            return result;
        }
    }
}
