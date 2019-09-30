using ITG.Brix.EccSetup.API.Context.Services;
using ITG.Brix.EccSetup.API.Context.Services.Requests.Models.OperatorActivities;
using ITG.Brix.EccSetup.API.Context.Services.Requests.Models.OperatorActivity;
using ITG.Brix.EccSetup.API.Context.Services.Requests.Models.OperatorActivity.From;
using ITG.Brix.EccSetup.API.Context.Services.Responses.Models.Errors;
using ITG.Brix.EccSetup.Application.Cqs.Queries.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Net;
using System.Threading.Tasks;

namespace ITG.Brix.EccSetup.API.Controllers
{
    [ApiVersionNeutral]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class OperatorActivitiesController : Controller
    {
        private readonly IApiResult _apiResult;

        public OperatorActivitiesController(IApiResult apiResult)
        {
            _apiResult = apiResult ?? throw new ArgumentNullException(nameof(apiResult));
        }

        [HttpGet]
        [ProducesResponseType(typeof(OperatorActivitiesModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ResponseError), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> List([FromQuery] ListOperatorActivityFromQuery query)
        {
            var request = new ListOperatorActivityRequest(query);

            var result = await _apiResult.Produce(request);

            return result;
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ResponseError), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Create([FromQuery] CreateOperatorActivityFromQuery query,
                                                [FromBody] JObject body)
        {
            var request = new CreateOperatorActivityRequest(query, body);

            var result = await _apiResult.Produce(request);

            return result;
        }
    }
}
