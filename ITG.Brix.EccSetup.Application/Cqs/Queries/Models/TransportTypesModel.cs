using System.Collections.Generic;

namespace ITG.Brix.EccSetup.Application.Cqs.Queries.Models
{
    public class TransportTypesModel
    {
        public long Count { get; set; }
        public string NextLink { get; set; }
        public IEnumerable<TransportTypeModel> Value { get; set; }
    }
}
