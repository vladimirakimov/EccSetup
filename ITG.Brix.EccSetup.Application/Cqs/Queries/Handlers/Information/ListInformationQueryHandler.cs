using AutoMapper;
using ITG.Brix.EccSetup.Application.Bases;
using ITG.Brix.EccSetup.Application.Cqs.Queries.Definitions;
using ITG.Brix.EccSetup.Application.Cqs.Queries.Models.BuildingBlocks;
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
    public class ListInformationQueryHandler : IRequestHandler<ListInformationQuery, Result>
    {
        private readonly IMapper _mapper;
        private readonly IInformationReadRepository _informationReadRepository;

        public ListInformationQueryHandler(IMapper mapper, IInformationReadRepository informationReadRepository)
        {
            _mapper = mapper ?? throw Error.ArgumentNull(nameof(mapper));
            _informationReadRepository = informationReadRepository ?? throw Error.ArgumentNull(nameof(informationReadRepository));
        }

        public async Task<Result> Handle(ListInformationQuery request, CancellationToken cancellationToken)
        {
            Result result;

            try
            {
                Expression<Func<Information, bool>> filter = null;
                int? skip = request.Skip.ToNullableInt();
                int? top = request.Top.ToNullableInt();

                var informationDomains = await _informationReadRepository.ListAsync(filter, skip, top);
                var informationModels = _mapper.Map<IEnumerable<InformationModel>>(informationDomains);
                var count = informationModels.Count();
                var remarksModel = new InformationsModel { Value = informationModels, Count = count, NextLink = null };

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
                result = Result.Fail(CustomFailures.ListInformationFailure);
            }

            return result;
        }
    }
}
