using ITG.Brix.EccSetup.API.Context.Services.Requests.Models;
using ITG.Brix.EccSetup.API.Context.Services.Requests.Models.File;
using ITG.Brix.EccSetup.API.Context.Services.Requests.Models.OperatorActivities;
using ITG.Brix.EccSetup.API.Context.Services.Requests.Models.OperatorActivity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ITG.Brix.EccSetup.API.Context.Services
{
    public interface IApiResult
    {
        Task<IActionResult> Produce(ListBusinessUnitRequest request);
        Task<IActionResult> Produce(GetBusinessUnitRequest request);
        Task<IActionResult> Produce(CreateBusinessUnitRequest request);
        Task<IActionResult> Produce(UpdateBusinessUnitRequest request);
        Task<IActionResult> Produce(DeleteBusinessUnitRequest request);

        Task<IActionResult> Produce(ListFlowRequest request);
        Task<IActionResult> Produce(GetFlowRequest request);
        Task<IActionResult> Produce(CreateFlowRequest request);
        Task<IActionResult> Produce(UpdateFlowRequest request);
        Task<IActionResult> Produce(DeleteFlowRequest request);

        Task<IActionResult> Produce(ListIconRequest request);
        Task<IActionResult> Produce(GetIconRequest request);
        Task<IActionResult> Produce(CreateIconRequest request);
        Task<IActionResult> Produce(UpdateIconRequest request);
        Task<IActionResult> Produce(DeleteIconRequest request);

        Task<IActionResult> Produce(ListLayoutRequest request);
        Task<IActionResult> Produce(GetLayoutRequest request);
        Task<IActionResult> Produce(CreateLayoutRequest request);
        Task<IActionResult> Produce(UpdateLayoutRequest request);
        Task<IActionResult> Produce(DeleteLayoutRequest request);

        Task<IActionResult> Produce(ListOperationRequest request);
        Task<IActionResult> Produce(GetOperationRequest request);
        Task<IActionResult> Produce(CreateOperationRequest request);
        Task<IActionResult> Produce(UpdateOperationRequest request);
        Task<IActionResult> Produce(DeleteOperationRequest request);


        Task<IActionResult> Produce(ListSourceRequest request);
        Task<IActionResult> Produce(GetSourceRequest request);
        Task<IActionResult> Produce(CreateSourceRequest request);
        Task<IActionResult> Produce(UpdateSourceRequest request);
        Task<IActionResult> Produce(DeleteSourceRequest request);

        Task<IActionResult> Produce(UploadFileRequest request);
        Task<IActionResult> Produce(DownloadFileRequest request);

        Task<IActionResult> Produce(CreateOperatorActivityRequest request);
        Task<IActionResult> Produce(ListOperatorActivityRequest request);
        Task<IActionResult> Produce(ListCustomerRequest request);
        Task<IActionResult> Produce(CreateCustomerRequest request);
        Task<IActionResult> Produce(ListOperationalDepartmentRequest request);
        Task<IActionResult> Produce(CreateOperationalDepartmentRequest request);
        Task<IActionResult> Produce(ListLocationRequest request);
        Task<IActionResult> Produce(CreateLocationRequest request);
        Task<IActionResult> Produce(CreateTransportTypeRequest request);
        Task<IActionResult> Produce(ListTransportTypeRequest request);
        Task<IActionResult> Produce(CreateSiteRequest request);
        Task<IActionResult> Produce(ListSiteRequest request);
        Task<IActionResult> Produce(CreateProductionSiteRequest request);
        Task<IActionResult> Produce(ListProductionSiteRequest request);
        Task<IActionResult> Produce(CreateTypePlanningRequest request);
        Task<IActionResult> Produce(ListTypePlanningRequest request);
        Task<IActionResult> Produce(CreateDamageCodeRequest request);
        Task<IActionResult> Produce(ListDamageCodeRequest request);
        Task<IActionResult> Produce(CreateStorageStatusRequest request);
        Task<IActionResult> Produce(ListStorageStatusRequest request);
    }
}
