using ITG.Brix.EccSetup.Domain.Internal;
using System;
using System.Collections.Generic;

namespace ITG.Brix.EccSetup.Domain
{
    public class Instruction : BuildingBlock
    {
        private List<Tag> _tags = new List<Tag>();

        public string Name { get; private set; }
        public string Description { get; private set; }
        public string Icon { get; private set; }
        public string Content { get; private set; }
        public string Image { get; private set; }
        public string Video { get; private set; }
        public IReadOnlyCollection<Tag> Tags => _tags.AsReadOnly();

        public Instruction(Guid id, string name, string description, string icon, string content, string image, string video) : base(id, BlockType.Instruction)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw Error.ArgumentNull(string.Format("{0} can't be empty.", nameof(name)));
            }

            Name = name;
            Description = description;
            Icon = icon;
            Content = content;
            Image = image;
            Video = video;
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
