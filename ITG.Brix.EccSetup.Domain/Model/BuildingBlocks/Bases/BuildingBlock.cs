using ITG.Brix.EccSetup.Domain.Bases;
using ITG.Brix.EccSetup.Domain.Internal;
using System;

namespace ITG.Brix.EccSetup.Domain
{
    public class BuildingBlock : Entity
    {
        public BlockType BlockType { get; protected set; }

        public BuildingBlock(Guid id, BlockType blockType)
        {
            if (id == default(Guid))
            {
                throw Error.Argument(string.Format("{0} can't be default guid.", nameof(id)));
            }

            Id = id;
            BlockType = blockType;
        }
    }
}
