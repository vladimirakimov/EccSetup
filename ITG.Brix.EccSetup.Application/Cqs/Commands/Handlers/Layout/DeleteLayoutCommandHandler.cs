using ITG.Brix.EccSetup.Application.Bases;
using ITG.Brix.EccSetup.Application.Cqs.Commands.Definitions;
using ITG.Brix.EccSetup.Application.Internal;
using ITG.Brix.EccSetup.Application.Resources;
using ITG.Brix.EccSetup.Domain.Repositories;
using ITG.Brix.EccSetup.Infrastructure.Exceptions;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ITG.Brix.EccSetup.Application.Cqs.Commands.Handlers
{
    public class DeleteLayoutCommandHandler : IRequestHandler<DeleteLayoutCommand, Result>
    {
        private readonly ILayoutWriteRepository _layoutWriteRepository;

        public DeleteLayoutCommandHandler(ILayoutWriteRepository layoutWriteRepository)
        {
            _layoutWriteRepository = layoutWriteRepository ?? throw Error.ArgumentNull(nameof(layoutWriteRepository));
        }

        public async Task<Result> Handle(DeleteLayoutCommand request, CancellationToken cancellationToken)
        {
            Result result;

            try
            {
                await _layoutWriteRepository.DeleteAsync(request.Id, request.Version);
                result = Result.Ok();
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
            catch (EntityVersionDbException)
            {
                result = Result.Fail(new System.Collections.Generic.List<Failure>() {
                                        new HandlerFault(){
                                            Code = HandlerFaultCode.NotMet.Name,
                                            Message = HandlerFailures.NotMet,
                                            Target = "version"}
                                        }
                );
            }
            catch
            {
                result = Result.Fail(CustomFailures.DeleteLayoutFailure);
            }

            return result;
        }
    }
}
