using System.Collections.Generic;

namespace ITG.Brix.EccSetup.Application.Cqs.Queries.Models
{
    public class OperationsModel
    {
        public long Count { get; set; }
        public string NextLink { get; set; }
        public IEnumerable<OperationModel> Value { get; set; }
    }
}
