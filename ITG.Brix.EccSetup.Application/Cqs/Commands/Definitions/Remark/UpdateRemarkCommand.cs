using ITG.Brix.EccSetup.Application.Bases;
using MediatR;
using System;
using System.Collections.Generic;

namespace ITG.Brix.EccSetup.Application.Cqs.Commands.Definitions
{
    public class UpdateRemarkCommand : IRequest<Result>
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string NameOnApplication { get; private set; }
        public string Description { get; private set; }
        public Guid Icon { get; private set; }
        public List<string> Tags { get; private set; }
        public List<string> DefaultRemarks { get; private set; }
        public int Version { get; set; }

        public UpdateRemarkCommand()
        {
        }

        public UpdateRemarkCommand(Guid id,
                                   string name,
                                   string nameOnApplication,
                                   string description,
                                   Guid icon,
                                   List<string> tags,
                                   List<string> defaultRemarks,
                                   int version) : this()
        {
            Id = id;
            Name = name;
            NameOnApplication = nameOnApplication;
            Description = description;
            Icon = icon;
            Tags = tags;
            DefaultRemarks = defaultRemarks;
            Version = version;
        }
    }
}
