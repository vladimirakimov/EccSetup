using ITG.Brix.EccSetup.Application.Bases;
using MediatR;
using System.Collections.Generic;

namespace ITG.Brix.EccSetup.Application.Cqs.Commands.Definitions
{
    public class CreateInstructionCommand : IRequest<Result>
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string Icon { get; private set; }
        public IEnumerable<string> Tags { get; private set; }
        public string Content { get; private set; }
        public string Image { get; private set; }
        public string Video { get; private set; }

        public CreateInstructionCommand(string name,
                                        string description,
                                        string icon,
                                        IEnumerable<string> tags,
                                        string content,
                                        string image,
                                        string video)
        {
            Name = name;
            Description = description;
            Icon = icon;
            Tags = tags ?? new List<string>();
            Content = content;
            Image = image;
            Video = video;
        }
    }
}
