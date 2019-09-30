using ITG.Brix.EccSetup.Domain.Bases;
using ITG.Brix.EccSetup.Domain.Internal;
using ITG.Brix.EccSetup.Domain.ValueObjects.Enumerations;
using System;
using System.Collections.Generic;

namespace ITG.Brix.EccSetup.Domain
{
    public class WorkOrderButton : Entity
    {
        public List<string> WorkOrderAttribute { get; private set; }
        public bool Highlight { get; private set; }
        public bool ShowCaption { get; private set; }
        public int SortSequence { get; private set; }
        public SortOrderEnum SortOrder { get; private set; }
        public bool HideOnButton { get; private set; }

        public WorkOrderButton(Guid id, List<string> workOrderAttribute, bool highlight, bool showCaption, int sortSequence, SortOrderEnum sortOrder, bool hideOnButton)
        {
            if (id == default(Guid))
            {
                throw Error.Argument(string.Format("{0} can't be default guid.", nameof(id)));
            }

            if (workOrderAttribute == null)
            {
                throw Error.ArgumentNull(string.Format("{0} can't be empty.", nameof(workOrderAttribute)));
            }

            Id = id;
            WorkOrderAttribute = workOrderAttribute;
            Highlight = highlight;
            ShowCaption = showCaption;
            SortSequence = sortSequence;
            SortOrder = sortOrder;
            HideOnButton = hideOnButton;
        }
    }
}
