using ITG.Brix.EccSetup.Domain.Bases;
using ITG.Brix.EccSetup.Domain.Internal;
using System;

namespace ITG.Brix.EccSetup.Domain
{
    public class Screen : Entity
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string ImageUrl { get; private set; }
        public ScreenType ScreenType { get; private set; }

        public Screen(Guid id, string name, string description, string imageUrl, ScreenType screenType)
        {
            if (id == default(Guid))
            {
                throw Error.Argument(string.Format("{0} can't be dafault guid.", nameof(id)));
            }

            if (string.IsNullOrEmpty(name))
            {
                throw Error.ArgumentNull(string.Format("{0} can't be empty.", nameof(name)));
            }
            Id = id;
            Name = name;
            Description = description;
            ImageUrl = imageUrl;
            ScreenType = screenType;
        }
    }
}
