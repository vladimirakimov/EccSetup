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
    public class ListCustomerQueryHandler : IRequestHandler<ListCustomerQuery, Result>
    {
        private readonly IMapper _mapper;
        private readonly ICustomerReadRepository _customerReadRepository;
        private readonly ICustomerOdataProvider _customerOdataProvider;

        public ListCustomerQueryHandler(IMapper mapper, ICustomerReadRepository customerReadRepository, ICustomerOdataProvider customerOdataProvider)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _customerReadRepository = customerReadRepository ?? throw new ArgumentNullException(nameof(customerReadRepository));
            _customerOdataProvider = customerOdataProvider ?? throw new ArgumentNullException(nameof(customerOdataProvider));
        }

        public async Task<Result> Handle(ListCustomerQuery request, CancellationToken cancellationToken)
        {
            Result result;
            try
            {
                Expression<Func<Customer, bool>> filter = _customerOdataProvider.GetFilterPredicate(request.Filter);
                int? skip = request.Skip.ToNullableInt();
                int? top = request.Top.ToNullableInt();

                var customerDomains = await _customerReadRepository.ListAsync(filter, skip, top);
                var customerModels = _mapper.Map<IEnumerable<CustomerModel>>(customerDomains);
                var count = customerModels.Count();
                var customerModel = new CustomersModel { Value = customerModels, Count = count, NextLink = null };

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
                result = Result.Fail(CustomFailures.ListCustomerFailure);
            }
            return result;
        }
    }
}
