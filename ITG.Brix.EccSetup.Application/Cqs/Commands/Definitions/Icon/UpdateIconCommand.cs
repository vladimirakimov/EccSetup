using ITG.Brix.EccSetup.Application.Bases;
using ITG.Brix.EccSetup.Application.DataTypes;
using MediatR;
using System;

namespace ITG.Brix.EccSetup.Application.Cqs.Commands.Definitions
{
    public class UpdateIconCommand : IRequest<Result>
    {
        public Guid Id { get; private set; }
        public Optional<string> Name { get; private set; }
        public Optional<string> DataPath { get; private set; }
        public int Version { get; private set; }

        public UpdateIconCommand(Guid id,
                                 Optional<string> name,
                                 Optional<string> dataPath,
                                 int version)
        {
            Id = id;
            Name = name;
            DataPath = dataPath;
            Version = version;
        }
    }
}
