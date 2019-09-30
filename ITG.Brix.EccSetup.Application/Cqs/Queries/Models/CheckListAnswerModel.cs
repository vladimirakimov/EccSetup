using System;

namespace ITG.Brix.EccSetup.Application.Cqs.Queries.Models
{
    public class ChecklistAnswerModel
    {
        public Guid QuestionId { get; set; }
        public string Answer { get; set; }
        public string Image { get; set; }
        public string Video { get; set; }
        public string Action { get; set; }
        public Guid LinksTo { get; set; }
    }
}
