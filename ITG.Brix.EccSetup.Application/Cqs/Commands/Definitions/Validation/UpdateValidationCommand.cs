using ITG.Brix.EccSetup.Application.Bases;
using ITG.Brix.EccSetup.Application.Cqs.Commands.Dtos;
using MediatR;
using System;
using System.Collections.Generic;

namespace ITG.Brix.EccSetup.Application.Cqs.Commands.Definitions
{
    public class UpdateValidationCommand : IRequest<Result>
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string NameOnApplication { get; private set; }
        public string Description { get; private set; }
        public string Instruction { get; private set; }
        public BuildingBlockIconDto Icon { get; private set; }
        public IEnumerable<string> Tags { get; private set; }
        public List<ImageDto> Images { get; private set; }
        public List<VideoDto> Videos { get; private set; }
        public int Version { get; set; }

        public UpdateValidationCommand(Guid id,
                                       string name,
                                       string nameOnApplication,
                                       string description,
                                       string instruction,
                                       BuildingBlockIconDto icon,
                                       IEnumerable<string> tags,
                                       List<ImageDto> images,
                                       List<VideoDto> videos,
                                       int version)
        {
            Id = id;
            Name = name;
            NameOnApplication = nameOnApplication;
            Description = description;
            Instruction = instruction;
            Icon = icon;
            Tags = tags ?? new List<string>();
            Images = images;
            Videos = videos;
            Version = version;
        }
    }
}
