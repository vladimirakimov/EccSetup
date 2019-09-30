using ITG.Brix.EccSetup.Application.Bases;
using MediatR;
using System;
using System.Collections.Generic;

namespace ITG.Brix.EccSetup.Application.Cqs.Commands.Definitions
{
    public class CreateInformationCommand : IRequest<Result>
    {
        public string Name { get; private set; }
        public string NameOnApplication { get; private set; }
        public string Description { get; private set; }
        public Guid Icon { get; private set; }
        public IEnumerable<string> Tags { get; private set; }

        public CreateInformationCommand(string name,
                                        string nameOnApplication,
                                        string description,
                                        Guid icon,
                                        IEnumerable<string> tags)
        {
            Name = name;
            NameOnApplication = nameOnApplication;
            Description = description;
            Icon = icon;
            Tags = tags ?? new List<string>();
        }
    }
}
