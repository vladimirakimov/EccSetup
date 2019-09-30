using ITG.Brix.EccSetup.Domain.Bases;
using ITG.Brix.EccSetup.Domain.Internal;
using System;
using System.Collections.Generic;

namespace ITG.Brix.EccSetup.Domain
{
    public class Operation : Entity
    {
        private List<Tag> _tags = new List<Tag>();

        public string Name { get; private set; }

        public string Description { get; private set; }

        public ColoredIcon Icon { get; private set; }

        public IReadOnlyCollection<Tag> Tags
        {
            get
            {
                return _tags.AsReadOnly();
            }
        }

        public Operation(Guid id, string name)
        {
            if (id == default(Guid))
            {
                throw Error.Argument(string.Format("{0} can't be default guid.", nameof(id)));
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                throw Error.ArgumentNull(string.Format("{0} can't be empty.", nameof(name)));
            }


            Id = id;
            Name = name;
        }

        public void ChangeName(string name)
        {
            Name = name;
        }

        public void SetDescription(string description)
        {
            Description = description;
        }

        public void SetIcon(ColoredIcon icon)
        {
            Icon = icon;
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

        public void ClearTags()
        {
            _tags.Clear();
        }

    }
}
