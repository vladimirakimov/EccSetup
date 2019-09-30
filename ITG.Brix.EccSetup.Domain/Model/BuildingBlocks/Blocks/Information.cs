using ITG.Brix.EccSetup.Domain.Internal;
using System;
using System.Collections.Generic;

namespace ITG.Brix.EccSetup.Domain
{
    public class Information : BuildingBlock
    {
        private List<Tag> _tags = new List<Tag>();

        public string Name { get; private set; }
        public string NameOnApplication { get; private set; }
        public string Description { get; private set; }
        public Guid Icon { get; private set; }
        public IReadOnlyCollection<Tag> Tags => _tags.AsReadOnly();

        public Information(Guid id, string name, string nameOnApplication, string description, Guid icon) : base(id, BlockType.InformationPopup)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw Error.ArgumentNull(string.Format("{0} can't be empty.", nameof(name)));
            }

            Name = name;
            NameOnApplication = nameOnApplication;
            Description = description;
            Icon = icon;
        }

        public void ChangeName(string name)
        {
            Name = name;
        }

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
