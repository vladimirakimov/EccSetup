using ITG.Brix.EccSetup.Application.Bases;
using ITG.Brix.EccSetup.Application.Cqs.Commands.Dtos;
using MediatR;
using System.Collections.Generic;

namespace ITG.Brix.EccSetup.Application.Cqs.Commands.Definitions
{
    public class CreateFlowCommand : IRequest<Result>
    {
        private readonly List<string> _operations;
        private readonly List<string> _sources;
        private readonly List<string> _sites;
        private readonly List<string> _operationalDepartments;
        private readonly List<string> _typePlannings;
        private readonly List<string> _customers;
        private readonly List<string> _productionSites;
        private readonly List<string> _transportTypes;
        private readonly List<BuildingBlockDto> _buildingBlocks;
        private readonly List<BuildingBlockDto> _freeActions;

        public string Name { get; private set; }
        public string Description { get; private set; }
        public string Image { get; private set; }
        public string Diagram { get; private set; }

        public IEnumerable<string> Operations => _operations;
        public IEnumerable<string> Customers => _customers;
        public IEnumerable<string> Sources => _sources;
        public IEnumerable<string> Sites => _sites;
        public IEnumerable<string> OperationalDepartments => _operationalDepartments;
        public IEnumerable<string> TypePlannings => _typePlannings;
        public IEnumerable<string> ProductionSites => _productionSites;
        public IEnumerable<string> TransportTypes => _transportTypes;
        public IEnumerable<BuildingBlockDto> BuildingBlocks => _buildingBlocks;
        public IEnumerable<BuildingBlockDto> FreeActions => _freeActions;

        public CreateFlowCommand(string name,
                                 string description,
                                 string image)
        {
            Name = name;
            Description = description;
            Image = image;
            Diagram = null;

            _operations = new List<string>();
            _sources = new List<string>();
            _sites = new List<string>();
            _operationalDepartments = new List<string>();
            _typePlannings = new List<string>();
            _customers = new List<string>();
            _productionSites = new List<string>();
            _transportTypes = new List<string>();
            _buildingBlocks = new List<BuildingBlockDto>();
            _freeActions = new List<BuildingBlockDto>();
        }

    }
}
