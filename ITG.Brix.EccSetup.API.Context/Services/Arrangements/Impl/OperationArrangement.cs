using ITG.Brix.EccSetup.API.Context.Services.Arrangements.Bases;
using ITG.Brix.EccSetup.API.Context.Services.Requests.Mappers;
using ITG.Brix.EccSetup.API.Context.Services.Requests.Models;
using ITG.Brix.EccSetup.API.Context.Services.Requests.Models.File;
using ITG.Brix.EccSetup.API.Context.Services.Requests.Models.OperatorActivities;
using ITG.Brix.EccSetup.API.Context.Services.Requests.Models.OperatorActivity;
using ITG.Brix.EccSetup.API.Context.Services.Responses;
using ITG.Brix.EccSetup.Application.Bases;
using ITG.Brix.EccSetup.Application.Cqs.Queries.Models;
using ITG.Brix.EccSetup.Application.Cqs.Queries.Models.Files;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ITG.Brix.EccSetup.API.Context.Services.Arrangements.Impl
{
    public class OperationArrangement : IOperationArrangement
    {
        private readonly IMediator _mediator;
        private readonly IApiResponse _apiResponse;
        private readonly ICqsMapper _cqsMapper;

        public OperationArrangement(IMediator mediator,
                                    IApiResponse apiResponse,
                                    ICqsMapper cqsMapper)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _apiResponse = apiResponse ?? throw new ArgumentNullException(nameof(apiResponse));
            _cqsMapper = cqsMapper ?? throw new ArgumentNullException(nameof(cqsMapper));
        }

        #region StorageStatus
        public async Task<IActionResult> Process(ListStorageStatusRequest request, IValidatorActionResult validatorActionResult)
        {
            IActionResult actionResult;

            if (validatorActionResult.Result == null)
            {
                request.UpdateFilter(fromToSet: new Dictionary<string, string>{
                        { "version", "dissalowed"}
                    });

                var call = _cqsMapper.Map(request);

                var result = await _mediator.Send(call);

                actionResult = result.IsFailure ? _apiResponse.Fail(result)
                                                : _apiResponse.Ok(((Result<StorageStatusesModel>)result).Value);
            }
            else
            {
                actionResult = validatorActionResult.Result;
            }

            return actionResult;
        }

        public async Task<IActionResult> Process(CreateStorageStatusRequest request, IValidatorActionResult validatorActionResult)
        {
            IActionResult actionResult;

            if (validatorActionResult.Result == null)
            {
                var call = _cqsMapper.Map(request);

                var result = await _mediator.Send(call);

                actionResult = result.IsFailure ? _apiResponse.Fail(result)
                                                : _apiResponse.Created(string.Format("/api/storagestatuses/{0}", ((Result<Guid>)result).Value), result.Version.ToString());
            }
            else
            {
                actionResult = validatorActionResult.Result;
            }

            return actionResult;
        }
        #endregion

        #region DamageCode
        public async Task<IActionResult> Process(ListDamageCodeRequest request, IValidatorActionResult validatorActionResult)
        {
            IActionResult actionResult;

            if (validatorActionResult.Result == null)
            {
                request.UpdateFilter(fromToSet: new Dictionary<string, string>{
                        { "version", "dissalowed"}
                    });

                var call = _cqsMapper.Map(request);

                var result = await _mediator.Send(call);

                actionResult = result.IsFailure ? _apiResponse.Fail(result)
                                                : _apiResponse.Ok(((Result<DamageCodesModel>)result).Value);
            }
            else
            {
                actionResult = validatorActionResult.Result;
            }

            return actionResult;
        }

        public async Task<IActionResult> Process(CreateDamageCodeRequest request, IValidatorActionResult validatorActionResult)
        {
            IActionResult actionResult;

            if (validatorActionResult.Result == null)
            {
                var call = _cqsMapper.Map(request);

                var result = await _mediator.Send(call);

                actionResult = result.IsFailure ? _apiResponse.Fail(result)
                                                : _apiResponse.Created(string.Format("/api/damagecodes/{0}", ((Result<Guid>)result).Value), result.Version.ToString());
            }
            else
            {
                actionResult = validatorActionResult.Result;
            }

            return actionResult;
        }
        #endregion

        #region TypePlanning
        public async Task<IActionResult> Process(ListTypePlanningRequest request, IValidatorActionResult validatorActionResult)
        {
            IActionResult actionResult;

            if (validatorActionResult.Result == null)
            {
                request.UpdateFilter(fromToSet: new Dictionary<string, string>{
                        { "version", "dissalowed"}
                    });

                var call = _cqsMapper.Map(request);

                var result = await _mediator.Send(call);

                actionResult = result.IsFailure ? _apiResponse.Fail(result)
                                                : _apiResponse.Ok(((Result<TypePlanningsModel>)result).Value);
            }
            else
            {
                actionResult = validatorActionResult.Result;
            }

            return actionResult;
        }

        public async Task<IActionResult> Process(CreateTypePlanningRequest request, IValidatorActionResult validatorActionResult)
        {
            IActionResult actionResult;

            if (validatorActionResult.Result == null)
            {
                var call = _cqsMapper.Map(request);

                var result = await _mediator.Send(call);

                actionResult = result.IsFailure ? _apiResponse.Fail(result)
                                                : _apiResponse.Created(string.Format("/api/typeplannings/{0}", ((Result<Guid>)result).Value), result.Version.ToString());
            }
            else
            {
                actionResult = validatorActionResult.Result;
            }

            return actionResult;
        }
        #endregion

        #region ProductionSite
        public async Task<IActionResult> Process(ListProductionSiteRequest request, IValidatorActionResult validatorActionResult)
        {
            IActionResult actionResult;

            if (validatorActionResult.Result == null)
            {
                request.UpdateFilter(fromToSet: new Dictionary<string, string>{
                        { "version", "dissalowed"}
                    });

                var call = _cqsMapper.Map(request);

                var result = await _mediator.Send(call);

                actionResult = result.IsFailure ? _apiResponse.Fail(result)
                                                : _apiResponse.Ok(((Result<ProductionSitesModel>)result).Value);
            }
            else
            {
                actionResult = validatorActionResult.Result;
            }

            return actionResult;
        }

        public async Task<IActionResult> Process(CreateProductionSiteRequest request, IValidatorActionResult validatorActionResult)
        {
            IActionResult actionResult;

            if (validatorActionResult.Result == null)
            {
                var call = _cqsMapper.Map(request);

                var result = await _mediator.Send(call);

                actionResult = result.IsFailure ? _apiResponse.Fail(result)
                                                : _apiResponse.Created(string.Format("/api/productionsites/{0}", ((Result<Guid>)result).Value), result.Version.ToString());
            }
            else
            {
                actionResult = validatorActionResult.Result;
            }

            return actionResult;
        }
        #endregion

        #region Site
        public async Task<IActionResult> Process(ListSiteRequest request, IValidatorActionResult validatorActionResult)
        {
            IActionResult actionResult;

            if (validatorActionResult.Result == null)
            {
                request.UpdateFilter(fromToSet: new Dictionary<string, string>{
                        { "version", "dissalowed"}
                    });

                var call = _cqsMapper.Map(request);

                var result = await _mediator.Send(call);

                actionResult = result.IsFailure ? _apiResponse.Fail(result)
                                                : _apiResponse.Ok(((Result<SitesModel>)result).Value);
            }
            else
            {
                actionResult = validatorActionResult.Result;
            }

            return actionResult;
        }

        public async Task<IActionResult> Process(CreateSiteRequest request, IValidatorActionResult validatorActionResult)
        {
            IActionResult actionResult;

            if (validatorActionResult.Result == null)
            {
                var call = _cqsMapper.Map(request);

                var result = await _mediator.Send(call);

                actionResult = result.IsFailure ? _apiResponse.Fail(result)
                                                : _apiResponse.Created(string.Format("/api/sites/{0}", ((Result<Guid>)result).Value), result.Version.ToString());
            }
            else
            {
                actionResult = validatorActionResult.Result;
            }

            return actionResult;
        }
        #endregion

        #region TransportType
        public async Task<IActionResult> Process(ListTransportTypeRequest request, IValidatorActionResult validatorActionResult)
        {
            IActionResult actionResult;

            if (validatorActionResult.Result == null)
            {
                request.UpdateFilter(fromToSet: new Dictionary<string, string>{
                        { "version", "dissalowed"}
                    });

                var call = _cqsMapper.Map(request);

                var result = await _mediator.Send(call);

                actionResult = result.IsFailure ? _apiResponse.Fail(result)
                                                : _apiResponse.Ok(((Result<TransportTypesModel>)result).Value);
            }
            else
            {
                actionResult = validatorActionResult.Result;
            }

            return actionResult;
        }

        public async Task<IActionResult> Process(CreateTransportTypeRequest request, IValidatorActionResult validatorActionResult)
        {
            IActionResult actionResult;

            if (validatorActionResult.Result == null)
            {
                var call = _cqsMapper.Map(request);

                var result = await _mediator.Send(call);

                actionResult = result.IsFailure ? _apiResponse.Fail(result)
                                                : _apiResponse.Created(string.Format("/api/transporttypes/{0}", ((Result<Guid>)result).Value), result.Version.ToString());
            }
            else
            {
                actionResult = validatorActionResult.Result;
            }

            return actionResult;
        }
        #endregion

        #region Location
        public async Task<IActionResult> Process(ListLocationRequest request, IValidatorActionResult validatorActionResult)
        {
            IActionResult actionResult;

            if (validatorActionResult.Result == null)
            {
                request.UpdateFilter(fromToSet: new Dictionary<string, string>{
                        { "version", "dissalowed"}
                    });

                var call = _cqsMapper.Map(request);

                var result = await _mediator.Send(call);

                actionResult = result.IsFailure ? _apiResponse.Fail(result)
                                                : _apiResponse.Ok(((Result<LocationsModel>)result).Value);
            }
            else
            {
                actionResult = validatorActionResult.Result;
            }

            return actionResult;
        }

        public async Task<IActionResult> Process(CreateLocationRequest request, IValidatorActionResult validatorActionResult)
        {
            IActionResult actionResult;

            if (validatorActionResult.Result == null)
            {
                var call = _cqsMapper.Map(request);

                var result = await _mediator.Send(call);

                actionResult = result.IsFailure ? _apiResponse.Fail(result)
                                                : _apiResponse.Created(string.Format("/api/locations/{0}", ((Result<Guid>)result).Value), result.Version.ToString());
            }
            else
            {
                actionResult = validatorActionResult.Result;
            }

            return actionResult;
        }
        #endregion

        #region OperationalDepartment
        public async Task<IActionResult> Process(ListOperationalDepartmentRequest request, IValidatorActionResult validatorActionResult)
        {
            IActionResult actionResult;

            if (validatorActionResult.Result == null)
            {
                request.UpdateFilter(fromToSet: new Dictionary<string, string>{
                        { "version", "dissalowed"}
                    });

                var call = _cqsMapper.Map(request);

                var result = await _mediator.Send(call);

                actionResult = result.IsFailure ? _apiResponse.Fail(result)
                                                : _apiResponse.Ok(((Result<OperationalDepartmentsModel>)result).Value);
            }
            else
            {
                actionResult = validatorActionResult.Result;
            }

            return actionResult;
        }

        public async Task<IActionResult> Process(CreateOperationalDepartmentRequest request, IValidatorActionResult validatorActionResult)
        {
            IActionResult actionResult;

            if (validatorActionResult.Result == null)
            {
                var call = _cqsMapper.Map(request);

                var result = await _mediator.Send(call);

                actionResult = result.IsFailure ? _apiResponse.Fail(result)
                                                : _apiResponse.Created(string.Format("/api/operationaldepartments/{0}", ((Result<Guid>)result).Value), result.Version.ToString());
            }
            else
            {
                actionResult = validatorActionResult.Result;
            }

            return actionResult;
        }
        #endregion

        #region Customer
        public async Task<IActionResult> Process(ListCustomerRequest request, IValidatorActionResult validatorActionResult)
        {
            IActionResult actionResult;

            if (validatorActionResult.Result == null)
            {
                request.UpdateFilter(fromToSet: new Dictionary<string, string>{
                        { "version", "dissalowed"}
                    });

                var call = _cqsMapper.Map(request);

                var result = await _mediator.Send(call);

                actionResult = result.IsFailure ? _apiResponse.Fail(result)
                                                : _apiResponse.Ok(((Result<CustomersModel>)result).Value);
            }
            else
            {
                actionResult = validatorActionResult.Result;
            }

            return actionResult;
        }

        public async Task<IActionResult> Process(CreateCustomerRequest request, IValidatorActionResult validatorActionResult)
        {
            IActionResult actionResult;

            if (validatorActionResult.Result == null)
            {
                var call = _cqsMapper.Map(request);

                var result = await _mediator.Send(call);

                actionResult = result.IsFailure ? _apiResponse.Fail(result)
                                                : _apiResponse.Created(string.Format("/api/customers/{0}", ((Result<Guid>)result).Value), result.Version.ToString());
            }
            else
            {
                actionResult = validatorActionResult.Result;
            }

            return actionResult;
        }
        #endregion

        #region BusinessUnit

        public async Task<IActionResult> Process(ListBusinessUnitRequest request, IValidatorActionResult validatorActionResult)
        {
            IActionResult actionResult;

            if (validatorActionResult.Result == null)
            {
                request.UpdateFilter(fromToSet: new Dictionary<string, string>{
                        { "version", "dissalowed"}
                    });

                var call = _cqsMapper.Map(request);

                var result = await _mediator.Send(call);

                actionResult = result.IsFailure ? _apiResponse.Fail(result)
                                                : _apiResponse.Ok(((Result<BusinessUnitsModel>)result).Value);
            }
            else
            {
                actionResult = validatorActionResult.Result;
            }

            return actionResult;
        }

        public async Task<IActionResult> Process(GetBusinessUnitRequest request, IValidatorActionResult validatorActionResult)
        {
            IActionResult actionResult;

            if (validatorActionResult.Result == null)
            {
                var call = _cqsMapper.Map(request);

                var result = await _mediator.Send(call);

                actionResult = result.IsFailure ? _apiResponse.Fail(result)
                                                : _apiResponse.Ok(((Result<BusinessUnitModel>)result).Value, ((Result<BusinessUnitModel>)result).Version.ToString());
            }
            else
            {
                actionResult = validatorActionResult.Result;
            }

            return actionResult;
        }

        public async Task<IActionResult> Process(CreateBusinessUnitRequest request, IValidatorActionResult validatorActionResult)
        {
            IActionResult actionResult;

            if (validatorActionResult.Result == null)
            {
                var call = _cqsMapper.Map(request);

                var result = await _mediator.Send(call);

                actionResult = result.IsFailure ? _apiResponse.Fail(result)
                                                : _apiResponse.Created(string.Format("/api/businessunits/{0}", ((Result<Guid>)result).Value), result.Version.ToString());
            }
            else
            {
                actionResult = validatorActionResult.Result;
            }

            return actionResult;
        }

        public async Task<IActionResult> Process(UpdateBusinessUnitRequest request, IValidatorActionResult validatorActionResult)
        {
            IActionResult actionResult;

            if (validatorActionResult.Result == null)
            {
                var call = _cqsMapper.Map(request);

                var result = await _mediator.Send(call);

                actionResult = result.IsFailure ? _apiResponse.Fail(result)
                                                : _apiResponse.Updated(result.Version.ToString());
            }
            else
            {
                actionResult = validatorActionResult.Result;
            }

            return actionResult;
        }

        public async Task<IActionResult> Process(DeleteBusinessUnitRequest request, IValidatorActionResult validatorActionResult)
        {
            IActionResult actionResult;

            if (validatorActionResult.Result == null)
            {
                var call = _cqsMapper.Map(request);

                var result = await _mediator.Send(call);

                actionResult = result.IsFailure ? _apiResponse.Fail(result)
                                                : _apiResponse.Deleted();
            }
            else
            {
                actionResult = validatorActionResult.Result;
            }

            return actionResult;
        }

        #endregion

        #region Flow

        public async Task<IActionResult> Process(ListFlowRequest request, IValidatorActionResult validatorActionResult)
        {
            IActionResult actionResult;

            if (validatorActionResult.Result == null)
            {
                request.Unescape();

                request.UpdateFilter(fromToSet: new Dictionary<string, string>{
                        { "image", "dissalowed"},
                        { "diagram", "dissalowed"},
                        { "version", "dissalowed"},
                    });

                var call = _cqsMapper.Map(request);

                var result = await _mediator.Send(call);

                actionResult = result.IsFailure ? _apiResponse.Fail(result)
                                                : _apiResponse.Ok(((Result<FlowsModel>)result).Value);
            }
            else
            {
                actionResult = validatorActionResult.Result;
            }

            return actionResult;
        }

        public async Task<IActionResult> Process(GetFlowRequest request, IValidatorActionResult validatorActionResult)
        {
            IActionResult actionResult;

            if (validatorActionResult.Result == null)
            {
                var call = _cqsMapper.Map(request);

                var result = await _mediator.Send(call);

                actionResult = result.IsFailure ? _apiResponse.Fail(result)
                                                : _apiResponse.Ok(((Result<FlowModel>)result).Value, ((Result<FlowModel>)result).Version.ToString());
            }
            else
            {
                actionResult = validatorActionResult.Result;
            }

            return actionResult;
        }

        public async Task<IActionResult> Process(CreateFlowRequest request, IValidatorActionResult validatorActionResult)
        {
            IActionResult actionResult;

            if (validatorActionResult.Result == null)
            {
                var call = _cqsMapper.Map(request);

                var result = await _mediator.Send(call);

                actionResult = result.IsFailure ? _apiResponse.Fail(result)
                                                : _apiResponse.Created(string.Format("/api/flows/{0}", ((Result<Guid>)result).Value), result.Version.ToString());
            }
            else
            {
                actionResult = validatorActionResult.Result;
            }

            return actionResult;
        }

        public async Task<IActionResult> Process(UpdateFlowRequest request, IValidatorActionResult validatorActionResult)
        {
            IActionResult actionResult;

            if (validatorActionResult.Result == null)
            {
                var call = _cqsMapper.Map(request);

                var result = await _mediator.Send(call);

                actionResult = result.IsFailure ? _apiResponse.Fail(result)
                                                : _apiResponse.Updated(result.Version.ToString());
            }
            else
            {
                actionResult = validatorActionResult.Result;
            }

            return actionResult;
        }

        public async Task<IActionResult> Process(DeleteFlowRequest request, IValidatorActionResult validatorActionResult)
        {
            IActionResult actionResult;

            if (validatorActionResult.Result == null)
            {
                var call = _cqsMapper.Map(request);

                var result = await _mediator.Send(call);

                actionResult = result.IsFailure ? _apiResponse.Fail(result)
                                                : _apiResponse.Deleted();
            }
            else
            {
                actionResult = validatorActionResult.Result;
            }

            return actionResult;
        }

        #endregion

        #region Icon

        public async Task<IActionResult> Process(ListIconRequest request, IValidatorActionResult validatorActionResult)
        {
            IActionResult actionResult;

            if (validatorActionResult.Result == null)
            {
                request.UpdateFilter(fromToSet: new Dictionary<string, string>{
                        { "datapath", "dissalowed"},
                        { "version", "dissalowed"},
                    });

                var call = _cqsMapper.Map(request);

                var result = await _mediator.Send(call);

                actionResult = result.IsFailure ? _apiResponse.Fail(result)
                                                : _apiResponse.Ok(((Result<IconsModel>)result).Value);
            }
            else
            {
                actionResult = validatorActionResult.Result;
            }

            return actionResult;
        }

        public async Task<IActionResult> Process(GetIconRequest request, IValidatorActionResult validatorActionResult)
        {
            IActionResult actionResult;

            if (validatorActionResult.Result == null)
            {
                var call = _cqsMapper.Map(request);

                var result = await _mediator.Send(call);

                actionResult = result.IsFailure ? _apiResponse.Fail(result)
                                                : _apiResponse.Ok(((Result<IconModel>)result).Value, ((Result<IconModel>)result).Version.ToString());
            }
            else
            {
                actionResult = validatorActionResult.Result;
            }

            return actionResult;
        }

        public async Task<IActionResult> Process(CreateIconRequest request, IValidatorActionResult validatorActionResult)
        {
            IActionResult actionResult;

            if (validatorActionResult.Result == null)
            {
                var call = _cqsMapper.Map(request);

                var result = await _mediator.Send(call);

                actionResult = result.IsFailure ? _apiResponse.Fail(result)
                                                : _apiResponse.Created(string.Format("/api/icons/{0}", ((Result<Guid>)result).Value), result.Version.ToString());
            }
            else
            {
                actionResult = validatorActionResult.Result;
            }

            return actionResult;
        }

        public async Task<IActionResult> Process(UpdateIconRequest request, IValidatorActionResult validatorActionResult)
        {
            IActionResult actionResult;

            if (validatorActionResult.Result == null)
            {
                var call = _cqsMapper.Map(request);

                var result = await _mediator.Send(call);

                actionResult = result.IsFailure ? _apiResponse.Fail(result)
                                                : _apiResponse.Updated(result.Version.ToString());
            }
            else
            {
                actionResult = validatorActionResult.Result;
            }

            return actionResult;
        }

        public async Task<IActionResult> Process(DeleteIconRequest request, IValidatorActionResult validatorActionResult)
        {
            IActionResult actionResult;

            if (validatorActionResult.Result == null)
            {
                var call = _cqsMapper.Map(request);

                var result = await _mediator.Send(call);

                actionResult = result.IsFailure ? _apiResponse.Fail(result)
                                                : _apiResponse.Deleted();
            }
            else
            {
                actionResult = validatorActionResult.Result;
            }

            return actionResult;
        }

        #endregion

        #region Layout

        public async Task<IActionResult> Process(ListLayoutRequest request, IValidatorActionResult validatorActionResult)
        {
            IActionResult actionResult;

            if (validatorActionResult.Result == null)
            {
                request.UpdateFilter(fromToSet: new Dictionary<string, string>{
                        { "image", "dissalowed"},
                        { "diagram", "dissalowed"},
                        { "version", "dissalowed"},
                    });

                var call = _cqsMapper.Map(request);

                var result = await _mediator.Send(call);

                actionResult = result.IsFailure ? _apiResponse.Fail(result)
                                                : _apiResponse.Ok(((Result<LayoutsModel>)result).Value);
            }
            else
            {
                actionResult = validatorActionResult.Result;
            }

            return actionResult;
        }

        public async Task<IActionResult> Process(GetLayoutRequest request, IValidatorActionResult validatorActionResult)
        {
            IActionResult actionResult;

            if (validatorActionResult.Result == null)
            {
                var call = _cqsMapper.Map(request);

                var result = await _mediator.Send(call);

                actionResult = result.IsFailure ? _apiResponse.Fail(result)
                                                : _apiResponse.Ok(((Result<LayoutModel>)result).Value, ((Result<LayoutModel>)result).Version.ToString());
            }
            else
            {
                actionResult = validatorActionResult.Result;
            }

            return actionResult;
        }

        public async Task<IActionResult> Process(CreateLayoutRequest request, IValidatorActionResult validatorActionResult)
        {
            IActionResult actionResult;

            if (validatorActionResult.Result == null)
            {
                var call = _cqsMapper.Map(request);

                var result = await _mediator.Send(call);

                actionResult = result.IsFailure ? _apiResponse.Fail(result)
                                                : _apiResponse.Created(string.Format("/api/layouts/{0}", ((Result<Guid>)result).Value), result.Version.ToString());
            }
            else
            {
                actionResult = validatorActionResult.Result;
            }

            return actionResult;
        }

        public async Task<IActionResult> Process(UpdateLayoutRequest request, IValidatorActionResult validatorActionResult)
        {
            IActionResult actionResult;

            if (validatorActionResult.Result == null)
            {
                var call = _cqsMapper.Map(request);

                var result = await _mediator.Send(call);

                actionResult = result.IsFailure ? _apiResponse.Fail(result)
                                                : _apiResponse.Updated(result.Version.ToString());
            }
            else
            {
                actionResult = validatorActionResult.Result;
            }

            return actionResult;
        }

        public async Task<IActionResult> Process(DeleteLayoutRequest request, IValidatorActionResult validatorActionResult)
        {
            IActionResult actionResult;

            if (validatorActionResult.Result == null)
            {
                var call = _cqsMapper.Map(request);

                var result = await _mediator.Send(call);

                actionResult = result.IsFailure ? _apiResponse.Fail(result)
                                                : _apiResponse.Deleted();
            }
            else
            {
                actionResult = validatorActionResult.Result;
            }

            return actionResult;
        }

        #endregion

        #region Operation

        public async Task<IActionResult> Process(ListOperationRequest request, IValidatorActionResult validatorActionResult)
        {
            IActionResult actionResult;

            if (validatorActionResult.Result == null)
            {
                request.UpdateFilter(fromToSet: new Dictionary<string, string>{
                        { "version", "dissalowed"}
                    });

                var call = _cqsMapper.Map(request);

                var result = await _mediator.Send(call);

                actionResult = result.IsFailure ? _apiResponse.Fail(result)
                                                : _apiResponse.Ok(((Result<OperationsModel>)result).Value);
            }
            else
            {
                actionResult = validatorActionResult.Result;
            }

            return actionResult;
        }

        public async Task<IActionResult> Process(GetOperationRequest request, IValidatorActionResult validatorActionResult)
        {
            IActionResult actionResult;

            if (validatorActionResult.Result == null)
            {
                var call = _cqsMapper.Map(request);

                var result = await _mediator.Send(call);

                actionResult = result.IsFailure ? _apiResponse.Fail(result)
                                                : _apiResponse.Ok(((Result<OperationModel>)result).Value, ((Result<OperationModel>)result).Version.ToString());
            }
            else
            {
                actionResult = validatorActionResult.Result;
            }

            return actionResult;
        }

        public async Task<IActionResult> Process(CreateOperationRequest request, IValidatorActionResult validatorActionResult)
        {
            IActionResult actionResult;

            if (validatorActionResult.Result == null)
            {
                var call = _cqsMapper.Map(request);

                var result = await _mediator.Send(call);

                actionResult = result.IsFailure ? _apiResponse.Fail(result)
                                                : _apiResponse.Created(string.Format("/api/operations/{0}", ((Result<Guid>)result).Value), result.Version.ToString());
            }
            else
            {
                actionResult = validatorActionResult.Result;
            }

            return actionResult;
        }

        public async Task<IActionResult> Process(UpdateOperationRequest request, IValidatorActionResult validatorActionResult)
        {
            IActionResult actionResult;

            if (validatorActionResult.Result == null)
            {
                var call = _cqsMapper.Map(request);

                var result = await _mediator.Send(call);

                actionResult = result.IsFailure ? _apiResponse.Fail(result)
                                                : _apiResponse.Updated(result.Version.ToString());
            }
            else
            {
                actionResult = validatorActionResult.Result;
            }

            return actionResult;
        }

        public async Task<IActionResult> Process(DeleteOperationRequest request, IValidatorActionResult validatorActionResult)
        {
            IActionResult actionResult;

            if (validatorActionResult.Result == null)
            {
                var call = _cqsMapper.Map(request);

                var result = await _mediator.Send(call);

                actionResult = result.IsFailure ? _apiResponse.Fail(result)
                                                : _apiResponse.Deleted();
            }
            else
            {
                actionResult = validatorActionResult.Result;
            }

            return actionResult;
        }

        #endregion

        #region Source

        public async Task<IActionResult> Process(ListSourceRequest request, IValidatorActionResult validatorActionResult)
        {
            IActionResult actionResult;

            if (validatorActionResult.Result == null)
            {
                request.UpdateFilter(fromToSet: new Dictionary<string, string>{
                        { "version", "dissalowed"}
                    });

                var call = _cqsMapper.Map(request);

                var result = await _mediator.Send(call);

                actionResult = result.IsFailure ? _apiResponse.Fail(result)
                                                : _apiResponse.Ok(((Result<SourcesModel>)result).Value);
            }
            else
            {
                actionResult = validatorActionResult.Result;
            }

            return actionResult;
        }

        public async Task<IActionResult> Process(GetSourceRequest request, IValidatorActionResult validatorActionResult)
        {
            IActionResult actionResult;

            if (validatorActionResult.Result == null)
            {
                var call = _cqsMapper.Map(request);

                var result = await _mediator.Send(call);

                actionResult = result.IsFailure ? _apiResponse.Fail(result)
                                                : _apiResponse.Ok(((Result<SourceModel>)result).Value, ((Result<SourceModel>)result).Version.ToString());
            }
            else
            {
                actionResult = validatorActionResult.Result;
            }

            return actionResult;
        }

        public async Task<IActionResult> Process(CreateSourceRequest request, IValidatorActionResult validatorActionResult)
        {
            IActionResult actionResult;

            if (validatorActionResult.Result == null)
            {
                var call = _cqsMapper.Map(request);

                var result = await _mediator.Send(call);

                actionResult = result.IsFailure ? _apiResponse.Fail(result)
                                                : _apiResponse.Created(string.Format("/api/sources/{0}", ((Result<Guid>)result).Value), result.Version.ToString());
            }
            else
            {
                actionResult = validatorActionResult.Result;
            }

            return actionResult;
        }

        public async Task<IActionResult> Process(UpdateSourceRequest request, IValidatorActionResult validatorActionResult)
        {
            IActionResult actionResult;

            if (validatorActionResult.Result == null)
            {
                var call = _cqsMapper.Map(request);

                var result = await _mediator.Send(call);

                actionResult = result.IsFailure ? _apiResponse.Fail(result)
                                                : _apiResponse.Updated(result.Version.ToString());
            }
            else
            {
                actionResult = validatorActionResult.Result;
            }

            return actionResult;
        }

        public async Task<IActionResult> Process(DeleteSourceRequest request, IValidatorActionResult validatorActionResult)
        {
            IActionResult actionResult;

            if (validatorActionResult.Result == null)
            {
                var call = _cqsMapper.Map(request);

                var result = await _mediator.Send(call);

                actionResult = result.IsFailure ? _apiResponse.Fail(result)
                                                : _apiResponse.Deleted();
            }
            else
            {
                actionResult = validatorActionResult.Result;
            }

            return actionResult;
        }

        #endregion

        #region Files
        public async Task<IActionResult> Process(UploadFileRequest request, IValidatorActionResult validatorActionResult)
        {
            IActionResult actionResult;

            if (validatorActionResult.Result == null)
            {
                var call = _cqsMapper.Map(request);

                var result = await _mediator.Send(call);

                actionResult = result.IsFailure ? _apiResponse.Fail(result)
                                                : _apiResponse.Created(((Result<string>)result).Value, "1");
            }
            else
            {
                actionResult = validatorActionResult.Result;
            }

            return actionResult;
        }

        public async Task<IActionResult> Process(DownloadFileRequest request, IValidatorActionResult validatorActionResult)
        {
            IActionResult actionResult;

            if (validatorActionResult.Result == null)
            {
                var call = _cqsMapper.Map(request);

                var result = await _mediator.Send(call);

                actionResult = result.IsFailure ? _apiResponse.Fail(result)
                                                : _apiResponse.Ok(((Result<FileDownloadModel>)result).Value);
            }
            else
            {
                actionResult = validatorActionResult.Result;
            }

            return actionResult;
        }
        #endregion

        #region OperatorActivities
        public async Task<IActionResult> Process(CreateOperatorActivityRequest request, IValidatorActionResult validatorActionResult)
        {
            IActionResult actionResult;

            if (validatorActionResult.Result == null)
            {
                var call = _cqsMapper.Map(request);

                var result = await _mediator.Send(call);

                actionResult = result.IsFailure ? _apiResponse.Fail(result)
                                                : _apiResponse.Created($"/api/operatorActivites/{((Result<Guid>)result).Value}", result.Version.ToString());

            }
            else
            {
                actionResult = validatorActionResult.Result;
            }

            return actionResult;
        }

        public async Task<IActionResult> Process(ListOperatorActivityRequest request, IValidatorActionResult validatorActionResult)
        {
            IActionResult actionResult;

            if (validatorActionResult.Result == null)
            {
                var call = _cqsMapper.Map(request);

                var result = await _mediator.Send(call);

                actionResult = result.IsFailure ? _apiResponse.Fail(result)
                                                : _apiResponse.Ok(((Result<OperatorActivitiesModel>)result).Value);
            }
            else
            {
                actionResult = validatorActionResult.Result;
            }

            return actionResult;
        }
        #endregion
    }
}
