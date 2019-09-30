using ITG.Brix.EccSetup.Domain.Bases;

namespace ITG.Brix.EccSetup.Domain
{
    public class Behavior : Enumeration
    {
        public static readonly Behavior BlockOrder = new Behavior(1, "Block order");
        public static readonly Behavior InformSupervisor = new Behavior(2, "Inform supervisor");

        public Behavior(int id, string name) : base(id, name) { }
    }
}
