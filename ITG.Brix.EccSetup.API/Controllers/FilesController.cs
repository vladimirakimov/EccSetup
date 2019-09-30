using ITG.Brix.EccSetup.API.Context.Services;
using ITG.Brix.EccSetup.API.Context.Services.Requests.Models.File;
using ITG.Brix.EccSetup.API.Context.Services.Requests.Models.File.From;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace ITG.Brix.EccSetup.API.Controllers
{
    [ApiVersionNeutral]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class FilesController : Controller
    {
        private readonly IApiResult _apiResult;

        public FilesController(IApiResult apiResult)
        {
            _apiResult = apiResult ?? throw new ArgumentNullException(nameof(apiResult));
        }

        [HttpPost]
        [Route("upload")]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.UnsupportedMediaType)]
        public async Task<IActionResult> Upload([FromQuery] UploadFileFromQuery query,
                                                [FromForm] UploadFileFromForm body)
        {
            var request = new UploadFileRequest(query, body);
            var result = await _apiResult.Produce(request);

            return result;
        }

        [HttpGet]
        [Route("download/{file}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]      
        public async Task<IActionResult> Download([FromRoute] DownloadFileFromRoute route,
                                                  [FromQuery] DownloadFileFromQuery query)
        {
            var request = new DownloadFileRequest(route, query);
            var result = await _apiResult.Produce(request);

            return result;
        }
    }
}
