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
    public class ListInputQueryHandler : IRequestHandler<ListInputQuery, Result>
    {
        private readonly IMapper _mapper;
        private readonly IInputReadRepository _inputReadRepository;

        public ListInputQueryHandler(IMapper mapper,
                                     IInputReadRepository inputReadRepository)
        {
            _mapper = mapper ?? throw Error.ArgumentNull(nameof(mapper));
            _inputReadRepository = inputReadRepository ?? throw Error.ArgumentNull(nameof(inputReadRepository));
        }

        public async Task<Result> Handle(ListInputQuery request, CancellationToken cancellationToken)
        {
            Result result;
            try
            {
                Expression<Func<Input, bool>> filter = null;
                int? skip = request.Skip.ToNullableInt();
                int? top = request.Top.ToNullableInt();

                var inputDomains = await _inputReadRepository.ListAsync(filter, skip, top);
                var inputModels = _mapper.Map<IEnumerable<InputModel>>(inputDomains);
                var count = inputModels.Count();
                var inputsModel = new InputsModel { Value = inputModels, Count = count, NextLink = null };

                result = Result.Ok(inputsModel);
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
                result = Result.Fail(CustomFailures.ListInputFailure);
            }
            return result;
        }
    }
}
