using System;
using System.Collections.Generic;

namespace ITG.Brix.EccSetup.Application.Cqs.Queries.Models
{
    public class OperationModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ColoredIconModel Icon { get; set; }
        public List<string> Tags { get; set; }
    }
}
