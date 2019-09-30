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
    public class CreateLayoutCommandHandler : IRequestHandler<CreateLayoutCommand, Result>
    {
        private readonly ILayoutWriteRepository _layoutWriteRepository;
        private readonly IIdentifierProvider _identifierProvider;
        private readonly IVersionProvider _versionProvider;

        public CreateLayoutCommandHandler(ILayoutWriteRepository layoutWriteRepository,
                                             IIdentifierProvider identifierProvider,
                                             IVersionProvider versionProvider)
        {
            _layoutWriteRepository = layoutWriteRepository ?? throw Error.ArgumentNull(nameof(layoutWriteRepository));
            _identifierProvider = identifierProvider ?? throw Error.ArgumentNull(nameof(identifierProvider));
            _versionProvider = versionProvider ?? throw Error.ArgumentNull(nameof(versionProvider));
        }
        public async Task<Result> Handle(CreateLayoutCommand request, CancellationToken cancellationToken)
        {
            var id = _identifierProvider.Generate();
            var layoutToCreate = new Layout(id, request.Name);
            if (!string.IsNullOrWhiteSpace(request.Description))
            {
                layoutToCreate.SetDescription(request.Description);
            }
            if (!string.IsNullOrWhiteSpace(request.Image))
            {
                layoutToCreate.SetImage(request.Image);
            }
            layoutToCreate.Version = _versionProvider.Generate();

            Result result;

            try
            {
                await _layoutWriteRepository.CreateAsync(layoutToCreate);
                result = Result.Ok(id, layoutToCreate.Version);
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
                result = Result.Fail(CustomFailures.CreateLayoutFailure);
            }

            return result;
        }
    }
}
