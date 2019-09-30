using System;
using System.Collections.Generic;

namespace ITG.Brix.EccSetup.Application.Cqs.Queries.Models
{
    public class InputModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid Icon { get; set; }
        public List<TagModel> Tags { get; set; }
        public string Instruction { get; set; }
        public List<ImageModel> Images { get; set; }
        public List<VideoModel> Videos { get; set; }
    }
}
