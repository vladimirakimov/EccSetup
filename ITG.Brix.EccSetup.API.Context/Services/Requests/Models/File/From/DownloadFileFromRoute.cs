using Microsoft.AspNetCore.Mvc;

namespace ITG.Brix.EccSetup.API.Context.Services.Requests.Models.File.From
{
    public class DownloadFileFromRoute
    {
        [FromRoute(Name = "file")]
        public string File { get; set; }
    }
}
