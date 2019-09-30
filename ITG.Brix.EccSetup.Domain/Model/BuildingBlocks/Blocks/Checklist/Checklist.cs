using ITG.Brix.EccSetup.Domain.Bases;
using ITG.Brix.EccSetup.Domain.Internal;
using System;
using System.Collections.Generic;

namespace ITG.Brix.EccSetup.Domain
{
    public class Checklist : BuildingBlock, IAggregateRoot
    {
        private List<Question> _questions = new List<Question>();
        private List<Tag> _tags = new List<Tag>();

        public string Name { get; private set; }
        public string Description { get; private set; }
        public Guid Icon { get; private set; }
        public bool ShuffleQuestions { get; private set; }
        public IReadOnlyCollection<Question> Questions => _questions.AsReadOnly();
        public IReadOnlyCollection<Tag> Tags => _tags.AsReadOnly();

        public Checklist(Guid id, string name, string description, Guid icon, bool shuffleQuestions) : base(id, BlockType.Checklist)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw Error.ArgumentNull(string.Format("{0} can't be empty.", nameof(name)));
            }

            Name = name;
            Description = description;
            Icon = icon;
            ShuffleQuestions = shuffleQuestions;
        }

        public void ChangeName(string name)
        {
            Name = name;
        }

        public void AddQuestion(Question question)
        {
            if (!_questions.Contains(question))
            {
                _questions.Add(question);
            }
        }

        public void RemoveQuestion(Question question)
        {
            if (_questions.Contains(question))
            {
                _questions.Remove(question);
            }
        }

        public void ClearQuestions() => _questions.Clear();

        public void AddTag(Tag tag)
        {
            if (!_tags.Contains(tag))
            {
                _tags.Add(tag);
            }
        }

        public void RemoveTag(Tag tag)
        {
            if (_tags.Contains(tag))
            {
                _tags.Remove(tag);
            }
        }

        public void ClearTags() => _tags.Clear();
    }
}
