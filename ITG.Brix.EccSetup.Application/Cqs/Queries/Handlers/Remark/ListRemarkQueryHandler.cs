using AutoMapper;
using ITG.Brix.EccSetup.Application.Bases;
using ITG.Brix.EccSetup.Application.Cqs.Queries.Definitions;
using ITG.Brix.EccSetup.Application.Cqs.Queries.Models.BuildingBlocks.Remark;
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
    public class ListRemarkQueryHandler : IRequestHandler<ListRemarkQuery, Result>
    {
        private readonly IMapper _mapper;
        private readonly IRemarkReadRepository _remarkReadRepository;

        public ListRemarkQueryHandler(IMapper mapper,
                                      IRemarkReadRepository remarkReadRepository)
        {
            _mapper = mapper ?? throw Error.ArgumentNull(nameof(mapper));
            _remarkReadRepository = remarkReadRepository ?? throw Error.ArgumentNull(nameof(remarkReadRepository));
        }

        public async Task<Result> Handle(ListRemarkQuery request, CancellationToken cancellationToken)
        {
            Result result;

            try
            {
                Expression<Func<Remark, bool>> filter = null;
                int? skip = request.Skip.ToNullableInt();
                int? top = request.Top.ToNullableInt();

                var remarkDomains = await _remarkReadRepository.ListAsync(filter, skip, top);
                var remarkModels = _mapper.Map<IEnumerable<RemarkModel>>(remarkDomains);
                var count = remarkModels.Count();
                var remarksModel = new RemarksModel { Value = remarkModels, Count = count, NextLink = null };

                result = Result.Ok(remarksModel);
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
                result = Result.Fail(CustomFailures.ListRemarkFailure);
            }

            return result;
        }
    }
}
