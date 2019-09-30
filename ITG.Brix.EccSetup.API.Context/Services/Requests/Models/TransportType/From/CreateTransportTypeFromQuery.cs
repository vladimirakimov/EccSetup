using Microsoft.AspNetCore.Mvc;

namespace ITG.Brix.EccSetup.API.Context.Services.Requests.Models.From
{
    public class CreateTransportTypeFromQuery
    {
        [FromQuery(Name = "api-version")]
        public string ApiVersion { get; set; }
    }
}
