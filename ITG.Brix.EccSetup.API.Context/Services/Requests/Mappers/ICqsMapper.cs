using ITG.Brix.EccSetup.API.Context.Services.Requests.Models;
using ITG.Brix.EccSetup.API.Context.Services.Requests.Models.File;
using ITG.Brix.EccSetup.API.Context.Services.Requests.Models.OperatorActivities;
using ITG.Brix.EccSetup.API.Context.Services.Requests.Models.OperatorActivity;
using ITG.Brix.EccSetup.Application.Cqs.Commands.Definitions;
using ITG.Brix.EccSetup.Application.Cqs.Commands.Definitions.File;
using ITG.Brix.EccSetup.Application.Cqs.Commands.Definitions.OperatorActivity;
using ITG.Brix.EccSetup.Application.Cqs.Queries.Definitions;
using ITG.Brix.EccSetup.Application.Cqs.Queries.Definitions.File;
using ITG.Brix.EccSetup.Application.Cqs.Queries.Definitions.OperatorActivity;

namespace ITG.Brix.EccSetup.API.Context.Services.Requests.Mappers
{
    public partial interface ICqsMapper
    {
        ListStorageStatusQuery Map(ListStorageStatusRequest request);
        CreateStorageStatusCommand Map(CreateStorageStatusRequest request);
        ListDamageCodeQuery Map(ListDamageCodeRequest request);
        CreateDamageCodeCommand Map(CreateDamageCodeRequest request);
        ListTypePlanningQuery Map(ListTypePlanningRequest request);
        CreateTypePlanningCommand Map(CreateTypePlanningRequest request);
        ListProductionSiteQuery Map(ListProductionSiteRequest request);
        CreateProductionSiteCommand Map(CreateProductionSiteRequest request);
        ListSiteQuery Map(ListSiteRequest request);
        CreateSiteCommand Map(CreateSiteRequest request);
        ListTransportTypeQuery Map(ListTransportTypeRequest request);
        CreateTransportTypeCommand Map(CreateTransportTypeRequest request);
        ListLocationQuery Map(ListLocationRequest request);
        CreateLocationCommand Map(CreateLocationRequest request);
        ListOperationalDepartmentQuery Map(ListOperationalDepartmentRequest request);
        CreateOperationalDepartmentCommand Map(CreateOperationalDepartmentRequest request);
        ListCustomerQuery Map(ListCustomerRequest request);
        CreateCustomerCommand Map(CreateCustomerRequest request);
        ListBusinessUnitQuery Map(ListBusinessUnitRequest request);
        GetBusinessUnitQuery Map(GetBusinessUnitRequest request);
        CreateBusinessUnitCommand Map(CreateBusinessUnitRequest request);
        UpdateBusinessUnitCommand Map(UpdateBusinessUnitRequest request);
        DeleteBusinessUnitCommand Map(DeleteBusinessUnitRequest request);

        ListFlowQuery Map(ListFlowRequest request);
        GetFlowQuery Map(GetFlowRequest request);
        CreateFlowCommand Map(CreateFlowRequest request);
        UpdateFlowCommand Map(UpdateFlowRequest request);
        DeleteFlowCommand Map(DeleteFlowRequest request);

        ListIconQuery Map(ListIconRequest request);
        GetIconQuery Map(GetIconRequest request);
        CreateIconCommand Map(CreateIconRequest request);
        UpdateIconCommand Map(UpdateIconRequest request);
        DeleteIconCommand Map(DeleteIconRequest request);

        ListLayoutQuery Map(ListLayoutRequest request);
        GetLayoutQuery Map(GetLayoutRequest request);
        CreateLayoutCommand Map(CreateLayoutRequest request);
        UpdateLayoutCommand Map(UpdateLayoutRequest request);
        DeleteLayoutCommand Map(DeleteLayoutRequest request);

        ListSourceQuery Map(ListSourceRequest request);
        GetSourceQuery Map(GetSourceRequest request);
        CreateSourceCommand Map(CreateSourceRequest request);
        UpdateSourceCommand Map(UpdateSourceRequest request);
        DeleteSourceCommand Map(DeleteSourceRequest request);

        ListOperationQuery Map(ListOperationRequest request);
        GetOperationQuery Map(GetOperationRequest request);
        CreateOperationCommand Map(CreateOperationRequest request);
        UpdateOperationCommand Map(UpdateOperationRequest request);
        DeleteOperationCommand Map(DeleteOperationRequest request);

        UploadFileCommand Map(UploadFileRequest request);
        DownloadFileQuery Map(DownloadFileRequest request);

        CreateOperatorActivityCommand Map(CreateOperatorActivityRequest request);
        ListOperatorActivityQuery Map(ListOperatorActivityRequest request);
    }
}
