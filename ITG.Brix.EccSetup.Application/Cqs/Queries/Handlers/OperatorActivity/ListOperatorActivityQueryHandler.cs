using AutoMapper;
using ITG.Brix.EccSetup.Application.Bases;
using ITG.Brix.EccSetup.Application.Cqs.Queries.Definitions.OperatorActivity;
using ITG.Brix.EccSetup.Application.Cqs.Queries.Models;
using ITG.Brix.EccSetup.Application.Extensions;
using ITG.Brix.EccSetup.Application.Resources;
using ITG.Brix.EccSetup.Domain.Repositories.OperatorActivities;
using ITG.Brix.EccSetup.Infrastructure.Exceptions;
using ITG.Brix.EccSetup.Infrastructure.Providers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ITG.Brix.EccSetup.Application.Cqs.Queries.Handlers
{
    public class ListOperatorActivityQueryHandler : IRequestHandler<ListOperatorActivityQuery, Result>
    {
        private readonly IOperatorActivityReadRepository _operatorActivityReadRepository;
        private readonly IOperatorActivityOdataProvider _operatorActivityOdataProvider;
        private readonly IMapper _mapper;

        public ListOperatorActivityQueryHandler(IOperatorActivityReadRepository operatorActivityReadRepository,
                                                IOperatorActivityOdataProvider operatorActivityOdataProvider,
                                                IMapper mapper)
        {
            _operatorActivityReadRepository = operatorActivityReadRepository ?? throw new ArgumentNullException(nameof(operatorActivityReadRepository));
            _operatorActivityOdataProvider = operatorActivityOdataProvider ?? throw new ArgumentNullException(nameof(operatorActivityOdataProvider));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Result> Handle(ListOperatorActivityQuery request, CancellationToken cancellationToken)
        {
            Result result;

            try
            {                
                int? skip = request.Skip.ToNullableInt();
                int? top = request.Top.ToNullableInt();

                var activitiesDomain = await _operatorActivityReadRepository.ListAsync(request.Filter, skip, top);
                var activitiesModel = _mapper.Map<IEnumerable<OperatorActivityModel>>(activitiesDomain);
                var count = activitiesModel.Count();
                var operatorActivitiesModel = new OperatorActivitiesModel { Value = activitiesModel, Count = count, NextLink = null };

                result = Result.Ok(operatorActivitiesModel);
            }
            catch (FilterODataException)
            {
                result = Result.Fail(new List<Failure>() {
                    new HandlerFault(){
                        Code = HandlerFaultCode.InvalidQueryFilter.Name,
                        Message=  HandlerFailures.InvalidQueryFilter,
                        Target = "$filter"
                    }
                });
            }
            catch
            {
                result = Result.Fail(CustomFailures.ListOperatorActivitiesFailure);
            }

            return result;
        }
    }
}
