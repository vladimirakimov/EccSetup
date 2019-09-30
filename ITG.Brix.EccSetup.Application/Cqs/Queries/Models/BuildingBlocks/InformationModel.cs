using System;
using System.Collections.Generic;

namespace ITG.Brix.EccSetup.Application.Cqs.Queries.Models.BuildingBlocks
{
    public class InformationModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string NameOnApplication { get; set; }
        public string Description { get; set; }
        public Guid Icon { get; set; }
        public List<TagModel> Tags { get; set; }
    }
}
