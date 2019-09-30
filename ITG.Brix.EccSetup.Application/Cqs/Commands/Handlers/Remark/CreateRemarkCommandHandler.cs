using ITG.Brix.EccSetup.Application.Bases;
using ITG.Brix.EccSetup.Application.Cqs.Commands.Definitions;
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
    public class CreateRemarkCommandHandler : IRequestHandler<CreateRemarkCommand, Result>
    {
        private readonly IRemarkWriteRepository _remarkWriteRepository;
        private readonly IIdentifierProvider _identifierProvider;
        private readonly IVersionProvider _versionProvider;

        public CreateRemarkCommandHandler(IRemarkWriteRepository remarkWriteRepository,
                                          IIdentifierProvider identifierProvider,
                                          IVersionProvider versionProvider)
        {
            _remarkWriteRepository = remarkWriteRepository ?? throw Error.ArgumentNull(nameof(remarkWriteRepository));
            _identifierProvider = identifierProvider ?? throw Error.ArgumentNull(nameof(identifierProvider));
            _versionProvider = versionProvider ?? throw Error.ArgumentNull(nameof(versionProvider));
        }

        public async Task<Result> Handle(CreateRemarkCommand request, CancellationToken cancellationToken)
        {
            var id = _identifierProvider.Generate();

            var remarkToCreate = new Remark(id, request.Name, request.NameOnApplication, request.Description, new RemarkIcon(request.Icon));
            request.Tags.ToList().ForEach(x => remarkToCreate.AddTag(new Tag(x)));
            request.DefaultRemarks.ToList().ForEach(x => remarkToCreate.AddDefaultRemark(new DefaultRemark(x)));

            remarkToCreate.Version = _versionProvider.Generate();

            Result result;

            try
            {
                await _remarkWriteRepository.CreateAsync(remarkToCreate);
                result = Result.Ok(id, remarkToCreate.Version);
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
                result = Result.Fail(CustomFailures.CreateRemarkFailure);
            }

            return result;
        }
    }
}
