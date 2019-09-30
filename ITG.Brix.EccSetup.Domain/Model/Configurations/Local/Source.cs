using ITG.Brix.EccSetup.Domain.Bases;
using ITG.Brix.EccSetup.Domain.Internal;
using System;
using System.Collections.Generic;

namespace ITG.Brix.EccSetup.Domain
{
    public class Source : Entity
    {
        private List<SourceBusinessUnit> _sourceBusinessUnits = new List<SourceBusinessUnit>();

        public string Name { get; private set; }

        public string Description { get; private set; }

        public IReadOnlyCollection<SourceBusinessUnit> SourceBusinessUnits
        {
            get
            {
                return _sourceBusinessUnits.AsReadOnly();
            }
        }

        public Source(Guid id, string name, string description)
        {
            if (id == default(Guid))
            {
                throw Error.Argument(string.Format("{0} can't be default guid.", nameof(id)));
            }
            if (string.IsNullOrWhiteSpace(name))
            {
                throw Error.ArgumentNull(string.Format("{0} can't be empty.", nameof(name)));
            }
            if (string.IsNullOrWhiteSpace(description))
            {
                throw Error.ArgumentNull(string.Format("{0} can't be empty.", nameof(description)));
            }

            Id = id;
            Name = name;
            Description = description;
        }

        public void ChangeName(string name)
        {
            Name = name;
        }

        public void ChangeDescription(string description)
        {
            Description = description;
        }

        public void AddSourceBusinessUnit(SourceBusinessUnit businessUnit)
        {
            _sourceBusinessUnits.Add(businessUnit);
        }

        public void RemoveSourceBusinessUnit(SourceBusinessUnit businessUnit)
        {
            _sourceBusinessUnits.Remove(businessUnit);
        }

        public void ClearSourceBusinessUnits()
        {
            _sourceBusinessUnits.Clear();
        }
    }
}
