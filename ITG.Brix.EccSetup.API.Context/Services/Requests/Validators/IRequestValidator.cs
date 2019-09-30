using ITG.Brix.EccSetup.API.Context.Bases;
using System;

namespace ITG.Brix.EccSetup.API.Context.Services.Requests.Validators
{
    public interface IRequestValidator
    {
        ValidationResult Validate<T>(T request);

        Type Type { get; }
    }
}
