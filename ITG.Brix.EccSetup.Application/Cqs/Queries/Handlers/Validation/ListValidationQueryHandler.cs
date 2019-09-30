using AutoMapper;
using ITG.Brix.EccSetup.Application.Bases;
using ITG.Brix.EccSetup.Application.Cqs.Queries.Definitions;
using ITG.Brix.EccSetup.Application.Cqs.Queries.Models.BuildingBlocks.Validations;
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
    public class ListValidationQueryHandler : IRequestHandler<ListValidationQuery, Result>
    {
        private readonly IMapper _mapper;
        private readonly IValidationReadRepository _validationReadRepository;

        public ListValidationQueryHandler(IMapper mapper,
                                          IValidationReadRepository validationReadRepository)
        {
            _mapper = mapper ?? throw Error.ArgumentNull(nameof(mapper));
            _validationReadRepository = validationReadRepository ?? throw Error.ArgumentNull(nameof(validationReadRepository));
        }

        public async Task<Result> Handle(ListValidationQuery request, CancellationToken cancellationToken)
        {
            Result result;
            try
            {
                Expression<Func<Validation, bool>> filter = null;
                int? skip = request.Skip.ToNullableInt();
                int? top = request.Top.ToNullableInt();

                var validationDomains = await _validationReadRepository.ListAsync(filter, skip, top);
                var validationModels = _mapper.Map<IEnumerable<ValidationModel>>(validationDomains);
                var count = validationModels.Count();
                var sourcesModel = new ValidationsModel { Value = validationModels, Count = count, NextLink = null };

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
                result = Result.Fail(CustomFailures.ListValidationFailure);
            }
            return result;
        }
    }
}
