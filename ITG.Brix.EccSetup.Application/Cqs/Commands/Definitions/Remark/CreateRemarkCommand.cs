using ITG.Brix.EccSetup.Application.Bases;
using MediatR;
using System;
using System.Collections.Generic;

namespace ITG.Brix.EccSetup.Application.Cqs.Commands.Definitions
{
    public class CreateRemarkCommand : IRequest<Result>
    {
        public string Name { get; private set; }
        public string NameOnApplication { get; private set; }
        public string Description { get; private set; }
        public Guid Icon { get; private set; }
        public List<string> Tags { get; private set; }
        public List<string> DefaultRemarks { get; private set; }

        public CreateRemarkCommand()
        {
        }

        public CreateRemarkCommand(string name,
                                   string nameOnApplication,
                                   string description,
                                   Guid icon,
                                   List<string> tags,
                                   List<string> defaultRemarks) : this()
        {
            Name = name;
            NameOnApplication = nameOnApplication;
            Description = description;
            Icon = icon;
            Tags = tags;
            DefaultRemarks = defaultRemarks;
        }
    }
}
