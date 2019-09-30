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
    public class ListProductionSiteQueryHandler : IRequestHandler<ListProductionSiteQuery, Result>
    {
        private readonly IMapper _mapper;
        private readonly IProductionSiteReadRepository _productionSiteReadRepository;
        private readonly IProductionSiteOdataProvider _productionSiteOdataProvider;

        public ListProductionSiteQueryHandler(IMapper mapper, IProductionSiteReadRepository productionSiteReadRepository, IProductionSiteOdataProvider productionSiteOdataProvider)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _productionSiteReadRepository = productionSiteReadRepository ?? throw new ArgumentNullException(nameof(productionSiteReadRepository));
            _productionSiteOdataProvider = productionSiteOdataProvider ?? throw new ArgumentNullException(nameof(productionSiteOdataProvider));
        }

        public async Task<Result> Handle(ListProductionSiteQuery request, CancellationToken cancellationToken)
        {
            Result result;
            try
            {
                Expression<Func<ProductionSite, bool>> filter = _productionSiteOdataProvider.GetFilterPredicate(request.Filter);
                int? skip = request.Skip.ToNullableInt();
                int? top = request.Top.ToNullableInt();

                var productionSiteDomains = await _productionSiteReadRepository.ListAsync(filter, skip, top);
                var productionSiteModels = _mapper.Map<IEnumerable<ProductionSiteModel>>(productionSiteDomains);
                var count = productionSiteModels.Count();
                var customerModel = new ProductionSitesModel { Value = productionSiteModels, Count = count, NextLink = null };

                result = Result.Ok(customerModel);
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
                result = Result.Fail(CustomFailures.ListProductionSiteFailure);
            }
            return result;
        }
    }
}
