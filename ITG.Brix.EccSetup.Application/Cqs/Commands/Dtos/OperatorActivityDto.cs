using System;

namespace ITG.Brix.EccSetup.Application.Cqs.Commands.Dtos
{
    public class OperatorActivityDto
    {
        public Guid GroupId { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Action { get; set; }
        public string Instruction { get; set; }
        public DateTime Created { get; set; }
        public string OperatorLogin { get; set; }
        public Guid OperatorId { get; set; }
        public Guid WorkOrderId { get; set; }
        public string Property { get; set; }
        public string Value { get; set; }
        public string SampleValue { get; set; }
        public string Description { get; set; }
        public bool IsValid { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
    }
}
