using Microsoft.AspNetCore.Mvc;

namespace ITG.Brix.EccSetup.API.Context.Services.Requests.Models.Customer.From
{
    public class ListCustomerFromQuery
    {
        [FromQuery(Name = "api-version")]
        public string ApiVersion { get; set; }

        [FromQuery(Name = "$filter")]
        public string Filter { get; set; }

        [FromQuery(Name = "$top")]
        public string Top { get; set; }

        [FromQuery(Name = "$skip")]
        public string Skip { get; set; }
    }
}
