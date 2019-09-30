using ITG.Brix.EccSetup.Application.Bases;
using MediatR;
using System.IO;

namespace ITG.Brix.EccSetup.Application.Cqs.Commands.Definitions.File
{
    public class UploadFileCommand : IRequest<Result>
    {
        public Stream File { get; private set; }
        public string FileExtension { get; private set; }

        public UploadFileCommand(Stream file, string fileExtension)
        {
            File = file;
            FileExtension = fileExtension;
        }
    }
}
