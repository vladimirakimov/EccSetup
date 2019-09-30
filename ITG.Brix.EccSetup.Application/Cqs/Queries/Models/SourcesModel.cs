using System.Collections.Generic;

namespace ITG.Brix.EccSetup.Application.Cqs.Queries.Models
{
    public class SourcesModel
    {
        public long Count { get; set; }
        public string NextLink { get; set; }
        public IEnumerable<SourceModel> Value { get; set; }
    }
}
