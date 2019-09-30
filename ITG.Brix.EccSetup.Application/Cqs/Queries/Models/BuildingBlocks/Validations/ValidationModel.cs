using ITG.Brix.EccSetup.Domain;
using System;

namespace ITG.Brix.EccSetup.Application.Cqs.Queries.Models.BuildingBlocks.Validations
{
    public class ValidationModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string NameOnApplication { get; set; }
        public string Description { get; set; }
        public string Instruction { get; set; }
        public BlockType BlockType { get; set; }
    }
}
