using System;

namespace ITG.Brix.EccSetup.Infrastructure.DataAccess.ClassModels
{
    public class OperatorActivityClass
    {
        public Guid Id { get; set; }
        public string GroupId { get; set; }
        public int Version { get; set; }
        public string Type { get; set; }
        public string Title { get; set; }
        public string Name { get; set; }
        public string Action { get; set; }
        public string Instruction { get; set; }
        public DateTime Created { get; set; }
        public string OperatorLogin { get; set; }
        public string OperatorId { get; set; }
        public string WorkOrderId { get; set; }
        public string Property { get; set; }
        public string Value { get; set; }
        public string SampleValue { get; set; }
        public string Description { get; set; }
        public bool IsValid { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
    }
}
