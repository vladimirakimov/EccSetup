using System.Collections.Generic;

namespace ITG.Brix.EccSetup.Application.Cqs.Queries.Models
{
    public class FlowsModel
    {
        public long Count { get; set; }
        public string NextLink { get; set; }
        public IEnumerable<FlowModel> Value { get; set; }
    }
}
