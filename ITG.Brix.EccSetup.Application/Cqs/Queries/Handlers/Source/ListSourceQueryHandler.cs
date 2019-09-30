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
    public class ListSourceQueryHandler : IRequestHandler<ListSourceQuery, Result>
    {
        private readonly IMapper _mapper;
        private readonly ISourceReadRepository _sourceReadRepository;
        private readonly ISourceOdataProvider _sourceOdataProvider;

        public ListSourceQueryHandler(IMapper mapper,
                                      ISourceReadRepository sourceReadRepository,
                                      ISourceOdataProvider sourceOdataProvider)
        {
            _mapper = mapper ?? throw Error.ArgumentNull(nameof(mapper));
            _sourceReadRepository = sourceReadRepository ?? throw Error.ArgumentNull(nameof(sourceReadRepository));
            _sourceOdataProvider = sourceOdataProvider ?? throw Error.ArgumentNull(nameof(sourceOdataProvider));
        }

        public async Task<Result> Handle(ListSourceQuery request, CancellationToken cancellationToken)
        {
            Result result;
            try
            {
                Expression<Func<Source, bool>> filter = _sourceOdataProvider.GetFilterPredicate(request.Filter);
                int? skip = request.Skip.ToNullableInt();
                int? top = request.Top.ToNullableInt();

                var sourceDomains = await _sourceReadRepository.ListAsync(filter, skip, top);
                var sourceModels = _mapper.Map<IEnumerable<SourceModel>>(sourceDomains);
                var count = sourceModels.Count();
                var sourcesModel = new SourcesModel { Value = sourceModels, Count = count, NextLink = null };

                result = Result.Ok(sourcesModel);
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
                result = Result.Fail(CustomFailures.ListSourceFailure);
            }
            return result;
        }
    }
}
