using ITG.Brix.EccSetup.API.Context.Providers;
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
using ITG.Brix.EccSetup.Application.DataTypes;
using ITG.Brix.EccSetup.Application.Extensions;
using System;
using System.Collections.Generic;

namespace ITG.Brix.EccSetup.API.Context.Services.Requests.Mappers.Impl
{
    public class CqsMapper : ICqsMapper
    {
        private readonly IJsonProvider _jsonProvider;

        public CqsMapper(IJsonProvider jsonProvider)
        {
            _jsonProvider = jsonProvider ?? throw new ArgumentNullException(nameof(jsonProvider));
        }

        #region StorageStatus
        public ListStorageStatusQuery Map(ListStorageStatusRequest request)
        {
            var filter = request.Filter;
            var top = request.Top;
            var skip = request.Skip;

            var result = new ListStorageStatusQuery(filter, top, skip);
            return result;
        }

        public CreateStorageStatusCommand Map(CreateStorageStatusRequest request)
        {
            var result = new CreateStorageStatusCommand(request.BodyCode, request.BodyName, request.BodyDefault, request.BodySource);
            return result;
        }
        #endregion

        #region DamageCode
        public ListDamageCodeQuery Map(ListDamageCodeRequest request)
        {
            var filter = request.Filter;
            var top = request.Top;
            var skip = request.Skip;

            var result = new ListDamageCodeQuery(filter, top, skip);
            return result;
        }

        public CreateDamageCodeCommand Map(CreateDamageCodeRequest request)
        {
            var result = new CreateDamageCodeCommand(request.BodyCode, request.BodyName, request.BodyDamagedQuantityRequired, request.BodySource);
            return result;
        }
        #endregion

        #region TypePlanning
        public ListTypePlanningQuery Map(ListTypePlanningRequest request)
        {
            var filter = request.Filter;
            var top = request.Top;
            var skip = request.Skip;

            var result = new ListTypePlanningQuery(filter, top, skip);
            return result;
        }

        public CreateTypePlanningCommand Map(CreateTypePlanningRequest request)
        {
            var result = new CreateTypePlanningCommand(request.BodyCode, request.BodyName, request.BodySource);
            return result;
        }
        #endregion

        #region ProductionSite
        public ListProductionSiteQuery Map(ListProductionSiteRequest request)
        {
            var filter = request.Filter;
            var top = request.Top;
            var skip = request.Skip;

            var result = new ListProductionSiteQuery(filter, top, skip);
            return result;
        }

        public CreateProductionSiteCommand Map(CreateProductionSiteRequest request)
        {
            var result = new CreateProductionSiteCommand(request.BodyCode, request.BodyName, request.BodySource);
            return result;
        }
        #endregion

        #region transportType
        public ListTransportTypeQuery Map(ListTransportTypeRequest request)
        {
            var filter = request.Filter;
            var top = request.Top;
            var skip = request.Skip;

            var result = new ListTransportTypeQuery(filter, top, skip);
            return result;
        }

        public CreateTransportTypeCommand Map(CreateTransportTypeRequest request)
        {
            var result = new CreateTransportTypeCommand(request.BodyCode, request.BodyName, request.BodySource);
            return result;
        }
        #endregion

        #region Site

        public ListSiteQuery Map(ListSiteRequest request)
        {
            var filter = request.Filter;
            var top = request.Top;
            var skip = request.Skip;

            var result = new ListSiteQuery(filter, top, skip);
            return result;
        }

        public CreateSiteCommand Map(CreateSiteRequest request)
        {
            var result = new CreateSiteCommand(request.BodyCode, request.BodyName, request.BodySource);
            return result;
        }
        #endregion

        #region Location
        public ListLocationQuery Map(ListLocationRequest request)
        {
            var filter = request.Filter;
            var top = request.Top;
            var skip = request.Skip;

            var result = new ListLocationQuery(filter, top, skip);
            return result;
        }

        public CreateLocationCommand Map(CreateLocationRequest request)
        {
            var result = new CreateLocationCommand(request.BodySource, request.BodySite, request.BodyWarehouse, request.BodyGate,
                request.BodyRow, request.BodyPosition, request.BodyType, request.BodyIsRack);
            return result;
        }
        #endregion

        #region OperationalDepartment
        public ListOperationalDepartmentQuery Map(ListOperationalDepartmentRequest request)
        {
            var filter = request.Filter;
            var top = request.Top;
            var skip = request.Skip;

            var result = new ListOperationalDepartmentQuery(filter, top, skip);
            return result;
        }

        public CreateOperationalDepartmentCommand Map(CreateOperationalDepartmentRequest request)
        {
            var result = new CreateOperationalDepartmentCommand(request.BodyCode, request.BodyName, request.BodySite, request.BodySource);
            return result;
        }
        #endregion

        #region Customer
        public ListCustomerQuery Map(ListCustomerRequest request)
        {
            var filter = request.Filter;
            var top = request.Top;
            var skip = request.Skip;

            var result = new ListCustomerQuery(filter, top, skip);
            return result;
        }

        public CreateCustomerCommand Map(CreateCustomerRequest request)
        {
            var result = new CreateCustomerCommand(request.BodyCode, request.BodyName, request.BodySource);
            return result;
        }
        #endregion

        #region BusinessUnit
        public ListBusinessUnitQuery Map(ListBusinessUnitRequest request)
        {
            var filter = request.Filter;
            var top = request.Top;
            var skip = request.Skip;

            var result = new ListBusinessUnitQuery(filter, top, skip);
            return result;
        }

        public GetBusinessUnitQuery Map(GetBusinessUnitRequest request)
        {
            var id = new Guid(request.RouteId);

            var result = new GetBusinessUnitQuery(id);
            return result;
        }

        public CreateBusinessUnitCommand Map(CreateBusinessUnitRequest request)
        {
            var result = new CreateBusinessUnitCommand(request.BodyName);
            return result;
        }

        public UpdateBusinessUnitCommand Map(UpdateBusinessUnitRequest request)
        {
            var id = new Guid(request.RouteId);

            var valuePairs = _jsonProvider.ToDictionary(request.BodyPatch);

            Optional<string> name = valuePairs.GetOptional("name");

            var version = ToVersion(request.HeaderIfMatch);

            var result = new UpdateBusinessUnitCommand(id, name, version);
            return result;
        }

        public DeleteBusinessUnitCommand Map(DeleteBusinessUnitRequest request)
        {
            var id = new Guid(request.RouteId);

            var version = ToVersion(request.HeaderIfMatch);

            var result = new DeleteBusinessUnitCommand(id, version);
            return result;
        }
        #endregion

        #region Flow

        public ListFlowQuery Map(ListFlowRequest request)
        {
            var filter = request.Filter;
            var top = request.Top;
            var skip = request.Skip;

            var result = new ListFlowQuery(filter, top, skip);
            return result;
        }

        public GetFlowQuery Map(GetFlowRequest request)
        {
            var id = new Guid(request.RouteId);

            var result = new GetFlowQuery(id);
            return result;
        }

        public CreateFlowCommand Map(CreateFlowRequest request)
        {
            var result = new CreateFlowCommand(request.BodyName, request.BodyDescription, request.BodyImage);
            return result;
        }

        public UpdateFlowCommand Map(UpdateFlowRequest request)
        {
            var id = new Guid(request.RouteId);

            var valuePairs = _jsonProvider.ToDictionary(request.BodyPatch);

            Optional<string> name = valuePairs.GetOptional("name");
            Optional<string> description = valuePairs.GetOptional("description");
            Optional<string> image = valuePairs.GetOptional("image");
            Optional<string> diagram = valuePairs.GetOptional("diagram");
            Optional<string> filterContent = valuePairs.GetOptional("filterContent");

            var version = ToVersion(request.HeaderIfMatch);

            var result = new UpdateFlowCommand(id, name, description, image, diagram, filterContent, version);
            return result;
        }

        public DeleteFlowCommand Map(DeleteFlowRequest request)
        {
            var id = new Guid(request.RouteId);

            var version = ToVersion(request.HeaderIfMatch);

            var result = new DeleteFlowCommand(id, version);
            return result;
        }

        #endregion

        #region Icon

        public ListIconQuery Map(ListIconRequest request)
        {
            var filter = request.Filter;
            var top = request.Top;
            var skip = request.Skip;

            var result = new ListIconQuery(filter, top, skip);
            return result;
        }

        public GetIconQuery Map(GetIconRequest request)
        {
            var id = new Guid(request.RouteId);

            var result = new GetIconQuery(id);
            return result;
        }

        public CreateIconCommand Map(CreateIconRequest request)
        {
            var result = new CreateIconCommand(request.BodyName, request.BodyDataPath);
            return result;
        }

        public UpdateIconCommand Map(UpdateIconRequest request)
        {
            var id = new Guid(request.RouteId);

            var valuePairs = _jsonProvider.ToDictionary(request.BodyPatch);

            Optional<string> name = valuePairs.GetOptional("name");
            Optional<string> dataPath = valuePairs.GetOptional("dataPath");


            var version = ToVersion(request.HeaderIfMatch);

            var result = new UpdateIconCommand(id, name, dataPath, version);
            return result;
        }

        public DeleteIconCommand Map(DeleteIconRequest request)
        {
            var id = new Guid(request.RouteId);

            var version = ToVersion(request.HeaderIfMatch);

            var result = new DeleteIconCommand(id, version);
            return result;
        }

        #endregion

        #region Layout

        public ListLayoutQuery Map(ListLayoutRequest request)
        {
            var filter = request.Filter;
            var top = request.Top;
            var skip = request.Skip;

            var result = new ListLayoutQuery(filter, top, skip);
            return result;
        }

        public GetLayoutQuery Map(GetLayoutRequest request)
        {
            var id = new Guid(request.RouteId);

            var result = new GetLayoutQuery(id);
            return result;
        }

        public CreateLayoutCommand Map(CreateLayoutRequest request)
        {
            var result = new CreateLayoutCommand(request.BodyName, request.BodyDescription, request.BodyImage);
            return result;
        }

        public UpdateLayoutCommand Map(UpdateLayoutRequest request)
        {
            var id = new Guid(request.RouteId);

            var valuePairs = _jsonProvider.ToDictionary(request.BodyPatch);

            Optional<string> name = valuePairs.GetOptional("name");
            Optional<string> description = valuePairs.GetOptional("description");
            Optional<string> image = valuePairs.GetOptional("image");
            Optional<string> diagram = valuePairs.GetOptional("diagram");

            var version = ToVersion(request.HeaderIfMatch);

            var result = new UpdateLayoutCommand(id, name, description, image, diagram, version);
            return result;
        }

        public DeleteLayoutCommand Map(DeleteLayoutRequest request)
        {
            var id = new Guid(request.RouteId);

            var version = ToVersion(request.HeaderIfMatch);

            var result = new DeleteLayoutCommand(id, version);
            return result;
        }

        #endregion

        #region Source

        public ListSourceQuery Map(ListSourceRequest request)
        {
            var filter = request.Filter;
            var top = request.Top;
            var skip = request.Skip;

            var result = new ListSourceQuery(filter, top, skip);
            return result;
        }

        public GetSourceQuery Map(GetSourceRequest request)
        {
            var id = new Guid(request.RouteId);

            var result = new GetSourceQuery(id);
            return result;
        }

        public CreateSourceCommand Map(CreateSourceRequest request)
        {
            var result = new CreateSourceCommand(request.BodyName, request.BodyDescription, request.BodyBusinessUnits);
            return result;
        }

        public UpdateSourceCommand Map(UpdateSourceRequest request)
        {
            var id = new Guid(request.RouteId);

            var valuePairs = _jsonProvider.ToDictionary(request.BodyPatch);

            Optional<string> name = valuePairs.GetOptional("name");
            Optional<string> description = valuePairs.GetOptional("description");
            Optional<IEnumerable<string>> sourceBusinessUnits = valuePairs.GetOptionalEnumerable("businessUnits");

            var version = ToVersion(request.HeaderIfMatch);

            var result = new UpdateSourceCommand(id, name, description, sourceBusinessUnits, version);
            return result;
        }

        public DeleteSourceCommand Map(DeleteSourceRequest request)
        {
            var id = new Guid(request.RouteId);

            var version = ToVersion(request.HeaderIfMatch);

            var result = new DeleteSourceCommand(id, version);
            return result;
        }

        #endregion

        #region Operation

        public ListOperationQuery Map(ListOperationRequest request)
        {
            var filter = request.Filter;
            var top = request.Top;
            var skip = request.Skip;

            var result = new ListOperationQuery(filter, top, skip);
            return result;
        }

        public GetOperationQuery Map(GetOperationRequest request)
        {
            var id = new Guid(request.RouteId);

            var result = new GetOperationQuery(id);
            return result;
        }

        public CreateOperationCommand Map(CreateOperationRequest request)
        {
            var result = new CreateOperationCommand(request.BodyName, request.BodyDescription, null, request.BodyTags);
            return result;
        }

        public UpdateOperationCommand Map(UpdateOperationRequest request)
        {
            var id = new Guid(request.RouteId);

            var valuePairs = _jsonProvider.ToDictionary(request.BodyPatch);

            Optional<string> name = valuePairs.GetOptional("name");
            Optional<string> description = valuePairs.GetOptional("description");
            Optional<IEnumerable<string>> tags = valuePairs.GetOptionalEnumerable("tags");

            var version = ToVersion(request.HeaderIfMatch);

            var result = new UpdateOperationCommand(id, name, description, null, tags, version);
            return result;
        }

        public DeleteOperationCommand Map(DeleteOperationRequest request)
        {
            var id = new Guid(request.RouteId);

            var version = ToVersion(request.HeaderIfMatch);

            var result = new DeleteOperationCommand(id, version);
            return result;
        }

        #endregion

        #region File
        public UploadFileCommand Map(UploadFileRequest request)
        {
            var result = new UploadFileCommand(request.File, request.FileExtension);

            return result;
        }

        public DownloadFileQuery Map(DownloadFileRequest request)
        {
            var result = new DownloadFileQuery(request.File);

            return result;
        }
        #endregion

        #region OperatorActivities 
        public CreateOperatorActivityCommand Map(CreateOperatorActivityRequest request)
        {
            var result = new CreateOperatorActivityCommand(request.Body);

            return result;
        }

        public ListOperatorActivityQuery Map(ListOperatorActivityRequest request)
        {
            var result = new ListOperatorActivityQuery(request.Filter,
                                                       request.Top,
                                                       request.Skip);

            return result;
        }
        #endregion

        private int ToVersion(string eTag)
        {
            var eTagValue = eTag.Replace("\"", "");
            var result = int.Parse(eTagValue);

            return result;
        }
    }
}
