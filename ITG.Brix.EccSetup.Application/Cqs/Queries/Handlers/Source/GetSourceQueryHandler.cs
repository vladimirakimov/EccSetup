using AutoMapper;
using ITG.Brix.EccSetup.Application.Bases;
using ITG.Brix.EccSetup.Application.Cqs.Queries.Definitions;
using ITG.Brix.EccSetup.Application.Cqs.Queries.Models;
using ITG.Brix.EccSetup.Application.Internal;
using ITG.Brix.EccSetup.Application.Resources;
using ITG.Brix.EccSetup.Domain.Repositories;
using ITG.Brix.EccSetup.Infrastructure.Exceptions;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ITG.Brix.EccSetup.Application.Cqs.Queries.Handlers
{
    public class GetSourceQueryHandler : IRequestHandler<GetSourceQuery, Result>
    {
        private readonly IMapper _mapper;
        private readonly ISourceReadRepository _sourceReadRepository;

        public GetSourceQueryHandler(IMapper mapper,
                                     ISourceReadRepository sourceReadRepository)
        {
            _mapper = mapper ?? throw Error.ArgumentNull(nameof(mapper));
            _sourceReadRepository = sourceReadRepository ?? throw Error.ArgumentNull(nameof(sourceReadRepository));
        }

        public async Task<Result> Handle(GetSourceQuery request, CancellationToken cancellationToken)
        {
            Result result;

            try
            {
                var source = await _sourceReadRepository.GetAsync(request.Id);
                var sourceModel = _mapper.Map<SourceModel>(source);

                result = Result.Ok(sourceModel, source.Version);
            }
            catch (EntityNotFoundDbException)
            {
                result = Result.Fail(new System.Collections.Generic.List<Failure>() {
                                        new HandlerFault(){
                                            Code = HandlerFaultCode.NotFound.Name,
                                            Message = string.Format(HandlerFailures.NotFound, "Source"),
                                            Target = "id"}
                                        }
                );
            }
            catch
            {
                result = Result.Fail(CustomFailures.GetSourceFailure);
            }

            return result;
        }
    }
}
