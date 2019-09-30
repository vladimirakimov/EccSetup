using ITG.Brix.EccSetup.Domain;
using System;
using System.Collections.Generic;

namespace ITG.Brix.EccSetup.Application.Cqs.Queries.Models
{
    public class InstructionModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
        public List<Tag> Tags { get; set; }
        public string Content { get; set; }
        public string Image { get; set; }
        public string Video { get; set; }
    }
}
