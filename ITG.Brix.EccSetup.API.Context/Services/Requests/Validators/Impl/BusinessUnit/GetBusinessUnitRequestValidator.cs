﻿using ITG.Brix.EccSetup.API.Context.Bases;
using ITG.Brix.EccSetup.API.Context.Services.Requests.Models;
using ITG.Brix.EccSetup.API.Context.Services.Requests.Validators.Components;
using ITG.Brix.EccSetup.API.Context.Services.Requests.Validators.Impl.Bases;
using System;

namespace ITG.Brix.EccSetup.API.Context.Services.Requests.Validators.Impl
{
    public class GetBusinessUnitRequestValidator : AbstractRequestValidator<GetBusinessUnitRequest>
    {
        private readonly IRequestComponentValidator _requestComponentValidator;

        public GetBusinessUnitRequestValidator(IRequestComponentValidator requestComponentValidator)
        {
            _requestComponentValidator = requestComponentValidator ?? throw new ArgumentNullException(nameof(requestComponentValidator));
        }

        public override ValidationResult Validate<T>(T request)
        {
            var req = request as GetBusinessUnitRequest;

            ValidationResult result;

            result = _requestComponentValidator.RouteId(req.RouteId);

            if (result == null)
            {
                result = _requestComponentValidator.QueryApiVersionRequired(req.QueryApiVersion);
            }

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
