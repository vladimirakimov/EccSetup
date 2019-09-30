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
    public class GetOperationQueryHandler : IRequestHandler<GetOperationQuery, Result>
    {
        private readonly IMapper _mapper;
        private readonly IOperationReadRepository _operationReadRepository;
        private readonly IIconReadRepository _iconReadRepository;

        public GetOperationQueryHandler(IMapper mapper,
                                        IOperationReadRepository operationReadRepository,
                                        IIconReadRepository iconReadRepository)
        {
            _mapper = mapper ?? throw Error.ArgumentNull(nameof(mapper));
            _operationReadRepository = operationReadRepository ?? throw Error.ArgumentNull(nameof(operationReadRepository));
            _iconReadRepository = iconReadRepository ?? throw Error.ArgumentNull(nameof(iconReadRepository));
        }

        public async Task<Result> Handle(GetOperationQuery request, CancellationToken cancellationToken)
        {
            Result result;

            try
            {
                var operation = await _operationReadRepository.GetAsync(request.Id);

                var operationModel = _mapper.Map<OperationModel>(operation);
                if (operation.Icon != null)
                {
                    var icon = await _iconReadRepository.GetAsync(operation.Icon.IconId);

                    if (icon != null)
                    {
                        operationModel.Icon.DataPath = icon.DataPath;
                    }
                }

                result = Result.Ok(operationModel, operation.Version);
            }
            catch (EntityNotFoundDbException)
            {
                result = Result.Fail(new System.Collections.Generic.List<Failure>() {
                                        new HandlerFault(){
                                            Code = HandlerFaultCode.NotFound.Name,
                                            Message = string.Format(HandlerFailures.NotFound, "Operation"),
                                            Target = "id"}
                                        }
                );
            }
            catch
            {
                result = Result.Fail(CustomFailures.GetOperationFailure);
            }

            return result;
        }
    }
}
