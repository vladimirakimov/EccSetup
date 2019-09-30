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
    public class FlowsController : Controller
    {
        private readonly IApiResult _apiResult;

        public FlowsController(IApiResult apiResult)
        {
            _apiResult = apiResult ?? throw new ArgumentNullException(nameof(apiResult));
        }

        [HttpGet]
        [ProducesResponseType(typeof(FlowsModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ResponseError), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> List([FromQuery] ListFlowFromQuery query)
        {
            var request = new ListFlowRequest(query);

            var result = await _apiResult.Produce(request);

            return result;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(FlowModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ResponseError), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ResponseError), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Get([FromRoute] GetFlowFromRoute route,
                                             [FromQuery] GetFlowFromQuery query)
        {
            var request = new GetFlowRequest(route, query);

            var result = await _apiResult.Produce(request);

            return result;
        }


        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.UnsupportedMediaType)]
        [ProducesResponseType((int)HttpStatusCode.Conflict)]
        [ProducesResponseType(typeof(ResponseError), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Create([FromQuery] CreateFlowFromQuery query,
                                                [FromBody] CreateFlowFromBody body)
        {
            var request = new CreateFlowRequest(query, body);

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
        public async Task<IActionResult> Update([FromRoute] UpdateFlowFromRoute route,
                                                [FromQuery] UpdateFlowFromQuery query,
                                                [FromHeader] UpdateFlowFromHeader header)
        {

            string bodyAsString = await Request.GetRawBodyStringAsync();
            var body = new UpdateFlowFromBody { Patch = bodyAsString };

            var request = new UpdateFlowRequest(route, query, header, body);

            var result = await _apiResult.Produce(request);

            return result;
        }

        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(ResponseError), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ResponseError), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ResponseError), (int)HttpStatusCode.PreconditionFailed)]
        public async Task<IActionResult> Delete([FromRoute] DeleteFlowFromRoute route,
                                                [FromQuery] DeleteFlowFromQuery query,
                                                [FromHeader] DeleteFlowFromHeader header)
        {
            var request = new DeleteFlowRequest(route, query, header);

            var result = await _apiResult.Produce(request);

            return result;
        }
    }
}
