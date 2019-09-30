using Microsoft.AspNetCore.Http;

namespace ITG.Brix.EccSetup.API.Context.Services.Requests.Models.File.From
{
    public class UploadFileFromForm
    {
        public IFormFile File { get; set; }
    }
}
