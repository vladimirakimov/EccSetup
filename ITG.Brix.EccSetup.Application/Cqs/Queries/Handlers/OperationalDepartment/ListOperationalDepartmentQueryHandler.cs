using AutoMapper;
using ITG.Brix.EccSetup.Application.Bases;
using ITG.Brix.EccSetup.Application.Cqs.Queries.Definitions;
using ITG.Brix.EccSetup.Application.Cqs.Queries.Models;
using ITG.Brix.EccSetup.Application.Extensions;
using ITG.Brix.EccSetup.Application.Resources;
using ITG.Brix.EccSetup.Domain;
using ITG.Brix.EccSetup.Domain.Repositories.OperationalDepartments;
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
    public class ListOperationalDepartmentQueryHandler : IRequestHandler<ListOperationalDepartmentQuery, Result>
    {
        private readonly IMapper _mapper;
        private readonly IOperationalDepartmentReadRepository _operationalDepartmentReadRepository;
        private readonly IOperationalDepartmentOdataProvider _operationalDepartmentOdataProvider;

        public ListOperationalDepartmentQueryHandler(IMapper mapper,
                                                     IOperationalDepartmentReadRepository operationalDepartmentReadRepository,
                                                     IOperationalDepartmentOdataProvider operationalDepartmentOdataProvider)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _operationalDepartmentReadRepository = operationalDepartmentReadRepository ?? throw new ArgumentNullException(nameof(operationalDepartmentReadRepository));
            _operationalDepartmentOdataProvider = operationalDepartmentOdataProvider ?? throw new ArgumentNullException(nameof(operationalDepartmentOdataProvider));
        }

        public async Task<Result> Handle(ListOperationalDepartmentQuery request, CancellationToken cancellationToken)
        {
            Result result;
            try
            {
                Expression<Func<OperationalDepartment, bool>> filter = _operationalDepartmentOdataProvider.GetFilterPredicate(request.Filter);
                int? skip = request.Skip.ToNullableInt();
                int? top = request.Top.ToNullableInt();

                var operationalDepartmentDomains = await _operationalDepartmentReadRepository.ListAsync(filter, skip, top);
                var operationalDepartmentModels = _mapper.Map<IEnumerable<OperationalDepartmentModel>>(operationalDepartmentDomains);
                var count = operationalDepartmentModels.Count();
                var operationalDepartmentModel = new OperationalDepartmentsModel { Value = operationalDepartmentModels, Count = count, NextLink = null };

                result = Result.Ok(operationalDepartmentModel);
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
                result = Result.Fail(CustomFailures.ListOperationalDepartmentFailure);
            }
            return result;
        }
    }
}
