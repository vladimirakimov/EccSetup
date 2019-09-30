using ITG.Brix.EccSetup.Domain.Bases;
using System.Collections.Generic;

namespace ITG.Brix.EccSetup.Domain.Model.Flows
{
    public class FlowFilter : ValueObject
    {
        public IEnumerable<FlowSource> Sources { get; set; }
        public IEnumerable<FlowOperation> Operations { get; set; }
        public IEnumerable<FlowSite> Sites { get; set; }
        public IEnumerable<FlowOperationalDepartment> OperationalDepartments { get; set; }
        public IEnumerable<FlowTypePlanning> TypePlannings { get; set; }
        public IEnumerable<FlowCustomer> Customers { get; set; }
        public IEnumerable<FlowProductionSite> ProductionSites { get; set; }
        public IEnumerable<FlowTransportType> TransportTypes { get; set; }
        public string DriverWait { get; set; }

        public FlowFilter(IEnumerable<FlowSource> sources,
                          IEnumerable<FlowOperation> operations,
                          IEnumerable<FlowSite> sites,
                          IEnumerable<FlowOperationalDepartment> operationalDepartments,
                          IEnumerable<FlowTypePlanning> typePlannings,
                          IEnumerable<FlowCustomer> customers,
                          IEnumerable<FlowProductionSite> productionSites,
                          IEnumerable<FlowTransportType> transportTypes,
                          string driverWait)
        {
            Sources = sources;
            Operations = operations;
            Sites = sites;
            OperationalDepartments = operationalDepartments;
            TypePlannings = typePlannings;
            Customers = customers;
            ProductionSites = productionSites;
            TransportTypes = transportTypes;
            DriverWait = driverWait;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Sources;
            yield return Operations;
            yield return Sites;
            yield return OperationalDepartments;
            yield return TypePlannings;
            yield return Customers;
            yield return ProductionSites;
            yield return TransportTypes;
            yield return DriverWait;
        }
    }
}
