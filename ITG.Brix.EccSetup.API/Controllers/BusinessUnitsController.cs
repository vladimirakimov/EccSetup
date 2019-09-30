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
    public class BusinessUnitsController : Controller
    {
        private readonly IApiResult _apiResult;

        public BusinessUnitsController(IApiResult apiResult)
        {
            _apiResult = apiResult ?? throw new ArgumentNullException(nameof(apiResult));
        }

        [HttpGet]
        [ProducesResponseType(typeof(BusinessUnitsModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ResponseError), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> List([FromQuery] ListBusinessUnitFromQuery query)
        {
            var request = new ListBusinessUnitRequest(query);

            var result = await _apiResult.Produce(request);

            return result;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(BusinessUnitModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ResponseError), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ResponseError), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Get([FromRoute] GetBusinessUnitFromRoute route,
                                             [FromQuery] GetBusinessUnitFromQuery query)
        {
            var request = new GetBusinessUnitRequest(route, query);

            var result = await _apiResult.Produce(request);

            return result;
        }


        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.UnsupportedMediaType)]
        [ProducesResponseType((int)HttpStatusCode.Conflict)]
        [ProducesResponseType(typeof(ResponseError), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Create([FromQuery] CreateBusinessUnitFromQuery query,
                                                [FromBody] CreateBusinessUnitFromBody body)
        {
            var request = new CreateBusinessUnitRequest(query, body);

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
        public async Task<IActionResult> Update([FromRoute] UpdateBusinessUnitFromRoute route,
                                                [FromQuery] UpdateBusinessUnitFromQuery query,
                                                [FromHeader] UpdateBusinessUnitFromHeader header)
        {

            string bodyAsString = await Request.GetRawBodyStringAsync();
            var body = new UpdateBusinessUnitFromBody { Patch = bodyAsString };

            var request = new UpdateBusinessUnitRequest(route, query, header, body);

            var result = await _apiResult.Produce(request);

            return result;
        }

        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(ResponseError), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ResponseError), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ResponseError), (int)HttpStatusCode.PreconditionFailed)]
        public async Task<IActionResult> Delete([FromRoute] DeleteBusinessUnitFromRoute route,
                                                [FromQuery] DeleteBusinessUnitFromQuery query,
                                                [FromHeader] DeleteBusinessUnitFromHeader header)
        {
            var request = new DeleteBusinessUnitRequest(route, query, header);

            var result = await _apiResult.Produce(request);

            return result;
        }
    }
}