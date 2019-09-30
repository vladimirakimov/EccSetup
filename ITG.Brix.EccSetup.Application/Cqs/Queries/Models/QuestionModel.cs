using ITG.Brix.EccSetup.Domain;
using System;
using System.Collections.Generic;

namespace ITG.Brix.EccSetup.Application.Cqs.Queries.Models
{
    public class QuestionModel
    {
        public Guid Id { get; set; }
        public Guid CheckListId { get; set; }
        public string Content { get; set; }
        public string Introduction { get; set; }
        public string Image { get; set; }
        public string Video { get; set; }
        public QuestionType Type { get; set; }
        public bool Sequence { get; set; }
        public bool ShuffleAnswers { get; set; }
        public bool Required { get; set; }
        public Guid LinksTo { get; set; }
        public List<ChecklistAnswerModel> Answers { get; set; }
    }
}
