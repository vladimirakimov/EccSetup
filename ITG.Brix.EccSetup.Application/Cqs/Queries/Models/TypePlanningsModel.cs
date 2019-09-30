using System.Collections.Generic;

namespace ITG.Brix.EccSetup.Application.Cqs.Queries.Models
{
    public class TypePlanningsModel
    {
        public long Count { get; set; }
        public string NextLink { get; set; }
        public IEnumerable<TypePlanningModel> Value { get; set; }
    }
}
