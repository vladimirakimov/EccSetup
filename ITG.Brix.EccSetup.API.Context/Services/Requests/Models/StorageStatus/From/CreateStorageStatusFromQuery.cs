using Microsoft.AspNetCore.Mvc;

namespace ITG.Brix.EccSetup.API.Context.Services.Requests.Models.From
{
    public class CreateStorageStatusFromQuery
    {
        [FromQuery(Name = "api-version")]
        public string ApiVersion { get; set; }
    }
}
