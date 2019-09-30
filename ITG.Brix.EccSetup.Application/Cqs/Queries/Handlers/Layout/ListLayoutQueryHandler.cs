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
    public class ListLayoutQueryHandler : IRequestHandler<ListLayoutQuery, Result>
    {
        private readonly IMapper _mapper;
        private readonly ILayoutReadRepository _layoutReadRepository;
        private readonly ILayoutOdataProvider _layoutOdataProvider;

        public ListLayoutQueryHandler(IMapper mapper,
                                      ILayoutReadRepository layoutReadRepository,
                                      ILayoutOdataProvider layoutOdataProvider)
        {
            _mapper = mapper ?? throw Error.ArgumentNull(nameof(mapper));
            _layoutReadRepository = layoutReadRepository ?? throw Error.ArgumentNull(nameof(layoutReadRepository));
            _layoutOdataProvider = layoutOdataProvider ?? throw Error.ArgumentNull(nameof(layoutOdataProvider));
        }

        public async Task<Result> Handle(ListLayoutQuery request, CancellationToken cancellationToken)
        {
            Result result;
            try
            {
                Expression<Func<Layout, bool>> filter = _layoutOdataProvider.GetFilterPredicate(request.Filter);
                int? skip = request.Skip.ToNullableInt();
                int? top = request.Top.ToNullableInt();

                var layoutDomains = await _layoutReadRepository.ListAsync(filter, skip, top);
                var layoutModels = _mapper.Map<IEnumerable<LayoutModel>>(layoutDomains);
                var count = layoutModels.Count();
                var layoutsModel = new LayoutsModel { Value = layoutModels, Count = count, NextLink = null };

                result = Result.Ok(layoutsModel);
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
                result = Result.Fail(CustomFailures.ListLayoutFailure);
            }
            return result;
        }
    }
}
