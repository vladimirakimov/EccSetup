using ITG.Brix.EccSetup.Domain.Bases;
using ITG.Brix.EccSetup.Domain.Internal;
using System;

namespace ITG.Brix.EccSetup.Domain
{
    public class Customer : Entity
    {
        public Customer(Guid id, string code, string name, string source)
        {
            if (id == default(Guid))
            {
                throw Error.Argument(string.Format("{0} can't be default guid.", nameof(id)));
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
            Code = code ?? throw new ArgumentNullException(nameof(code));
            Name = name;
            Source = source ?? throw new ArgumentNullException(nameof(source));
        }

        public string Code { get; set; }
        public string Name { get; set; }
        public string Source { get; set; }

    }
}
