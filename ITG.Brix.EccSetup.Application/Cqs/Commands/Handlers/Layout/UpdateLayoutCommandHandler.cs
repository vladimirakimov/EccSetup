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
    public class UpdateLayoutCommandHandler : IRequestHandler<UpdateLayoutCommand, Result>
    {
        private readonly ILayoutWriteRepository _layoutWriteRepository;
        private readonly ILayoutReadRepository _layoutReadRepository;
        private readonly IVersionProvider _versionProvider;

        public UpdateLayoutCommandHandler(ILayoutWriteRepository layoutWriteRepository,
                                          ILayoutReadRepository layoutReadRepository,
                                          IVersionProvider versionProvider)
        {
            _layoutWriteRepository = layoutWriteRepository ?? throw Error.ArgumentNull(nameof(layoutWriteRepository));
            _layoutReadRepository = layoutReadRepository ?? throw Error.ArgumentNull(nameof(layoutReadRepository));
            _versionProvider = versionProvider ?? throw Error.ArgumentNull(nameof(versionProvider));
        }
        public async Task<Result> Handle(UpdateLayoutCommand request, CancellationToken cancellationToken)
        {
            Result result;
            try
            {
                var layout = await _layoutReadRepository.GetAsync(request.Id);
                if (layout.Version != request.Version)
                {
                    throw new CommandVersionException();
                }

                if (request.Name.HasValue)
                {
                    layout.ChangeName(request.Name.Value);
                }
                if (request.Description.HasValue)
                {
                    layout.SetDescription(request.Description.Value);
                }
                if (request.Image.HasValue)
                {
                    layout.SetImage(request.Image.Value);
                }
                if (request.Diagram.HasValue)
                {
                    layout.SetDiagram(request.Diagram.Value);
                }
                layout.Version = _versionProvider.Generate();
                await _layoutWriteRepository.UpdateAsync(layout);
                result = Result.Ok(layout.Version);

            }
            catch (EntityNotFoundDbException)
            {
                result = Result.Fail(new System.Collections.Generic.List<Failure>() {
                                        new HandlerFault(){
                                            Code = HandlerFaultCode.NotFound.Name,
                                            Message = string.Format(HandlerFailures.NotFound, "Layout"),
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
                result = Result.Fail(CustomFailures.UpdateLayoutFailure);
            }

            return result;
        }
    }
}
