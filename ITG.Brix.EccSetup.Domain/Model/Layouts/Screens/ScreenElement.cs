using ITG.Brix.EccSetup.Domain.Bases;
using ITG.Brix.EccSetup.Domain.Internal;
using System;

namespace ITG.Brix.EccSetup.Domain
{
    public class ScreenElement : Entity
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public Icon Icon { get; private set; }
        public bool ShowOperationIcon { get; private set; }
        public ScreenElementType ScreenElementType { get; private set; }
        public string Image { get; private set; }

        public ScreenElement(Guid id, string name, string description, Icon icon, bool showOperationIcon, ScreenElementType screenElementType, string image)
        {
            if (id == default(Guid))
            {
                throw Error.Argument(string.Format("{0} can't be default guid.", nameof(id)));
            }

            if (string.IsNullOrEmpty(name))
            {
                throw Error.ArgumentNull(string.Format("{0} can't be empty.", nameof(name)));
            }

            Id = id;
            Name = name;
            Description = description;
            Icon = icon;
            ShowOperationIcon = showOperationIcon;
            ScreenElementType = screenElementType;
            Image = image;
        }
    }
}
