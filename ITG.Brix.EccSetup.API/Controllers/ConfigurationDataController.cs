using ITG.Brix.EccSetup.Application.Cqs.Commands.Definitions;
using ITG.Brix.EccSetup.Application.Cqs.Queries.Definitions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ITG.Brix.EccSetup.API.Controllers
{
    [Produces("application/xml")]
    [Route("api/[controller]")]
    public class ConfigurationDataController : Controller
    {
        private readonly IMediator _mediator;

        public ConfigurationDataController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException();
        }

        [HttpGet]
        public async Task<IActionResult> GetData(GetConfigurationDataQuery query)
        {
            var result = await _mediator.Send(query);
            return Json(result);
        }

        [HttpPost]
        [Produces("application/xml")]
        public async Task<IActionResult> SyncData([FromBody]XElement xmlElement)
        {
            var command = new SyncConfigurationDataCommand
            {
                XmlElement = xmlElement
            };

            var result = await _mediator.Send(command);
            return Json(result);
        }
    }
}