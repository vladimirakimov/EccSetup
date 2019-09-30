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
    public class ListDamageCodeQueryHandler : IRequestHandler<ListDamageCodeQuery, Result>
    {
        private readonly IMapper _mapper;
        private readonly IDamageCodeReadRepository _damageCodeReadRepository;
        private readonly IDamageCodeOdataProvider _damageCodeOdataProvider;

        public ListDamageCodeQueryHandler(IMapper mapper, IDamageCodeReadRepository damageCodeReadRepository, IDamageCodeOdataProvider damageCodeOdataProvider)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _damageCodeReadRepository = damageCodeReadRepository ?? throw new ArgumentNullException(nameof(damageCodeReadRepository));
            _damageCodeOdataProvider = damageCodeOdataProvider ?? throw new ArgumentNullException(nameof(damageCodeOdataProvider));
        }

        public async Task<Result> Handle(ListDamageCodeQuery request, CancellationToken cancellationToken)
        {
            Result result;
            try
            {
                Expression<Func<DamageCode, bool>> filter = _damageCodeOdataProvider.GetFilterPredicate(request.Filter);
                int? skip = request.Skip.ToNullableInt();
                int? top = request.Top.ToNullableInt();

                var damageCodeDomains = await _damageCodeReadRepository.ListAsync(filter, skip, top);
                var damageCodeModels = _mapper.Map<IEnumerable<DamageCodeModel>>(damageCodeDomains);
                var count = damageCodeModels.Count();
                var damageCodeModel = new DamageCodesModel { Value = damageCodeModels, Count = count, NextLink = null };

                result = Result.Ok(damageCodeModel);
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
                result = Result.Fail(CustomFailures.ListDamageCodeFailure);
            }
            return result;
        }
    }
}
