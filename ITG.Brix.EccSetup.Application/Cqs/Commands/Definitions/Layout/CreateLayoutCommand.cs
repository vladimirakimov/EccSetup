using ITG.Brix.EccSetup.Application.Bases;
using MediatR;

namespace ITG.Brix.EccSetup.Application.Cqs.Commands.Definitions
{
    public class CreateLayoutCommand : IRequest<Result>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }

        public CreateLayoutCommand(string name,
                                   string description,
                                   string image)
        {
            Name = name;
            Description = description;
            Image = image;
        }
    }
}
