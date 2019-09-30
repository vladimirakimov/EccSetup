using System;

namespace ITG.Brix.EccSetup.Domain
{
    public class FreeAction : BuildingBlock
    {
        public FreeActionType ActionType { get; private set; }

        public FreeAction(Guid id, FreeActionType actionType) : base(id, BlockType.FreeAction)
        {
            ActionType = actionType ?? throw new ArgumentNullException(nameof(actionType));
        }
    }
}
