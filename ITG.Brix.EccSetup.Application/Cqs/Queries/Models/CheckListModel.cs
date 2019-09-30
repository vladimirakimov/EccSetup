using System;
using System.Collections.Generic;

namespace ITG.Brix.EccSetup.Application.Cqs.Queries.Models
{
    public class ChecklistModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid Icon { get; set; }
        public List<TagModel> Tags { get; set; }
        public bool ShuffleQuestions { get; set; }
        public List<QuestionModel> Questions { get; set; }
    }
}
