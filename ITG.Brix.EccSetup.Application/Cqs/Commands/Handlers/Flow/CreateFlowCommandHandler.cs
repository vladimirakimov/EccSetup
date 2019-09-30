using ITG.Brix.EccSetup.Application.Bases;
using ITG.Brix.EccSetup.Application.Cqs.Commands.Definitions;
using ITG.Brix.EccSetup.Application.Internal;
using ITG.Brix.EccSetup.Application.Resources;
using ITG.Brix.EccSetup.Domain;
using ITG.Brix.EccSetup.Domain.Repositories;
using ITG.Brix.EccSetup.Infrastructure.Exceptions;
using ITG.Brix.EccSetup.Infrastructure.Providers;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ITG.Brix.EccSetup.Application.Cqs.Commands.Handlers
{
    public class CreateFlowCommandHandler : IRequestHandler<CreateFlowCommand, Result>
    {
        private readonly IFlowWriteRepository _flowWriteRepository;
        private readonly IIdentifierProvider _identifierProvider;
        private readonly IVersionProvider _versionProvider;

        public CreateFlowCommandHandler(IFlowWriteRepository flowWriteRepository,
                                        IIdentifierProvider identifierProvider,
                                        IVersionProvider versionProvider)
        {
            _flowWriteRepository = flowWriteRepository ?? throw Error.ArgumentNull(nameof(flowWriteRepository));
            _identifierProvider = identifierProvider ?? throw Error.ArgumentNull(nameof(identifierProvider));
            _versionProvider = versionProvider ?? throw Error.ArgumentNull(nameof(versionProvider));
        }

        public async Task<Result> Handle(CreateFlowCommand request, CancellationToken cancellationToken)
        {
            var id = _identifierProvider.Generate();

            var flowToCreate = new Flow(id, request.Name);
            if (!string.IsNullOrWhiteSpace(request.Description))
            {
                flowToCreate.SetDescription(request.Description);
            }
            if (!string.IsNullOrWhiteSpace(request.Image))
            {
                flowToCreate.SetImage(request.Image);
            }
            //command.BuildingBlocks.ToList().ForEach(x => flowToCreate.AddBuildingBlock(new BuildingBlock(x.Id, (BlockType)Enum.Parse(typeof(BlockType), x.BlockType, true))));
            //command.FreeActions.ToList().ForEach(x => flowToCreate.AddFreeAction(new BuildingBlock(x.Id, (BlockType)Enum.Parse(typeof(BlockType), x.BlockType, true))));
            //command.OperationalDepartments.ToList().ForEach(x => flowToCreate.AddOperationalDepartment(new FlowOperationalDepartment(x)));
            //command.Operations.ToList().ForEach(x => flowToCreate.AddOperation(new FlowOperation(x)));
            //command.ProductionSites.ToList().ForEach(x => flowToCreate.AddProductionSite(new FlowProductionSite(x)));
            //command.Sites.ToList().ForEach(x => flowToCreate.AddSite(new FlowSite(x)));
            //command.Sources.ToList().ForEach(x => flowToCreate.AddSource(new FlowSource(x)));
            //command.TransportTypes.ToList().ForEach(x => flowToCreate.AddTransportType(new FlowTransportType(x)));
            //command.TypePlannings.ToList().ForEach(x => flowToCreate.AddTypePlanning(new FlowTypePlanning(x)));
            //command.Customers.ToList().ForEach(x => flowToCreate.AddCustomer(new FlowCustomer(x)));

            flowToCreate.Version = _versionProvider.Generate();

            Result result;

            try
            {
                await _flowWriteRepository.CreateAsync(flowToCreate);
                result = Result.Ok(id, flowToCreate.Version);
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
                result = Result.Fail(CustomFailures.CreateFlowFailure);
            }

            return result;
        }
    }
}
