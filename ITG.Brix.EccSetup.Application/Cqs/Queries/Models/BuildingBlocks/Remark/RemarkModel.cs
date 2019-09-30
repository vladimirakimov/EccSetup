using System;
using System.Collections.Generic;

namespace ITG.Brix.EccSetup.Application.Cqs.Queries.Models.BuildingBlocks.Remark
{
    public class RemarkModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string NameOnApplication { get; set; }
        public string Description { get; set; }
        public Guid Icon { get; set; }
        public List<string> DefaultRemarks { get; set; }
        public List<string> Tags { get; set; }
    }
}
