using System;
using ITG.Brix.EccSetup.Domain.Bases;
using ITG.Brix.EccSetup.Domain.Internal;

namespace ITG.Brix.EccSetup.Domain
{
    public class Location : Entity
    {
        public Location() { }
        public Location(Guid id, string source, string site, string warehouse, string gate, string row, string position, string type, bool isRack)
        {
            if (id == default(Guid))
            {
                throw Error.ArgumentNull(string.Format("{0} can't be default guid.", nameof(id)));
            }

            if (string.IsNullOrWhiteSpace(source))
            {
                throw Error.ArgumentNull(string.Format("{0} can't be empty.", nameof(source)));
            }

            if (string.IsNullOrWhiteSpace(site))
            {
                throw Error.ArgumentNull(string.Format("{0} can't be empty.", nameof(site)));
            }

            if (string.IsNullOrWhiteSpace(warehouse))
            {
                throw Error.ArgumentNull(string.Format("{0} can't be empty.", nameof(warehouse)));
            }

            if (string.IsNullOrWhiteSpace(gate))
            {
                throw Error.ArgumentNull(string.Format("{0} can't be empty.", nameof(gate)));
            }

            if (string.IsNullOrWhiteSpace(row))
            {
                throw Error.ArgumentNull(string.Format("{0} can't be empty.", nameof(row)));
            }

            if (string.IsNullOrWhiteSpace(position))
            {
                throw Error.ArgumentNull(string.Format("{0} can't be empty.", nameof(position)));
            }

            if (string.IsNullOrWhiteSpace(type))
            {
                throw Error.ArgumentNull(string.Format("{0} can't be empty.", nameof(type)));
            }

            Id = id;
            Source = source;
            Site = site;
            Warehouse = warehouse;
            Gate = gate;
            Row = row;
            Position = position;
            Type = type;
            IsRack = isRack;
        }

        public string Source { get; private set; }
        public string Site { get; private set; }
        public string Warehouse { get; private set; }
        public string Gate { get; private set; }
        public string Row { get; private set; }
        public string Position { get; private set; }
        public string Type { get; private set; }
        public bool IsRack { get; private set; }
    }
}
