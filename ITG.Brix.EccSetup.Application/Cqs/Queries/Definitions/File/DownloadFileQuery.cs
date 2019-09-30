using ITG.Brix.EccSetup.Application.Bases;
using MediatR;

namespace ITG.Brix.EccSetup.Application.Cqs.Queries.Definitions.File
{
    public class DownloadFileQuery : IRequest<Result>
    {
        public string File { get; private set; }

        public DownloadFileQuery(string file)
        {
            File = file;
        }
    }
}
