using AutoMapper;
using ITG.Brix.EccSetup.Application.Bases;
using ITG.Brix.EccSetup.Application.Cqs.Queries.Definitions;
using ITG.Brix.EccSetup.Application.Cqs.Queries.Models;
using ITG.Brix.EccSetup.Application.Extensions;
using ITG.Brix.EccSetup.Application.Resources;
using ITG.Brix.EccSetup.Domain;
using ITG.Brix.EccSetup.Domain.Repositories;
using ITG.Brix.EccSetup.Infrastructure.DataAccess.ClassModels;
using ITG.Brix.EccSetup.Infrastructure.Exceptions;
using ITG.Brix.EccSetup.Infrastructure.Providers.Impl;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace ITG.Brix.EccSetup.Application.Cqs.Queries.Handlers
{
    public class ListLocationQueryHandler : IRequestHandler<ListLocationQuery, Result>
    {
        private readonly IMapper _mapper;
        private readonly ILocationReadRepository _locationReadRepository;
        private readonly ILocationOdataProvider _locationOdataProvider;

        public ListLocationQueryHandler(IMapper mapper, ILocationReadRepository locationReadRepository, ILocationOdataProvider locationOdataProvider)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _locationReadRepository = locationReadRepository ?? throw new ArgumentNullException(nameof(locationReadRepository));
            _locationOdataProvider = locationOdataProvider ?? throw new ArgumentNullException(nameof(locationOdataProvider));
        }

        public async Task<Result> Handle(ListLocationQuery request, CancellationToken cancellationToken)
        {
            Result result;
            try
            {
                int? skip = request.Skip.ToNullableInt();
                int? top = request.Top.ToNullableInt();

                var locationDomains = await _locationReadRepository.ListAsync(request.Filter, skip, top);
                var locationModels = _mapper.Map<IEnumerable<LocationModel>>(locationDomains);
                var count = locationModels.Count();
                var locationModel = new LocationsModel { Value = locationModels, Count = count, NextLink = null };

                result = Result.Ok(locationModel);
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
                result = Result.Fail(CustomFailures.ListLocationFailure);
            }
            return result;
        }
    }
}
