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
    public class ListInstructionQueryHandler : IRequestHandler<ListInstructionQuery, Result>
    {
        private readonly IInstructionReadRepository _instructionReadRepository;
        private readonly IMapper _mapper;

        public ListInstructionQueryHandler(IMapper mapper, IInstructionReadRepository instructionReadRepository)
        {
            _mapper = mapper ?? throw Error.ArgumentNull(nameof(mapper));
            _instructionReadRepository = instructionReadRepository ?? throw Error.ArgumentNull(nameof(instructionReadRepository));
        }
        public async Task<Result> Handle(ListInstructionQuery request, CancellationToken cancellationToken)
        {
            Result result;
            try
            {
                Expression<Func<Instruction, bool>> filter = null;
                int? skip = request.Skip.ToNullableInt();
                int? top = request.Top.ToNullableInt();

                var instructionDomains = await _instructionReadRepository.ListAsync(filter, skip, top);
                var instructionModels = _mapper.Map<IEnumerable<InstructionModel>>(instructionDomains);
                var count = instructionModels.Count();
                var sourcesModel = new InstructionsModel { Value = instructionModels, Count = count, NextLink = null };

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
                result = Result.Fail(CustomFailures.ListInstructionFailure);
            }
            return result;
        }
    }
}
