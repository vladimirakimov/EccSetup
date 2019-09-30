using ITG.Brix.EccSetup.Domain.Bases;
using ITG.Brix.EccSetup.Domain.Internal;
using ITG.Brix.EccSetup.Domain.Model.Flows;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ITG.Brix.EccSetup.Domain
{
    public class Flow : Entity
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string Image { get; private set; }
        public string Diagram { get; private set; }
        public string FilterContent { get; private set; }
        public FlowFilter Filter { get; private set; }


        public Flow(Guid id, string name)
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

        public void SetImage(string image)
        {
            Image = image;
        }

        public void SetDiagram(string diagram)
        {
            Diagram = diagram;
        }

        public void SetFilterContent(string filterContent)
        {
            FilterContent = filterContent;
        }

        public void SetFilter(FlowFilter filter)
        {
            Filter = filter;
        }
    }
}
