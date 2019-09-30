using System.Collections.Generic;

namespace ITG.Brix.EccSetup.Application.Cqs.Queries.Models.BuildingBlocks.Remark
{
    public class RemarksModel
    {
        public long Count { get; set; }
        public string NextLink { get; set; }
        public IEnumerable<RemarkModel> Value { get; set; }
    }
}
