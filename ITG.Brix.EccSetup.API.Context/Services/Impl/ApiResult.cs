using ITG.Brix.EccSetup.API.Context.Services.Arrangements;
using ITG.Brix.EccSetup.API.Context.Services.Requests.Models;
using ITG.Brix.EccSetup.API.Context.Services.Requests.Models.Customer;
using ITG.Brix.EccSetup.API.Context.Services.Requests.Models.File;
using ITG.Brix.EccSetup.API.Context.Services.Requests.Models.OperatorActivities;
using ITG.Brix.EccSetup.API.Context.Services.Requests.Models.OperatorActivity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ITG.Brix.EccSetup.API.Context.Services.Impl
{
    public class ApiResult : IApiResult
    {
        private readonly IValidationArrangement _validationArrangement;
        private readonly IOperationArrangement _operationArrangement;

        public ApiResult(IValidationArrangement validationArrangement,
                         IOperationArrangement operationArrangement)
        {
            _validationArrangement = validationArrangement ?? throw new ArgumentNullException(nameof(validationArrangement));
            _operationArrangement = operationArrangement ?? throw new ArgumentNullException(nameof(operationArrangement));
        }

        #region StorageStatus
        public async Task<IActionResult> Produce(ListStorageStatusRequest request)
        {
            var validatorActionResult = await _validationArrangement.Validate(request);
            var actionResult = await _operationArrangement.Process(request, validatorActionResult);

            return actionResult;
        }

        public async Task<IActionResult> Produce(CreateStorageStatusRequest request)
        {
            var validatorActionResult = await _validationArrangement.Validate(request);
            var actionResult = await _operationArrangement.Process(request, validatorActionResult);

            return actionResult;
        }
        #endregion

        #region DamageCode
        public async Task<IActionResult> Produce(ListDamageCodeRequest request)
        {
            var validatorActionResult = await _validationArrangement.Validate(request);
            var actionResult = await _operationArrangement.Process(request, validatorActionResult);

            return actionResult;
        }

        public async Task<IActionResult> Produce(CreateDamageCodeRequest request)
        {
            var validatorActionResult = await _validationArrangement.Validate(request);
            var actionResult = await _operationArrangement.Process(request, validatorActionResult);

            return actionResult;
        }
        #endregion

        #region TypePlanning
        public async Task<IActionResult> Produce(ListTypePlanningRequest request)
        {
            var validatorActionResult = await _validationArrangement.Validate(request);
            var actionResult = await _operationArrangement.Process(request, validatorActionResult);

            return actionResult;
        }

        public async Task<IActionResult> Produce(CreateTypePlanningRequest request)
        {
            var validatorActionResult = await _validationArrangement.Validate(request);
            var actionResult = await _operationArrangement.Process(request, validatorActionResult);

            return actionResult;
        }
        #endregion

        #region ProductionSite
        public async Task<IActionResult> Produce(ListProductionSiteRequest request)
        {
            var validatorActionResult = await _validationArrangement.Validate(request);
            var actionResult = await _operationArrangement.Process(request, validatorActionResult);

            return actionResult;
        }

        public async Task<IActionResult> Produce(CreateProductionSiteRequest request)
        {
            var validatorActionResult = await _validationArrangement.Validate(request);
            var actionResult = await _operationArrangement.Process(request, validatorActionResult);

            return actionResult;
        }
        #endregion

        #region TransportType
        public async Task<IActionResult> Produce(ListTransportTypeRequest request)
        {
            var validatorActionResult = await _validationArrangement.Validate(request);
            var actionResult = await _operationArrangement.Process(request, validatorActionResult);

            return actionResult;
        }

        public async Task<IActionResult> Produce(CreateTransportTypeRequest request)
        {
            var validatorActionResult = await _validationArrangement.Validate(request);
            var actionResult = await _operationArrangement.Process(request, validatorActionResult);

            return actionResult;
        }
        #endregion

        #region Site
        public async Task<IActionResult> Produce(ListSiteRequest request)
        {
            var validatorActionResult = await _validationArrangement.Validate(request);
            var actionResult = await _operationArrangement.Process(request, validatorActionResult);

            return actionResult;
        }

        public async Task<IActionResult> Produce(CreateSiteRequest request)
        {
            var validatorActionResult = await _validationArrangement.Validate(request);
            var actionResult = await _operationArrangement.Process(request, validatorActionResult);

            return actionResult;
        }
        #endregion

        #region Location
        public async Task<IActionResult> Produce(ListLocationRequest request)
        {
            var validatorActionResult = await _validationArrangement.Validate(request);
            var actionResult = await _operationArrangement.Process(request, validatorActionResult);

            return actionResult;
        }

        public async Task<IActionResult> Produce(CreateLocationRequest request)
        {
            var validatorActionResult = await _validationArrangement.Validate(request);
            var actionResult = await _operationArrangement.Process(request, validatorActionResult);

            return actionResult;
        }
        #endregion

        #region OperationalDepartment
        public async Task<IActionResult> Produce(ListOperationalDepartmentRequest request)
        {
            var validatorActionResult = await _validationArrangement.Validate(request);
            var actionResult = await _operationArrangement.Process(request, validatorActionResult);

            return actionResult;
        }

        public async Task<IActionResult> Produce(CreateOperationalDepartmentRequest request)
        {
            var validatorActionResult = await _validationArrangement.Validate(request);
            var actionResult = await _operationArrangement.Process(request, validatorActionResult);

            return actionResult;
        }
        #endregion

        #region Customer
        public async Task<IActionResult> Produce(ListCustomerRequest request)
        {
            var validatorActionResult = await _validationArrangement.Validate(request);
            var actionResult = await _operationArrangement.Process(request, validatorActionResult);

            return actionResult;
        }

        public async Task<IActionResult> Produce(CreateCustomerRequest request)
        {
            var validatorActionResult = await _validationArrangement.Validate(request);
            var actionResult = await _operationArrangement.Process(request, validatorActionResult);

            return actionResult;
        }
        #endregion

        #region BusinessUnit

        public async Task<IActionResult> Produce(ListBusinessUnitRequest request)
        {
            var validatorActionResult = await _validationArrangement.Validate(request);
            var actionResult = await _operationArrangement.Process(request, validatorActionResult);

            return actionResult;
        }

        public async Task<IActionResult> Produce(GetBusinessUnitRequest request)
        {
            var validatorActionResult = await _validationArrangement.Validate(request);
            var actionResult = await _operationArrangement.Process(request, validatorActionResult);

            return actionResult;
        }

        public async Task<IActionResult> Produce(CreateBusinessUnitRequest request)
        {
            var validatorActionResult = await _validationArrangement.Validate(request);
            var actionResult = await _operationArrangement.Process(request, validatorActionResult);

            return actionResult;
        }

        public async Task<IActionResult> Produce(UpdateBusinessUnitRequest request)
        {
            var validatorActionResult = await _validationArrangement.Validate(request);
            var actionResult = await _operationArrangement.Process(request, validatorActionResult);

            return actionResult;
        }

        public async Task<IActionResult> Produce(DeleteBusinessUnitRequest request)
        {
            var validatorActionResult = await _validationArrangement.Validate(request);
            var actionResult = await _operationArrangement.Process(request, validatorActionResult);

            return actionResult;
        }

        #endregion

        #region Flow

        public async Task<IActionResult> Produce(ListFlowRequest request)
        {
            var validatorActionResult = await _validationArrangement.Validate(request);
            var actionResult = await _operationArrangement.Process(request, validatorActionResult);

            return actionResult;
        }

        public async Task<IActionResult> Produce(GetFlowRequest request)
        {
            var validatorActionResult = await _validationArrangement.Validate(request);
            var actionResult = await _operationArrangement.Process(request, validatorActionResult);

            return actionResult;
        }

        public async Task<IActionResult> Produce(CreateFlowRequest request)
        {
            var validatorActionResult = await _validationArrangement.Validate(request);
            var actionResult = await _operationArrangement.Process(request, validatorActionResult);

            return actionResult;
        }

        public async Task<IActionResult> Produce(UpdateFlowRequest request)
        {
            var validatorActionResult = await _validationArrangement.Validate(request);
            var actionResult = await _operationArrangement.Process(request, validatorActionResult);

            return actionResult;
        }

        public async Task<IActionResult> Produce(DeleteFlowRequest request)
        {
            var validatorActionResult = await _validationArrangement.Validate(request);
            var actionResult = await _operationArrangement.Process(request, validatorActionResult);

            return actionResult;
        }

        #endregion

        #region Icon

        public async Task<IActionResult> Produce(ListIconRequest request)
        {
            var validatorActionResult = await _validationArrangement.Validate(request);
            var actionResult = await _operationArrangement.Process(request, validatorActionResult);

            return actionResult;
        }

        public async Task<IActionResult> Produce(GetIconRequest request)
        {
            var validatorActionResult = await _validationArrangement.Validate(request);
            var actionResult = await _operationArrangement.Process(request, validatorActionResult);

            return actionResult;
        }

        public async Task<IActionResult> Produce(CreateIconRequest request)
        {
            var validatorActionResult = await _validationArrangement.Validate(request);
            var actionResult = await _operationArrangement.Process(request, validatorActionResult);

            return actionResult;
        }

        public async Task<IActionResult> Produce(UpdateIconRequest request)
        {
            var validatorActionResult = await _validationArrangement.Validate(request);
            var actionResult = await _operationArrangement.Process(request, validatorActionResult);

            return actionResult;
        }

        public async Task<IActionResult> Produce(DeleteIconRequest request)
        {
            var validatorActionResult = await _validationArrangement.Validate(request);
            var actionResult = await _operationArrangement.Process(request, validatorActionResult);

            return actionResult;
        }

        #endregion

        #region Layout

        public async Task<IActionResult> Produce(ListLayoutRequest request)
        {
            var validatorActionResult = await _validationArrangement.Validate(request);
            var actionResult = await _operationArrangement.Process(request, validatorActionResult);

            return actionResult;
        }

        public async Task<IActionResult> Produce(GetLayoutRequest request)
        {
            var validatorActionResult = await _validationArrangement.Validate(request);
            var actionResult = await _operationArrangement.Process(request, validatorActionResult);

            return actionResult;
        }

        public async Task<IActionResult> Produce(CreateLayoutRequest request)
        {
            var validatorActionResult = await _validationArrangement.Validate(request);
            var actionResult = await _operationArrangement.Process(request, validatorActionResult);

            return actionResult;
        }

        public async Task<IActionResult> Produce(UpdateLayoutRequest request)
        {
            var validatorActionResult = await _validationArrangement.Validate(request);
            var actionResult = await _operationArrangement.Process(request, validatorActionResult);

            return actionResult;
        }

        public async Task<IActionResult> Produce(DeleteLayoutRequest request)
        {
            var validatorActionResult = await _validationArrangement.Validate(request);
            var actionResult = await _operationArrangement.Process(request, validatorActionResult);

            return actionResult;
        }

        #endregion

        #region Operation

        public async Task<IActionResult> Produce(ListOperationRequest request)
        {
            var validatorActionResult = await _validationArrangement.Validate(request);
            var actionResult = await _operationArrangement.Process(request, validatorActionResult);

            return actionResult;
        }

        public async Task<IActionResult> Produce(GetOperationRequest request)
        {
            var validatorActionResult = await _validationArrangement.Validate(request);
            var actionResult = await _operationArrangement.Process(request, validatorActionResult);

            return actionResult;
        }

        public async Task<IActionResult> Produce(CreateOperationRequest request)
        {
            var validatorActionResult = await _validationArrangement.Validate(request);
            var actionResult = await _operationArrangement.Process(request, validatorActionResult);

            return actionResult;
        }

        public async Task<IActionResult> Produce(UpdateOperationRequest request)
        {
            var validatorActionResult = await _validationArrangement.Validate(request);
            var actionResult = await _operationArrangement.Process(request, validatorActionResult);

            return actionResult;
        }

        public async Task<IActionResult> Produce(DeleteOperationRequest request)
        {
            var validatorActionResult = await _validationArrangement.Validate(request);
            var actionResult = await _operationArrangement.Process(request, validatorActionResult);

            return actionResult;
        }

        #endregion

        #region Source

        public async Task<IActionResult> Produce(ListSourceRequest request)
        {
            var validatorActionResult = await _validationArrangement.Validate(request);
            var actionResult = await _operationArrangement.Process(request, validatorActionResult);

            return actionResult;
        }

        public async Task<IActionResult> Produce(GetSourceRequest request)
        {
            var validatorActionResult = await _validationArrangement.Validate(request);
            var actionResult = await _operationArrangement.Process(request, validatorActionResult);

            return actionResult;
        }

        public async Task<IActionResult> Produce(CreateSourceRequest request)
        {
            var validatorActionResult = await _validationArrangement.Validate(request);
            var actionResult = await _operationArrangement.Process(request, validatorActionResult);

            return actionResult;
        }

        public async Task<IActionResult> Produce(UpdateSourceRequest request)
        {
            var validatorActionResult = await _validationArrangement.Validate(request);
            var actionResult = await _operationArrangement.Process(request, validatorActionResult);

            return actionResult;
        }

        public async Task<IActionResult> Produce(DeleteSourceRequest request)
        {
            var validatorActionResult = await _validationArrangement.Validate(request);
            var actionResult = await _operationArrangement.Process(request, validatorActionResult);

            return actionResult;
        }

        #endregion

        #region Files
        public async Task<IActionResult> Produce(UploadFileRequest request)
        {
            var validatorActionResult = await _validationArrangement.Validate(request);
            var actionResult = await _operationArrangement.Process(request, validatorActionResult);

            return actionResult;
        }

        public async Task<IActionResult> Produce(DownloadFileRequest request)
        {
            var validatorActionResult = await _validationArrangement.Validate(request);
            var actionResult = await _operationArrangement.Process(request, validatorActionResult);

            return actionResult;
        }

        #endregion

        #region OperatorActivities
        public async Task<IActionResult> Produce(CreateOperatorActivityRequest request)
        {
            var validationResult = await _validationArrangement.Validate(request);
            var actionResult = await _operationArrangement.Process(request, validationResult);

            return actionResult;
        }

        public async Task<IActionResult> Produce(ListOperatorActivityRequest request)
        {
            var validationResult = await _validationArrangement.Validate(request);
            var actionResult = await _operationArrangement.Process(request, validationResult);

            return actionResult;
        }
        #endregion
    }
}
