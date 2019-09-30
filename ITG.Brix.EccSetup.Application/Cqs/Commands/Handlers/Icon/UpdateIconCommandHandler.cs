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
    public class UpdateIconCommandHandler : IRequestHandler<UpdateIconCommand, Result>
    {
        private readonly IIconWriteRepository _iconWriteRepository;
        private readonly IIconReadRepository _iconReadRepository;
        private readonly IVersionProvider _versionProvider;

        public UpdateIconCommandHandler(IIconWriteRepository iconWriteRepository,
                                        IIconReadRepository iconReadRepository,
                                        IVersionProvider versionProvider)
        {
            _iconWriteRepository = iconWriteRepository ?? throw Error.ArgumentNull(nameof(iconWriteRepository));
            _iconReadRepository = iconReadRepository ?? throw Error.ArgumentNull(nameof(iconReadRepository));
            _versionProvider = versionProvider ?? throw Error.ArgumentNull(nameof(versionProvider));
        }

        public async Task<Result> Handle(UpdateIconCommand request, CancellationToken cancellationToken)
        {
            Result result;
            try
            {
                var icon = await _iconReadRepository.GetAsync(request.Id);
                if (icon.Version != request.Version)
                {
                    throw new CommandVersionException();
                }

                if (request.Name.HasValue)
                {
                    icon.ChangeName(request.Name.Value);
                }
                if (request.DataPath.HasValue)
                {
                    icon.ChangeDataPath(request.DataPath.Value);
                }

                icon.Version = _versionProvider.Generate();
                await _iconWriteRepository.UpdateAsync(icon);
                result = Result.Ok(icon.Version);

            }
            catch (EntityNotFoundDbException)
            {
                result = Result.Fail(new System.Collections.Generic.List<Failure>() {
                                        new HandlerFault(){
                                            Code = HandlerFaultCode.NotFound.Name,
                                            Message = string.Format(HandlerFailures.NotFound, "Icon"),
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
                result = Result.Fail(CustomFailures.UpdateIconFailure);
            }

            return result;
        }
    }
}
