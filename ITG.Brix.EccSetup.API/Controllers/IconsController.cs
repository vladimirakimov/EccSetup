using ITG.Brix.EccSetup.API.Context.Services;
using ITG.Brix.EccSetup.API.Context.Services.Requests.Models;
using ITG.Brix.EccSetup.API.Context.Services.Requests.Models.From;
using ITG.Brix.EccSetup.API.Context.Services.Responses.Models.Errors;
using ITG.Brix.EccSetup.API.Extensions;
using ITG.Brix.EccSetup.Application.Cqs.Queries.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;

namespace ITG.Brix.EccSetup.API.Controllers
{
    [ApiVersionNeutral]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class IconsController : Controller
    {
        private readonly IApiResult _apiResult;

        public IconsController(IApiResult apiResult)
        {
            _apiResult = apiResult ?? throw new ArgumentNullException(nameof(apiResult));
        }

        [HttpGet]
        [ProducesResponseType(typeof(IconsModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ResponseError), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> List([FromQuery] ListIconFromQuery query)
        {
            var request = new ListIconRequest(query);

            var result = await _apiResult.Produce(request);

            return result;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(IconModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ResponseError), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ResponseError), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Get([FromRoute] GetIconFromRoute route,
                                             [FromQuery] GetIconFromQuery query)
        {
            var request = new GetIconRequest(route, query);

            var result = await _apiResult.Produce(request);

            return result;
        }


        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.UnsupportedMediaType)]
        [ProducesResponseType((int)HttpStatusCode.Conflict)]
        [ProducesResponseType(typeof(ResponseError), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Create([FromQuery] CreateIconFromQuery query,
                                                [FromBody] CreateIconFromBody body)
        {
            var request = new CreateIconRequest(query, body);

            var result = await _apiResult.Produce(request);

            return result;
        }

        [HttpPatch("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.UnsupportedMediaType)]
        [ProducesResponseType((int)HttpStatusCode.Conflict)]
        [ProducesResponseType(typeof(ResponseError), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ResponseError), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ResponseError), (int)HttpStatusCode.PreconditionFailed)]
        public async Task<IActionResult> Update([FromRoute] UpdateIconFromRoute route,
                                                [FromQuery] UpdateIconFromQuery query,
                                                [FromHeader] UpdateIconFromHeader header)
        {

            string bodyAsString = await Request.GetRawBodyStringAsync();
            var body = new UpdateIconFromBody { Patch = bodyAsString };

            var request = new UpdateIconRequest(route, query, header, body);

            var result = await _apiResult.Produce(request);

            return result;
        }

        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(ResponseError), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ResponseError), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ResponseError), (int)HttpStatusCode.PreconditionFailed)]
        public async Task<IActionResult> Delete([FromRoute] DeleteIconFromRoute route,
                                                [FromQuery] DeleteIconFromQuery query,
                                                [FromHeader] DeleteIconFromHeader header)
        {
            var request = new DeleteIconRequest(route, query, header);

            var result = await _apiResult.Produce(request);

            return result;
        }
    }
}