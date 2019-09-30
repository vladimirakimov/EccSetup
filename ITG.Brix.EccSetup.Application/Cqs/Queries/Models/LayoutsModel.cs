using System.Collections.Generic;

namespace ITG.Brix.EccSetup.Application.Cqs.Queries.Models
{
    public class LayoutsModel
    {
        public long Count { get; set; }
        public string NextLink { get; set; }
        public IEnumerable<LayoutModel> Value { get; set; }
    }
}
