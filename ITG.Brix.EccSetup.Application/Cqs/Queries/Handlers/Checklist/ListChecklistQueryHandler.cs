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
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace ITG.Brix.EccSetup.Application.Cqs.Queries.Handlers
{
    public class ListChecklistQueryHandler : IRequestHandler<ListChecklistQuery, Result>
    {
        private readonly IMapper _mapper;
        private readonly IChecklistReadRepository _checklistReadRepository;

        public ListChecklistQueryHandler(IMapper mapper,
                                         IChecklistReadRepository checklistReadRepository)
        {
            _checklistReadRepository = checklistReadRepository ?? throw Error.ArgumentNull(nameof(checklistReadRepository));
            _mapper = mapper ?? throw Error.ArgumentNull(nameof(mapper));
        }

        public async Task<Result> Handle(ListChecklistQuery request, CancellationToken cancellationToken)
        {
            Result result;
            try
            {
                Expression<Func<Checklist, bool>> filter = null;
                int? skip = request.Skip.ToNullableInt();
                int? top = request.Top.ToNullableInt();

                var checklistDomains = await _checklistReadRepository.ListAsync(filter, skip, top);
                var checklistModels = _mapper.Map<IEnumerable<ChecklistModel>>(checklistDomains);
                var count = checklistModels.Count();
                var checklistsModel = new ChecklistsModel { Value = checklistModels, Count = count, NextLink = null };

                result = Result.Ok(checklistsModel);
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
                result = Result.Fail(CustomFailures.ListChecklistFailure);
            }
            return result;
        }
    }
}
