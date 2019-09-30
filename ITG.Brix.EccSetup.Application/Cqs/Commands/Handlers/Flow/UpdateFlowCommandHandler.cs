using ITG.Brix.EccSetup.Application.Bases;
using ITG.Brix.EccSetup.Application.Cqs.Commands.Definitions;
using ITG.Brix.EccSetup.Application.Exceptions;
using ITG.Brix.EccSetup.Application.Internal;
using ITG.Brix.EccSetup.Application.Resources;
using ITG.Brix.EccSetup.Application.Services.Json;
using ITG.Brix.EccSetup.Application.Utils;
using ITG.Brix.EccSetup.Domain;
using ITG.Brix.EccSetup.Domain.Model.Flows;
using ITG.Brix.EccSetup.Domain.Repositories;
using ITG.Brix.EccSetup.Infrastructure.Exceptions;
using ITG.Brix.EccSetup.Infrastructure.Providers;
using MediatR;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ITG.Brix.EccSetup.Application.Cqs.Commands.Handlers
{
    public class UpdateFlowCommandHandler : IRequestHandler<UpdateFlowCommand, Result>
    {
        private readonly IFlowWriteRepository _flowWriteRepository;
        private readonly IFlowReadRepository _flowReadRepository;
        private readonly IVersionProvider _versionProvider;
        private readonly IJsonService<object> _jsonService;

        public UpdateFlowCommandHandler(IFlowWriteRepository flowWriteRepository,
                                        IFlowReadRepository flowReadRepository,
                                        IVersionProvider versionProvider,
                                        IJsonService<object> jsonService)
        {
            _flowWriteRepository = flowWriteRepository ?? throw Error.ArgumentNull(nameof(flowWriteRepository));
            _flowReadRepository = flowReadRepository ?? throw Error.ArgumentNull(nameof(flowReadRepository));
            _versionProvider = versionProvider ?? throw Error.ArgumentNull(nameof(versionProvider));
            _jsonService = jsonService ?? throw Error.ArgumentNull(nameof(jsonService));
        }

        public async Task<Result> Handle(UpdateFlowCommand request, CancellationToken cancellationToken)
        {
            Result result;
            try
            {
                var flow = await _flowReadRepository.GetAsync(request.Id);
                if (flow.Version != request.Version)
                {
                    throw new CommandVersionException();
                }

                if (request.Name.HasValue)
                {
                    flow.ChangeName(request.Name.Value);
                }
                if (request.Description.HasValue)
                {
                    flow.SetDescription(request.Description.Value);
                }
                if (request.Image.HasValue)
                {
                    flow.SetImage(request.Image.Value);
                }
                if (request.Diagram.HasValue)
                {
                    flow.SetDiagram(request.Diagram.Value);
                }
                if (request.FilterContent.HasValue)
                {
                    flow.SetFilterContent(request.FilterContent.Value);
                    var filter = _jsonService.Deserialize<FlowFilter>(request.FilterContent.Value.ToString());

                    filter.Sources = (filter.Sources.IsEnumerableNullOrEmpty()) ? new List<FlowSource> { new FlowSource("x") } : new List<FlowSource>(filter.Sources);
                    filter.Operations = (filter.Operations.IsEnumerableNullOrEmpty()) ? new List<FlowOperation> { new FlowOperation("x") } : new List<FlowOperation>(filter.Operations);
                    filter.Sites = (filter.Sites.IsEnumerableNullOrEmpty()) ? new List<FlowSite> { new FlowSite("x") } : new List<FlowSite>(filter.Sites);
                    filter.OperationalDepartments = (filter.OperationalDepartments.IsEnumerableNullOrEmpty()) ? new List<FlowOperationalDepartment> { new FlowOperationalDepartment("x") } : new List<FlowOperationalDepartment>(filter.OperationalDepartments);
                    filter.TypePlannings = (filter.TypePlannings.IsEnumerableNullOrEmpty()) ? new List<FlowTypePlanning> { new FlowTypePlanning("x") } : new List<FlowTypePlanning>(filter.TypePlannings);
                    filter.Customers = (filter.Customers.IsEnumerableNullOrEmpty()) ? new List<FlowCustomer> { new FlowCustomer("x") } : new List<FlowCustomer>(filter.Customers);
                    filter.ProductionSites = (filter.ProductionSites.IsEnumerableNullOrEmpty()) ? new List<FlowProductionSite> { new FlowProductionSite("x") } : new List<FlowProductionSite>(filter.ProductionSites);
                    filter.TransportTypes = (filter.TransportTypes.IsEnumerableNullOrEmpty()) ? new List<FlowTransportType> { new FlowTransportType("x") } : new List<FlowTransportType>(filter.TransportTypes);
                    filter.DriverWait = (filter.DriverWait == null) ? "x" : filter.DriverWait;

                    flow.SetFilter(filter);
                }

                flow.Version = _versionProvider.Generate();
                await _flowWriteRepository.UpdateAsync(flow);
                result = Result.Ok(flow.Version);

            }
            catch (EntityNotFoundDbException)
            {
                result = Result.Fail(new System.Collections.Generic.List<Failure>() {
                                        new HandlerFault(){
                                            Code = HandlerFaultCode.NotFound.Name,
                                            Message = string.Format(HandlerFailures.NotFound, "Flow"),
                                            Target = "id"}
                                        }
                );
            }
            catch (CommandVersionException)
            {
                result = Result.Fail(new System.Collections.Generic.List<Failure>() {
                                        new HandlerFault(){
                                            Code = HandlerFaultCode.NotMet.Name,
                                            Message = HandlerFailures.NotMet,
                                            Target = "version"}
                                        }
                );
            }
            catch (UniqueKeyException)
            {
                result = Result.Fail(new System.Collections.Generic.List<Failure>() {
                                        new HandlerFault(){
                                            Code = HandlerFaultCode.Conflict.Name,
                                            Message = HandlerFailures.Conflict,
                                            Target = "name"}
                                        }
                );
            }
            catch
            {
                result = Result.Fail(CustomFailures.UpdateFlowFailure);
            }

            return result;
        }
    }
}
