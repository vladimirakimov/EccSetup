using AutoMapper;
using ITG.Brix.EccSetup.Application.Bases;
using ITG.Brix.EccSetup.Application.Cqs.Queries.Definitions;
using ITG.Brix.EccSetup.Application.Cqs.Queries.Models;
using ITG.Brix.EccSetup.Application.Extensions;
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
    public class ListTypePlanningQueryHandler : IRequestHandler<ListTypePlanningQuery, Result>
    {
        private readonly IMapper _mapper;
        private readonly ITypePlanningReadRepository _typePlanningReadRepository;
        private readonly ITypePlanningOdataProvider _typePlanningOdataProvider;

        public ListTypePlanningQueryHandler(IMapper mapper, ITypePlanningReadRepository typePlanningReadRepository, ITypePlanningOdataProvider typePlanningOdataProvider)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _typePlanningReadRepository = typePlanningReadRepository ?? throw new ArgumentNullException(nameof(typePlanningReadRepository));
            _typePlanningOdataProvider = typePlanningOdataProvider ?? throw new ArgumentNullException(nameof(typePlanningOdataProvider));
        }

        public async Task<Result> Handle(ListTypePlanningQuery request, CancellationToken cancellationToken)
        {
            Result result;
            try
            {
                Expression<Func<TypePlanning, bool>> filter = _typePlanningOdataProvider.GetFilterPredicate(request.Filter);
                int? skip = request.Skip.ToNullableInt();
                int? top = request.Top.ToNullableInt();

                var typePlanningDomains = await _typePlanningReadRepository.ListAsync(filter, skip, top);
                var typePlanningModels = _mapper.Map<IEnumerable<TypePlanningModel>>(typePlanningDomains);
                var count = typePlanningModels.Count();
                var typePlanningModel = new TypePlanningsModel { Value = typePlanningModels, Count = count, NextLink = null };

                result = Result.Ok(typePlanningModel);
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
                result = Result.Fail(CustomFailures.ListTypePlanningFailure);
            }
            return result;
        }
    }
}
