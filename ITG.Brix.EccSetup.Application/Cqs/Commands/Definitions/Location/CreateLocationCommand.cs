using ITG.Brix.EccSetup.Application.Bases;
using MediatR;

namespace ITG.Brix.EccSetup.Application.Cqs.Commands.Definitions
{
    public class CreateLocationCommand : IRequest<Result>
    {
        public string Source { get; private set; }
        public string Site { get; private set; }
        public string Warehouse { get; private set; }
        public string Gate { get; private set; }
        public string Row { get; private set; }
        public string Position { get; private set; }
        public string Type { get; private set; }
        public string IsRack { get; private set; }

        public CreateLocationCommand(string source, string site, string warehouse, string gate, string row, string position, string type, string isRack)
        {
            Source = source;
            Site = site;
            Warehouse = warehouse;
            Gate = gate;
            Row = row;
            Position = position;
            Type = type;
            IsRack = isRack;
        }
    }
}
