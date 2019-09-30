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
    public class ListSiteQueryHandler : IRequestHandler<ListSiteQuery, Result>
    {
        private readonly IMapper _mapper;
        private readonly ISiteReadRepository _siteReadRepository;
        private readonly ISiteOdataProvider _siteOdataProvider;

        public ListSiteQueryHandler(IMapper mapper, ISiteReadRepository siteReadRepository, ISiteOdataProvider siteOdataProvider)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _siteReadRepository = siteReadRepository ?? throw new ArgumentNullException(nameof(siteReadRepository));
            _siteOdataProvider = siteOdataProvider ?? throw new ArgumentNullException(nameof(siteOdataProvider));
        }

        public async Task<Result> Handle(ListSiteQuery request, CancellationToken cancellationToken)
        {
            Result result;
            try
            {
                Expression<Func<Site, bool>> filter = _siteOdataProvider.GetFilterPredicate(request.Filter);
                int? skip = request.Skip.ToNullableInt();
                int? top = request.Top.ToNullableInt();

                var siteDomains = await _siteReadRepository.ListAsync(filter, skip, top);
                var siteModels = _mapper.Map<IEnumerable<SiteModel>>(siteDomains);
                var count = siteModels.Count();
                var siteModel = new SitesModel { Value = siteModels, Count = count, NextLink = null };

                result = Result.Ok(siteModel);
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
                result = Result.Fail(CustomFailures.ListSiteFailure);
            }
            return result;
        }
    }
}
