using ITG.Brix.EccSetup.Domain.Bases;
using System;

namespace ITG.Brix.EccSetup.Domain.Model.OperatorActivities
{
    public class OperatorActivity : Entity
    {
        public Guid GroupId { get; set; }
        public BlockType Type { get; private set; }
        public string Name { get; private set; }
        public string Title { get; private set; }
        public string Action { get; private set; }
        public string Instruction { get; private set; }
        public DateTime Created { get; private set; }
        public string OperatorLogin { get; private set; }
        public Guid OperatorId { get; private set; }
        public Guid WorkOrderId { get; private set; }
        public string Property { get; private set; }
        public string Value { get; private set; }
        public string SampleValue { get; private set; }
        public string Description { get; private set; }
        public bool IsValid { get; private set; }
        public string Question { get; private set; }
        public string Answer { get; private set; }

        public OperatorActivity(Guid id)
        {
            Id = id;
        }

        public void SetType(BlockType type)
        {
            Type = type;
        }

        public void SetTitle(string title)
        {
            Title = title;
        }

        public void SetName(string name)
        {
            Name = name;
        }

        public void SetCreated(DateTime created)
        {
            Created = created;
        }

        public void SetOperatorLogin(string operatorLogin)
        {
            OperatorLogin = operatorLogin;
        }

        public void SetOperatorId(Guid operatorId)
        {
            OperatorId = operatorId;
        }

        public void SetWorkOrderId(Guid workOrderId)
        {
            WorkOrderId = workOrderId;
        }

        public void SetDescription(string description)
        {
            Description = description;
        }

        public void SetIsValid(bool isValid)
        {
            IsValid = isValid;
        }

        public void SetQuestion(string question)
        {
            Question = question;
        }

        public void SetAnswer(string answer)
        {
            Answer = answer;
        }

        public OperatorActivity() { }
    }
}
