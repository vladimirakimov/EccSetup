using ITG.Brix.EccSetup.Domain.Bases;

namespace ITG.Brix.EccSetup.Domain
{
    public class FreeActionType : Enumeration
    {
        public static readonly FreeActionType Validation = new FreeActionType(1, "Take picture");
        public static readonly FreeActionType Input = new FreeActionType(2, "Add remarks");
        public static readonly FreeActionType Checklist = new FreeActionType(3, "Shift products");
        public static readonly FreeActionType Instruction = new FreeActionType(4, "Search stock");

        public FreeActionType(int id, string name) : base(id, name) { }
    }
}
