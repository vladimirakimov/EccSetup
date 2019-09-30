using System;
using ITG.Brix.EccSetup.Domain.Bases;
using ITG.Brix.EccSetup.Domain.Internal;

namespace ITG.Brix.EccSetup.Domain
{
    public class TypePlanning : Entity
    {
        public TypePlanning(Guid id, string code, string name, string source)
        {
            if (id == default(Guid))
            {
                throw Error.ArgumentNull(string.Format("{0} can't be default guid.", nameof(id)));
            }

            if (string.IsNullOrWhiteSpace(source))
            {
                throw Error.ArgumentNull(string.Format("{0} can't be empty.", nameof(source)));
            }

            if (string.IsNullOrWhiteSpace(code))
            {
                throw Error.ArgumentNull(string.Format("{0} can't be empty.", nameof(code)));
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                throw Error.ArgumentNull(string.Format("{0} can't be empty.", nameof(name)));
            }

            Id = id;
            Code = code;
            Name = name;
            Source = source;
        }

        public string Code { get; private set; }
        public string Name { get; private set; }
        public string Source { get; private set; }
    }
}
