using ITG.Brix.EccSetup.Domain.Internal;
using System;
using System.Collections.Generic;

namespace ITG.Brix.EccSetup.Domain
{
    public class Remark : BuildingBlock
    {
        private List<DefaultRemark> _defaultRemarks = new List<DefaultRemark>();
        private List<Tag> _tags = new List<Tag>();

        public string Name { get; private set; }
        public string NameOnApplication { get; private set; }
        public string Description { get; private set; }
        public RemarkIcon Icon { get; private set; }
        public IReadOnlyCollection<DefaultRemark> DefaultRemarks => _defaultRemarks.AsReadOnly();
        public IReadOnlyCollection<Tag> Tags => _tags.AsReadOnly();

        public Remark(Guid id, string name, string nameOnApplication, string description, RemarkIcon icon) : base(id, BlockType.Remark)
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


        public void AddDefaultRemark(DefaultRemark defaultRemark)
        {
            if (!_defaultRemarks.Contains(defaultRemark))
            {
                _defaultRemarks.Add(defaultRemark);
            }
        }

        public void RemoveDefaultRemark(DefaultRemark defaultRemark)
        {
            if (_defaultRemarks.Contains(defaultRemark))
            {
                _defaultRemarks.Remove(defaultRemark);
            }
        }

        public void ClearDefaultRemarks() => _defaultRemarks.Clear();
    }
}
