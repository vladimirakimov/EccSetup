using ITG.Brix.EccSetup.Application.Bases;
using ITG.Brix.EccSetup.Application.Cqs.Commands.Dtos;
using MediatR;
using System.Collections.Generic;

namespace ITG.Brix.EccSetup.Application.Cqs.Commands.Definitions
{
    public class CreateValidationCommand : IRequest<Result>
    {
        public string Name { get; private set; }
        public string NameOnApplication { get; private set; }
        public string Description { get; private set; }
        public string Instruction { get; private set; }
        public BuildingBlockIconDto Icon { get; private set; }
        public IEnumerable<string> Tags { get; private set; }
        public List<ImageDto> Images { get; private set; }
        public List<VideoDto> Videos { get; private set; }


        public CreateValidationCommand(string name,
                                       string nameOnApplication,
                                       string description,
                                       string instruction,
                                       BuildingBlockIconDto icon,
                                       IEnumerable<string> tags,
                                       List<ImageDto> images,
                                       List<VideoDto> videos)
        {
            Name = name;
            NameOnApplication = nameOnApplication;
            Description = description;
            Instruction = instruction;
            Icon = icon;
            Tags = tags ?? new List<string>();
            Images = images;
            Videos = videos;
        }
    }
}
