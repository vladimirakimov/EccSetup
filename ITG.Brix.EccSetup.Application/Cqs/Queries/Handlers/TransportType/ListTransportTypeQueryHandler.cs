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
    public class ListTransportTypeQueryHandler : IRequestHandler<ListTransportTypeQuery, Result>
    {
        private readonly IMapper _mapper;
        private readonly ITransportTypeReadRepository _transportTypeReadRepository;
        private readonly ITransportTypeOdataProvider _transportTypeOdataProvider;

        public ListTransportTypeQueryHandler(IMapper mapper, ITransportTypeReadRepository transportTypeReadRepository, ITransportTypeOdataProvider transportTypeOdataProvider)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _transportTypeReadRepository = transportTypeReadRepository ?? throw new ArgumentNullException(nameof(transportTypeReadRepository));
            _transportTypeOdataProvider = transportTypeOdataProvider ?? throw new ArgumentNullException(nameof(transportTypeOdataProvider));
        }

        public async Task<Result> Handle(ListTransportTypeQuery request, CancellationToken cancellationToken)
        {
            Result result;
            try
            {
                Expression<Func<TransportType, bool>> filter = _transportTypeOdataProvider.GetFilterPredicate(request.Filter);
                int? skip = request.Skip.ToNullableInt();
                int? top = request.Top.ToNullableInt();

                var transportTypeDomains = await _transportTypeReadRepository.ListAsync(filter, skip, top);
                var transportTypeModels = _mapper.Map<IEnumerable<TransportTypeModel>>(transportTypeDomains);
                var count = transportTypeModels.Count();
                var customerModel = new TransportTypesModel { Value = transportTypeModels, Count = count, NextLink = null };

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
                result = Result.Fail(CustomFailures.ListTransportTypeFailure);
            }
            return result;
        }
    }
}
