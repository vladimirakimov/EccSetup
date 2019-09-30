using System.Collections.Generic;

namespace ITG.Brix.EccSetup.Application.Cqs.Queries.Models
{
    public class IconsModel
    {
        public long Count { get; set; }
        public string NextLink { get; set; }
        public IEnumerable<IconModel> Value { get; set; }
    }
}
