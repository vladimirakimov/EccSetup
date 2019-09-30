using ITG.Brix.EccSetup.API.Context.Bases;
using System.Net;

namespace ITG.Brix.EccSetup.API.Context.Services.Responses.Mappers
{
    public interface IHttpStatusCodeMapper
    {
        HttpStatusCode Map(ValidationResult validationResult);
    }
}
