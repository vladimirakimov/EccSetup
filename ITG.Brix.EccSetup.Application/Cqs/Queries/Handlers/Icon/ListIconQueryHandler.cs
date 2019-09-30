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
    public class ListIconQueryHandler : IRequestHandler<ListIconQuery, Result>
    {
        private readonly IMapper _mapper;
        private readonly IIconReadRepository _iconReadRepository;
        private readonly IIconOdataProvider _iconOdataProvider;

        public ListIconQueryHandler(IMapper mapper,
                                    IIconReadRepository iconReadRepository,
                                    IIconOdataProvider iconOdataProvider)
        {
            _mapper = mapper ?? throw Error.ArgumentNull(nameof(mapper));
            _iconReadRepository = iconReadRepository ?? throw Error.ArgumentNull(nameof(iconReadRepository));
            _iconOdataProvider = iconOdataProvider ?? throw Error.ArgumentNull(nameof(iconOdataProvider));
        }

        public async Task<Result> Handle(ListIconQuery request, CancellationToken cancellationToken)
        {
            Result result;
            try
            {
                Expression<Func<Icon, bool>> filter = _iconOdataProvider.GetFilterPredicate(request.Filter);
                int? skip = request.Skip.ToNullableInt();
                int? top = request.Top.ToNullableInt();

                var iconDomains = await _iconReadRepository.ListAsync(filter, skip, top);
                var iconModels = _mapper.Map<IEnumerable<IconModel>>(iconDomains);
                var count = iconModels.Count();
                var iconsModel = new IconsModel { Value = iconModels, Count = count, NextLink = null };

                result = Result.Ok(iconsModel);
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
                result = Result.Fail(CustomFailures.ListIconFailure);
            }
            return result;
        }
    }
}
