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
    public class ListStorageStatusQueryHandler : IRequestHandler<ListStorageStatusQuery, Result>
    {
        private readonly IMapper _mapper;
        private readonly IStorageStatusReadRepository _storageStatusReadRepository;
        private readonly IStorageStatusOdataProvider _storageStatusOdataProvider;

        public ListStorageStatusQueryHandler(IMapper mapper, IStorageStatusReadRepository storageStatusReadRepository, IStorageStatusOdataProvider storageStatusOdataProvider)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _storageStatusReadRepository = storageStatusReadRepository ?? throw new ArgumentNullException(nameof(storageStatusReadRepository));
            _storageStatusOdataProvider = storageStatusOdataProvider ?? throw new ArgumentNullException(nameof(storageStatusOdataProvider));
        }

        public async Task<Result> Handle(ListStorageStatusQuery request, CancellationToken cancellationToken)
        {
            Result result;
            try
            {
                Expression<Func<StorageStatus, bool>> filter = _storageStatusOdataProvider.GetFilterPredicate(request.Filter);
                int? skip = request.Skip.ToNullableInt();
                int? top = request.Top.ToNullableInt();

                var storageStatusDomains = await _storageStatusReadRepository.ListAsync(filter, skip, top);
                var storageStatusModels = _mapper.Map<IEnumerable<StorageStatusModel>>(storageStatusDomains);
                var count = storageStatusModels.Count();
                var customerModel = new StorageStatusesModel { Value = storageStatusModels, Count = count, NextLink = null };

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
                result = Result.Fail(CustomFailures.ListStorageStatusFailure);
            }
            return result;
        }
    }
}
