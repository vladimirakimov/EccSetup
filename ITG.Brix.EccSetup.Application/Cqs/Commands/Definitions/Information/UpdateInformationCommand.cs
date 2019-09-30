using ITG.Brix.EccSetup.Application.Bases;
using MediatR;
using System;
using System.Collections.Generic;

namespace ITG.Brix.EccSetup.Application.Cqs.Commands.Definitions
{
    public class UpdateInformationCommand : IRequest<Result>
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string NameOnApplication { get; private set; }
        public string Description { get; private set; }
        public Guid Icon { get; private set; }
        public IEnumerable<string> Tags { get; private set; }
        public int Version { get; private set; }

        public UpdateInformationCommand(Guid id,
                                        string name,
                                        string nameOnApplication,
                                        string description,
                                        Guid icon,
                                        IEnumerable<string> tags,
                                        int version)
        {
            Id = id;
            Name = name;
            NameOnApplication = nameOnApplication;
            Description = description;
            Icon = icon;
            Tags = tags ?? new List<string>();
            Version = version;
        }
    }
}
