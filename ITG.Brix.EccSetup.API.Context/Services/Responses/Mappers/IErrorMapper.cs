using ITG.Brix.EccSetup.API.Context.Bases;
using ITG.Brix.EccSetup.API.Context.Services.Responses.Models.Errors;

namespace ITG.Brix.EccSetup.API.Context.Services.Responses.Mappers
{
    public interface IErrorMapper
    {
        ResponseError Map(ValidationResult validationResult);
    }
}
