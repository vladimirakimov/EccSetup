using AutoMapper;
using ITG.Brix.EccSetup.Application.Bases;
using ITG.Brix.EccSetup.Application.Cqs.Queries.Definitions;
using ITG.Brix.EccSetup.Application.Cqs.Queries.Models;
using ITG.Brix.EccSetup.Application.Extensions;
using ITG.Brix.EccSetup.Application.Internal;
using ITG.Brix.EccSetup.Application.Resources;
using ITG.Brix.EccSetup.Domain;
using ITG.Brix.EccSetup.Domain.Repositories;
using ITG.Brix.EccSetup.Infrastructure.Exceptions;
using ITG.Brix.EccSetup.Infrastructure.Providers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace ITG.Brix.EccSetup.Application.Cqs.Queries.Handlers
{
    public class ListOperationQueryHandler : IRequestHandler<ListOperationQuery, Result>
    {
        private readonly IMapper _mapper;
        private readonly IOperationReadRepository _operationReadRepository;
        private readonly IIconReadRepository _iconReadRepository;
        private readonly IOperationOdataProvider _operationOdataProvider;

        public ListOperationQueryHandler(IMapper mapper,
                                         IOperationReadRepository operationReadRepository,
                                         IIconReadRepository iconReadRepository,
                                         IOperationOdataProvider operationOdataProvider)
        {
            _mapper = mapper ?? throw Error.ArgumentNull(nameof(mapper));
            _operationReadRepository = operationReadRepository ?? throw Error.ArgumentNull(nameof(operationReadRepository));
            _iconReadRepository = iconReadRepository ?? throw Error.ArgumentNull(nameof(iconReadRepository));
            _operationOdataProvider = operationOdataProvider ?? throw Error.ArgumentNull(nameof(operationOdataProvider));
        }

        public async Task<Result> Handle(ListOperationQuery request, CancellationToken cancellationToken)
        {
            Result result;
            try
            {
                Expression<Func<Operation, bool>> filter = _operationOdataProvider.GetFilterPredicate(request.Filter);
                int? skip = request.Skip.ToNullableInt();
                int? top = request.Top.ToNullableInt();

                var operationDomains = await _operationReadRepository.ListAsync(filter, skip, top);
                var operationModels = _mapper.Map<IEnumerable<OperationModel>>(operationDomains);
                foreach (var operationModel in operationModels)
                {
                    if (operationModel.Icon != null)
                    {
                        var icon = await _iconReadRepository.GetAsync(operationModel.Icon.IconId);
                        if (icon != null)
                        {
                            operationModel.Icon.DataPath = icon.DataPath;
                        }
                    }
                }
                var count = operationModels.Count();
                var operationsModel = new OperationsModel { Value = operationModels, Count = count, NextLink = null };

                result = Result.Ok(operationsModel);
            }
            catch (FilterODataException)
            {
                result = Result.Fail(new System.Collections.Generic.List<Failure>() {
                                        new HandlerFault(){
                                            Code = HandlerFaultCode.InvalidQueryFilter.Name,
                                            Message = HandlerFailures.InvalidQueryFilter,
                                            Target = "$filter"}
                                        }
                );
            }
            catch
            {
                result = Result.Fail(CustomFailures.ListOperationFailure);
            }
            return result;
        }
    }
}
