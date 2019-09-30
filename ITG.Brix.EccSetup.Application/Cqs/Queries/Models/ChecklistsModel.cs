using System.Collections.Generic;

namespace ITG.Brix.EccSetup.Application.Cqs.Queries.Models
{
    public class ChecklistsModel
    {
        public long Count { get; set; }
        public string NextLink { get; set; }
        public IEnumerable<ChecklistModel> Value { get; set; }
    }
}
