using ITG.Brix.EccSetup.Domain.Bases;
using System.Collections.Generic;

namespace ITG.Brix.EccSetup.Domain
{
    public class BlockType : Enumeration
    {
        public static readonly BlockType Validation = new BlockType(1, "Validation");
        public static readonly BlockType Input = new BlockType(2, "Input");
        public static readonly BlockType Checklist = new BlockType(3, "Checklist");
        public static readonly BlockType Instruction = new BlockType(4, "Instruction");
        public static readonly BlockType FreeAction = new BlockType(5, "Free Action");
        public static readonly BlockType InformationPopup = new BlockType(6, "Information Popup");
        public static readonly BlockType Remark = new BlockType(7, "Remark");
        public static readonly BlockType RemarksInput = new BlockType(8, "RemarksInput");

        public BlockType(int id, string name) : base(id, name) { }

        public static IEnumerable<BlockType> List()
        {
            return new[] { Validation, Input, Checklist, Instruction, FreeAction, InformationPopup, Remark };
        }
    }
}
