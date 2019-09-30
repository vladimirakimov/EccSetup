using ITG.Brix.EccSetup.Domain.Bases;
using ITG.Brix.EccSetup.Domain.Internal;
using System;
using System.Collections.Generic;

namespace ITG.Brix.EccSetup.Domain
{
    public class Question : ValueObject
    {
        private List<ChecklistAnswer> _answers = new List<ChecklistAnswer>();

        public Guid Id { get; private set; }
        public Guid ChecklistId { get; private set; }
        public string Content { get; private set; }
        public string Introduction { get; private set; }
        public string Image { get; private set; }
        public string Video { get; private set; }
        public QuestionType Type { get; private set; }
        public bool Sequence { get; private set; }
        public bool ShuffleAnswers { get; private set; }
        public bool Required { get; private set; }
        public Guid LinksTo { get; private set; }


        public Question(Guid id, Guid checkListId, string content, string introduction, string image, string video, QuestionType questionType, bool sequence, bool shuffleAnswers, bool required, Guid linksTo)
        {
            if (id == default(Guid))
            {
                throw Error.Argument(string.Format("{0} can't be default guid.", nameof(id)));
            }

            if (string.IsNullOrEmpty(content))
            {
                throw Error.ArgumentNull(string.Format("{0} can't be empty.", nameof(content)));
            }

            Id = id;
            ChecklistId = checkListId;
            Content = content;
            Introduction = introduction;
            Image = image;
            Video = video;
            Type = questionType;
            Sequence = sequence;
            ShuffleAnswers = shuffleAnswers;
            Required = required;
            LinksTo = linksTo;
        }

        public IReadOnlyCollection<ChecklistAnswer> Answers => _answers.AsReadOnly();

        public void AddAnswer(ChecklistAnswer answer)
        {
            if (!_answers.Contains(answer))
            {
                _answers.Add(answer);
            }
        }

        public void RemoveAnswer(ChecklistAnswer answer)
        {
            if (_answers.Contains(answer))
            {
                _answers.Remove(answer);
            }
        }

        public void ClearAnswers() => _answers.Clear();

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Id;
            yield return ChecklistId;
            yield return Content;
            yield return Introduction;
            yield return Image;
            yield return Video;
            yield return Type;
            yield return Sequence;
            yield return ShuffleAnswers;
            yield return Required;
            yield return LinksTo;
        }

        public class QuestionBuilder
        {
            private Guid _questionId;
            private Guid _checkListId;
            private string _content;
            private string _introduction;
            private string _image;
            private string _video;
            private QuestionType _questionType;
            private bool _sequence;
            private bool _shuffleAnswers;
            private bool _required;
            private Guid _linksTo;

            public QuestionBuilder WithQuestionId(Guid value)
            {
                _questionId = value;
                return this;
            }

            public QuestionBuilder WithChecklistId(Guid value)
            {
                _checkListId = value;
                return this;
            }

            public QuestionBuilder WithContent(string value)
            {
                _content = value;
                return this;
            }

            public QuestionBuilder WithIntroduction(string value)
            {
                _introduction = value;
                return this;
            }

            public QuestionBuilder WithImage(string value)
            {
                _image = value;
                return this;
            }

            public QuestionBuilder WithVideo(string value)
            {
                _video = value;
                return this;
            }

            public QuestionBuilder WithQuestionType(QuestionType value)
            {
                _questionType = value;
                return this;
            }

            public QuestionBuilder WithSequence(bool value)
            {
                _sequence = value;
                return this;
            }

            public QuestionBuilder WithShuffleAnswers(bool value)
            {
                _shuffleAnswers = value;
                return this;
            }

            public QuestionBuilder WithRequired(bool value)
            {
                _required = value;
                return this;
            }

            public QuestionBuilder WithLinksTo(Guid value)
            {
                _linksTo = value;
                return this;
            }

            public Question Build()
            {
                return new Question(id: _questionId,
                    checkListId: _checkListId,
                    image: _image,
                    introduction: _introduction,
                    linksTo: _linksTo,
                    content: _content,
                    questionType: _questionType,
                    required: _required,
                    sequence: _sequence,
                    shuffleAnswers: _shuffleAnswers,
                    video: _video);
            }
        }
    }
}
