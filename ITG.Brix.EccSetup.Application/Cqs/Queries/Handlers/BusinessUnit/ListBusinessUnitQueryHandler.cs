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
    public class ListBusinessUnitQueryHandler : IRequestHandler<ListBusinessUnitQuery, Result>
    {
        private readonly IMapper _mapper;
        private readonly IBusinessUnitReadRepository _businessUnitReadRepository;
        private readonly IBusinessUnitOdataProvider _businessUnitOdataProvider;

        public ListBusinessUnitQueryHandler(IMapper mapper,
                                            IBusinessUnitReadRepository businessUnitReadRepository,
                                            IBusinessUnitOdataProvider businessUnitOdataProvider)
        {
            _mapper = mapper ?? throw Error.ArgumentNull(nameof(mapper));
            _businessUnitReadRepository = businessUnitReadRepository ?? throw Error.ArgumentNull(nameof(businessUnitReadRepository));
            _businessUnitOdataProvider = businessUnitOdataProvider ?? throw Error.ArgumentNull(nameof(businessUnitOdataProvider));
        }

        public async Task<Result> Handle(ListBusinessUnitQuery request, CancellationToken cancellationToken)
        {
            Result result;
            try
            {
                Expression<Func<BusinessUnit, bool>> filter = _businessUnitOdataProvider.GetFilterPredicate(request.Filter);
                int? skip = request.Skip.ToNullableInt();
                int? top = request.Top.ToNullableInt();

                var businessUnitDomains = await _businessUnitReadRepository.ListAsync(filter, skip, top);
                var businessUnitModels = _mapper.Map<IEnumerable<BusinessUnitModel>>(businessUnitDomains);
                var count = businessUnitModels.Count();
                var sourcesModel = new BusinessUnitsModel { Value = businessUnitModels, Count = count, NextLink = null };

                result = Result.Ok(sourcesModel);
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
                result = Result.Fail(CustomFailures.ListBusinessUnitFailure);
            }
            return result;
        }
    }
}
