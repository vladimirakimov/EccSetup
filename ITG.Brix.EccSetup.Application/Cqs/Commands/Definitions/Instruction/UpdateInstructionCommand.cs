using ITG.Brix.EccSetup.Application.Bases;
using MediatR;
using System;
using System.Collections.Generic;

namespace ITG.Brix.EccSetup.Application.Cqs.Commands.Definitions
{
    public class UpdateInstructionCommand : IRequest<Result>
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string Icon { get; private set; }
        public IEnumerable<string> Tags { get; private set; }
        public string Content { get; private set; }
        public string Image { get; private set; }
        public string Video { get; private set; }
        public int Version { get; private set; }

        public UpdateInstructionCommand(Guid id,
                                        string name,
                                        string description,
                                        string icon,
                                        IEnumerable<string> tags,
                                        string content,
                                        string image,
                                        string video,
                                        int version)
        {
            Id = id;
            Name = name;
            Description = description;
            Icon = icon;
            Tags = tags ?? new List<string>();
            Content = content;
            Image = image;
            Video = video;
            Version = version;
        }
    }
}
