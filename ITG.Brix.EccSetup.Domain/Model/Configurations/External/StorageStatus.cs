using ITG.Brix.EccSetup.Domain.Bases;
using ITG.Brix.EccSetup.Domain.Internal;
using System;

namespace ITG.Brix.EccSetup.Domain
{
    public class StorageStatus : Entity
    {
        public StorageStatus(Guid id, string code, string name, bool @default, string source)
        {
            if (id == default(Guid))
            {
                throw Error.Argument(string.Format("{0} can't be default guid.", nameof(id)));
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                throw Error.Argument(string.Format("{0} can't be empty.", nameof(name)));
            }

            if (string.IsNullOrWhiteSpace(code))
            {
                throw Error.Argument(string.Format("{0} can't be empty.", nameof(code)));
            }

            if (string.IsNullOrWhiteSpace(source))
            {
                throw Error.Argument(string.Format("{0} can't be empty.", nameof(source)));
            }

            Id = id;
            Code = code;
            Name = name;
            Default = @default;
            Source = source;
        }

        public string Code { get; private set; }
        public string Name { get; private set; }
        public bool Default { get; private set; }
        public string Source { get; private set; }
    }
}
