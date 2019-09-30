using System.Collections.Generic;

namespace ITG.Brix.EccSetup.Application.Cqs.Queries.Models
{
    public class SitesModel
    {
        public long Count { get; set; }
        public string NextLink { get; set; }
        public IEnumerable<SiteModel> Value { get; set; }
    }
}
