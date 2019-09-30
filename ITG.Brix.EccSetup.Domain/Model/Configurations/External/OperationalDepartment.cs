using ITG.Brix.EccSetup.Domain.Bases;
using ITG.Brix.EccSetup.Domain.Internal;
using System;
using System.Collections.Generic;

namespace ITG.Brix.EccSetup.Domain
{
    public class OperationalDepartment : Entity
    {
        public string Code { get; private set; }
        public string Name { get; private set; }
        public string Site { get; private set; }
        public string Source { get; private set; }

        public OperationalDepartment(Guid id, string code, string name, string site, string source)
        {
            if (id == default(Guid))
            {
                throw Error.ArgumentNull(string.Format("{0} can't be default guid.", nameof(id)));
            }

            if (string.IsNullOrWhiteSpace(code))
            {
                throw Error.ArgumentNull(string.Format("{0} can't be empty.", nameof(code)));
            }

            if (string.IsNullOrWhiteSpace(source))
            {
                throw Error.ArgumentNull(string.Format("{0} can't be empty.", nameof(source)));
            }

            if (string.IsNullOrWhiteSpace(site))
            {
                throw Error.ArgumentNull(string.Format("{0} can't be empty.", nameof(site)));
            }

            Id = id;
            Code = code;
            Name = name;
            Site = site;
            Source = source;
        }
    }
}
