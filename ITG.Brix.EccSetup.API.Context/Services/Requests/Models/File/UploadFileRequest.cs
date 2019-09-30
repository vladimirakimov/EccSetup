using ITG.Brix.EccSetup.API.Context.Services.Requests.Models.File.From;
using System;
using System.IO;

namespace ITG.Brix.EccSetup.API.Context.Services.Requests.Models.File
{
    public class UploadFileRequest
    {
        private readonly UploadFileFromQuery _query;
        private readonly UploadFileFromForm _body;

        public UploadFileRequest(UploadFileFromQuery query,
                                 UploadFileFromForm body)
        {
            _query = query ?? throw new ArgumentNullException(nameof(query));
            _body = body ?? throw new ArgumentNullException(nameof(body));
        }

        public string QueryApiVersion => _query.ApiVersion;

        public Stream File => _body.File.OpenReadStream();

        public string FileExtension => Path.GetExtension(_body.File.FileName);
    }
}
