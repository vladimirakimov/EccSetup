using System.Collections.Generic;

namespace ITG.Brix.EccSetup.Application.Cqs.Queries.Models
{
    public class StorageStatusesModel
    {
        public long Count { get; set; }
        public string NextLink { get; set; }
        public IEnumerable<StorageStatusModel> Value { get; set; }
    }
}
