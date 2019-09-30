using System.Collections.Generic;

namespace ITG.Brix.EccSetup.Application.Cqs.Queries.Models
{
    public class DamageCodesModel
    {
        public long Count { get; set; }
        public string NextLink { get; set; }
        public IEnumerable<DamageCodeModel> Value { get; set; }
    }
}
