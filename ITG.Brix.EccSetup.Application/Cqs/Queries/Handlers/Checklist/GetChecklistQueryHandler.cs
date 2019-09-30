using AutoMapper;
using ITG.Brix.EccSetup.Application.Bases;
using ITG.Brix.EccSetup.Application.Cqs.Queries.Definitions;
using ITG.Brix.EccSetup.Application.Cqs.Queries.Models;
using ITG.Brix.EccSetup.Application.Internal;
using ITG.Brix.EccSetup.Application.Resources;
using ITG.Brix.EccSetup.Domain.Repositories;
using ITG.Brix.EccSetup.Infrastructure.Exceptions;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ITG.Brix.EccSetup.Application.Cqs.Queries.Handlers
{
    public class GetChecklistQueryHandler : IRequestHandler<GetChecklistQuery, Result>
    {
        private readonly IMapper _mapper;
        private readonly IChecklistReadRepository _checklistReadRepository;

        public GetChecklistQueryHandler(IMapper mapper,
                                        IChecklistReadRepository checklistReadRepository)
        {
            _checklistReadRepository = checklistReadRepository ?? throw Error.ArgumentNull(nameof(checklistReadRepository));
            _mapper = mapper ?? throw Error.ArgumentNull(nameof(mapper));
        }
        public async Task<Result> Handle(GetChecklistQuery request, CancellationToken cancellationToken)
        {
            Result result;

            try
            {
                var checklist = await _checklistReadRepository.GetAsync(request.Id);
                var checklistModel = _mapper.Map<ChecklistModel>(checklist);

                result = Result.Ok(checklistModel, checklist.Version);
            }
            catch (EntityNotFoundDbException)
            {
                result = Result.Fail(new System.Collections.Generic.List<Failure>() {
                                        new HandlerFault(){
                                            Code = HandlerFaultCode.NotFound.Name,
                                            Message = string.Format(HandlerFailures.NotFound, "Checklist"),
                                            Target = "id"}
                                        }
                );
            }
            catch
            {
                result = Result.Fail(CustomFailures.GetChecklistFailure);
            }

            return result;
        }
    }
}
