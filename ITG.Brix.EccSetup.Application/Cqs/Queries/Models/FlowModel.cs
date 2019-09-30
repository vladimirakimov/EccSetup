using ITG.Brix.EccSetup.Application.Cqs.Queries.Models.Flows;
using System;

namespace ITG.Brix.EccSetup.Application.Cqs.Queries.Models
{
    public class FlowModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string Diagram { get; set; }
        public string FilterContent { get; set; }
        public FlowFilterModel Filter { get; set; }
    }
}
