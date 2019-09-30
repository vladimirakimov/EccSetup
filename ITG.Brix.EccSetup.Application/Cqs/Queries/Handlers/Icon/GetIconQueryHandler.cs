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
    public class GetIconQueryHandler : IRequestHandler<GetIconQuery, Result>
    {
        private readonly IIconReadRepository _iconReadRepository;
        private readonly IMapper _mapper;

        public GetIconQueryHandler(IMapper mapper,
                                   IIconReadRepository iconReadRepository)
        {
            _mapper = mapper ?? throw Error.ArgumentNull(nameof(mapper));
            _iconReadRepository = iconReadRepository ?? throw Error.ArgumentNull(nameof(iconReadRepository));
        }

        public async Task<Result> Handle(GetIconQuery request, CancellationToken cancellationToken)
        {
            Result result;

            try
            {
                var icon = await _iconReadRepository.GetAsync(request.Id);
                var iconModel = _mapper.Map<IconModel>(icon);

                result = Result.Ok(iconModel, icon.Version);
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
            catch
            {
                result = Result.Fail(CustomFailures.GetIconFailure);
            }

            return result;
        }
    }
}
