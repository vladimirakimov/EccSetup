using System.Collections.Generic;

namespace ITG.Brix.EccSetup.Application.Cqs.Queries.Models.BuildingBlocks
{
    public class InformationsModel
    {
        public long Count { get; set; }
        public string NextLink { get; set; }
        public IEnumerable<InformationModel> Value { get; set; }
    }
}
