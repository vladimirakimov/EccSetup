using ITG.Brix.EccSetup.API.Context.Services.Requests.Models.File.From;
using System;

namespace ITG.Brix.EccSetup.API.Context.Services.Requests.Models.File
{
    public class DownloadFileRequest
    {
        private readonly DownloadFileFromRoute _route;
        private readonly DownloadFileFromQuery _query;

        public DownloadFileRequest(DownloadFileFromRoute route,
                                   DownloadFileFromQuery query)
        {
            _route = route ?? throw new ArgumentNullException(nameof(route));
            _query = query ?? throw new ArgumentNullException(nameof(query));
        }

        public string QueryApiVersion => _query.ApiVersion;
        public string File => _route.File;
    }
}
