using ITG.Brix.EccSetup.Domain.Bases;
using ITG.Brix.EccSetup.Domain.Internal;
using System;
using System.Collections.Generic;

namespace ITG.Brix.EccSetup.Domain
{
    public class ChecklistAnswer : ValueObject
    {
        public Guid QuestionId { get; private set; }
        public string Answer { get; private set; }
        public string Image { get; private set; }
        public string Video { get; private set; }
        public string Action { get; private set; }
        public Guid LinksTo { get; private set; }

        public ChecklistAnswer(Guid questionId, string answer, string image, string video, string action, Guid linksTo)
        {
            if (string.IsNullOrEmpty(answer))
            {
                throw Error.ArgumentNull($"{nameof(answer)} can't be empty.");
            }
            QuestionId = questionId;
            Answer = answer;
            Image = image;
            Video = video;
            Action = action;
            LinksTo = linksTo;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return QuestionId;
            yield return Answer;
            yield return Image;
            yield return Video;
            yield return Action;
            yield return LinksTo;
        }
    }
}
