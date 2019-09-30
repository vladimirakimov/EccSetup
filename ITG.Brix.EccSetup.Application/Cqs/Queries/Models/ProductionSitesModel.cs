using System.Collections.Generic;

namespace ITG.Brix.EccSetup.Application.Cqs.Queries.Models
{
    public class ProductionSitesModel
    {
        public long Count { get; set; }
        public string NextLink { get; set; }
        public IEnumerable<ProductionSiteModel> Value { get; set; }
    }
}
