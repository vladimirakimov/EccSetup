using System.Collections.Generic;

namespace ITG.Brix.EccSetup.Application.Cqs.Queries.Models
{
    public class LocationsModel
    {
        public long Count { get; set; }
        public string NextLink { get; set; }
        public IEnumerable<LocationModel> Value { get; set; }
    }
}
