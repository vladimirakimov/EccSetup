using ITG.Brix.EccSetup.API.Context.Services.Arrangements.Bases;
using ITG.Brix.EccSetup.API.Context.Services.Requests.Models;
using ITG.Brix.EccSetup.API.Context.Services.Requests.Models.File;
using ITG.Brix.EccSetup.API.Context.Services.Requests.Models.OperatorActivities;
using ITG.Brix.EccSetup.API.Context.Services.Requests.Models.OperatorActivity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ITG.Brix.EccSetup.API.Context.Services.Arrangements
{
    public interface IOperationArrangement
    {
        Task<IActionResult> Process(CreateStorageStatusRequest request, IValidatorActionResult validatorActionResult);
        Task<IActionResult> Process(ListStorageStatusRequest request, IValidatorActionResult validatorActionResult);
        Task<IActionResult> Process(CreateDamageCodeRequest request, IValidatorActionResult validatorActionResult);
        Task<IActionResult> Process(ListDamageCodeRequest request, IValidatorActionResult validatorActionResult);
        Task<IActionResult> Process(CreateTypePlanningRequest request, IValidatorActionResult validatorActionResult);
        Task<IActionResult> Process(ListTypePlanningRequest request, IValidatorActionResult validatorActionResult);
        Task<IActionResult> Process(CreateProductionSiteRequest request, IValidatorActionResult validatorActionResult);
        Task<IActionResult> Process(ListProductionSiteRequest request, IValidatorActionResult validatorActionResult);
        Task<IActionResult> Process(CreateSiteRequest request, IValidatorActionResult validatorActionResult);
        Task<IActionResult> Process(ListSiteRequest request, IValidatorActionResult validatorActionResult);
        Task<IActionResult> Process(CreateTransportTypeRequest request, IValidatorActionResult validatorActionResult);
        Task<IActionResult> Process(ListTransportTypeRequest request, IValidatorActionResult validatorActionResult);
        Task<IActionResult> Process(CreateLocationRequest request, IValidatorActionResult validatorActionResult);
        Task<IActionResult> Process(ListLocationRequest request, IValidatorActionResult validatorActionResult);
        Task<IActionResult> Process(CreateOperationalDepartmentRequest request, IValidatorActionResult validatorActionResult);
        Task<IActionResult> Process(ListOperationalDepartmentRequest request, IValidatorActionResult validatorActionResult);
        Task<IActionResult> Process(CreateCustomerRequest request, IValidatorActionResult validatorActionResult);
        Task<IActionResult> Process(ListCustomerRequest request, IValidatorActionResult validatorActionResult);
        Task<IActionResult> Process(ListBusinessUnitRequest request, IValidatorActionResult validatorActionResult);
        Task<IActionResult> Process(GetBusinessUnitRequest request, IValidatorActionResult validatorActionResult);
        Task<IActionResult> Process(CreateBusinessUnitRequest request, IValidatorActionResult validatorActionResult);
        Task<IActionResult> Process(UpdateBusinessUnitRequest request, IValidatorActionResult validatorActionResult);
        Task<IActionResult> Process(DeleteBusinessUnitRequest request, IValidatorActionResult validatorActionResult);

        Task<IActionResult> Process(ListFlowRequest request, IValidatorActionResult validatorActionResult);
        Task<IActionResult> Process(GetFlowRequest request, IValidatorActionResult validatorActionResult);
        Task<IActionResult> Process(CreateFlowRequest request, IValidatorActionResult validatorActionResult);
        Task<IActionResult> Process(UpdateFlowRequest request, IValidatorActionResult validatorActionResult);
        Task<IActionResult> Process(DeleteFlowRequest request, IValidatorActionResult validatorActionResult);

        Task<IActionResult> Process(ListIconRequest request, IValidatorActionResult validatorActionResult);
        Task<IActionResult> Process(GetIconRequest request, IValidatorActionResult validatorActionResult);
        Task<IActionResult> Process(CreateIconRequest request, IValidatorActionResult validatorActionResult);
        Task<IActionResult> Process(UpdateIconRequest request, IValidatorActionResult validatorActionResult);
        Task<IActionResult> Process(DeleteIconRequest request, IValidatorActionResult validatorActionResult);

        Task<IActionResult> Process(ListLayoutRequest request, IValidatorActionResult validatorActionResult);
        Task<IActionResult> Process(GetLayoutRequest request, IValidatorActionResult validatorActionResult);
        Task<IActionResult> Process(CreateLayoutRequest request, IValidatorActionResult validatorActionResult);
        Task<IActionResult> Process(UpdateLayoutRequest request, IValidatorActionResult validatorActionResult);
        Task<IActionResult> Process(DeleteLayoutRequest request, IValidatorActionResult validatorActionResult);

        Task<IActionResult> Process(ListOperationRequest request, IValidatorActionResult validatorActionResult);
        Task<IActionResult> Process(GetOperationRequest request, IValidatorActionResult validatorActionResult);
        Task<IActionResult> Process(CreateOperationRequest request, IValidatorActionResult validatorActionResult);
        Task<IActionResult> Process(UpdateOperationRequest request, IValidatorActionResult validatorActionResult);
        Task<IActionResult> Process(DeleteOperationRequest request, IValidatorActionResult validatorActionResult);


        Task<IActionResult> Process(ListSourceRequest request, IValidatorActionResult validatorActionResult);
        Task<IActionResult> Process(GetSourceRequest request, IValidatorActionResult validatorActionResult);
        Task<IActionResult> Process(CreateSourceRequest request, IValidatorActionResult validatorActionResult);
        Task<IActionResult> Process(UpdateSourceRequest request, IValidatorActionResult validatorActionResult);
        Task<IActionResult> Process(DeleteSourceRequest request, IValidatorActionResult validatorActionResult);

        Task<IActionResult> Process(UploadFileRequest request, IValidatorActionResult validatorActionResult);
        Task<IActionResult> Process(DownloadFileRequest request, IValidatorActionResult validatorActionResult);

        Task<IActionResult> Process(CreateOperatorActivityRequest request, IValidatorActionResult validatorResult);
        Task<IActionResult> Process(ListOperatorActivityRequest request, IValidatorActionResult validatorActionResult);
    }
}
