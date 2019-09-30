using ITG.Brix.EccSetup.API.Context.Bases;
using ITG.Brix.EccSetup.API.Context.Services.Requests.Models;
using ITG.Brix.EccSetup.API.Context.Services.Requests.Validators.Components;
using ITG.Brix.EccSetup.API.Context.Services.Requests.Validators.Impl.Bases;
using System;

namespace ITG.Brix.EccSetup.API.Context.Services.Requests.Validators.Impl
{
    public class CreateProductionSiteRequestValidator : AbstractRequestValidator<CreateProductionSiteRequest>
    {
        private readonly IRequestComponentValidator _requestComponentValidator;

        public CreateProductionSiteRequestValidator(IRequestComponentValidator requestComponentValidator)
        {
            _requestComponentValidator = requestComponentValidator ?? throw new ArgumentNullException(nameof(requestComponentValidator));
        }

        public override ValidationResult Validate<T>(T request)
        {
            var req = request as CreateProductionSiteRequest;

            ValidationResult result;

            result = _requestComponentValidator.QueryApiVersionRequired(req.QueryApiVersion);

            if (result == null)
            {
                result = _requestComponentValidator.QueryApiVersion(req.QueryApiVersion);
            }

            if (result == null)
            {
                result = new ValidationResult();
            }

            return result;
        }
    }
}
