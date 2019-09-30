using System.Collections.Generic;

namespace ITG.Brix.EccSetup.Application.Cqs.Queries.Models.Flows
{
    public class FlowFilterModel
    {
        public IEnumerable<FlowSourceModel> Sources { get; set; }
        public IEnumerable<FlowOperationModel> Operations { get; set; }
        public IEnumerable<FlowSiteModel> Sites { get; set; }
        public IEnumerable<FlowOperationalDepartmentModel> OperationalDepartments { get; set; }
        public IEnumerable<FlowTypePlanningModel> TypePlannings { get; set; }
        public IEnumerable<FlowCustomerModel> Customers { get; set; }
        public IEnumerable<FlowProductionSiteModel> ProductionSites { get; set; }
        public IEnumerable<FlowTransportTypeModel> TransportTypes { get; set; }
        public string DriverWait { get; set; }
    }
}
