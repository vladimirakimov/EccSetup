using FluentAssertions;
using ITG.Brix.EccSetup.API.Context.Bases;
using ITG.Brix.EccSetup.API.Context.Constants;
using ITG.Brix.EccSetup.API.Context.Resources;
using ITG.Brix.EccSetup.API.Context.Services.Requests.Models.From;
using ITG.Brix.EccSetup.API.Context.Services.Responses.Models.Errors;
using ITG.Brix.EccSetup.Application.Bases;
using ITG.Brix.EccSetup.Application.Cqs.Queries.Models;
using ITG.Brix.EccSetup.Application.Resources;
using ITG.Brix.EccSetup.IntegrationTests.Bases;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ITG.Brix.EccSetup.IntegrationTests.Controllers
{
    [TestClass]
    [TestCategory("Integration")]
    public class FlowsControllerTests
    {
        private HttpClient _client;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            ControllerTestsHelper.InitServer();
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
        }

        [TestInitialize]
        public void TestInitialize()
        {
            DatabaseHelper.Init("Flows");
            _client = ControllerTestsHelper.GetClient();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            _client.Dispose();
        }

        #region List

        [TestMethod]
        public async Task ListAllShouldSucceed()
        {
            // Arrange
            var apiVersion = "1.0";
            await ControllerHelper.CreateFlow("Name", "Description", "Image");

            // Act
            var response = await _client.GetAsync(string.Format("api/flows?api-version={0}", apiVersion));
            var responseBody = await response.Content.ReadAsStringAsync();
            var flowsModel = JsonConvert.DeserializeObject<FlowsModel>(responseBody);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            flowsModel.Value.Should().HaveCount(1);
            flowsModel.Count.Should().Be(1);
            flowsModel.NextLink.Should().BeNull();
        }

        [DataTestMethod]
        [DataRow("name eq 'Namea'", 1)]
        [DataRow("name ne 'Namea'", 25)]
        [DataRow("startswith(name, 'Name') eq true", 26)]
        [DataRow("startswith(name, 'Hello') eq false", 26)]
        [DataRow("startswith(name, 'Name') eq true and endswith(name, 'z') eq true", 1)]
        [DataRow("endswith(name, 'z') eq true", 1)]
        [DataRow("endswith(name, 'z') eq false", 25)]
        [DataRow("substringof('amea', name) eq true", 1)]
        [DataRow("tolower(name) eq 'namea'", 1)]
        [DataRow("toupper(name) eq 'NAMEA'", 1)]
        public async Task ListWithFilterShouldSucceed(string filter, int countResult)
        {
            // Arrange
            var name = "Name";
            var description = "Description";
            var image = "Image";

            var apiVersion = "1.0";

            string alphabet = "abcdefghijklmnopqrstuvwxyz";

            foreach (char c in alphabet)
            {
                await ControllerHelper.CreateFlow(name + c, description + c, image + c);
            }

            // Act
            var response = await _client.GetAsync(string.Format("api/flows?api-version={0}&$filter={1}", apiVersion, filter));
            var responseBody = await response.Content.ReadAsStringAsync();
            var flowsModel = JsonConvert.DeserializeObject<FlowsModel>(responseBody);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            flowsModel.Value.Should().HaveCount(countResult);
            flowsModel.Count.Should().Be(countResult);
            flowsModel.NextLink.Should().BeNull();
        }

        [TestMethod]
        public async Task ListFlowsWithFilterShouldSucceed()
        {
            //Arrange
            var filter = "source eq 'BKAL33%2BKBT%20T' and site eq 'Site' and operation eq 'Unload into warehouse' and department eq 'Operational department' and type-planning eq 'Type planning' and customer eq 'Customer' and prodsite eq 'Production site' and transport-type eq 'Transport type' and driver-wait eq 'No'";
            var name = "TestFilterFlow";
            var description = "Description";
            var image = "azure.com";
            var apiVersion = "1.0";

            //Act
            var flow = await ControllerHelper.CreateFlow(name, description, image);           
            var routeId = flow.Id;
            var ifmatch = flow.ETag;

            var jsonObj = new { name = "UpdatedName", diagram = "diagram", filterContent = "{\r\n   \"sources\": [\r\n      {         \r\n         \"name\": \"BKAL33+KBT T\"\r\n      }\r\n   ],\r\n   \"operations\": [\r\n      {\r\n         \"name\": \"Unload into warehouse\",\r\n      }\r\n   ],\r\n   \"sites\":[\r\n   {\r\n\t  \"Name\" : \"Site\"\r\n   }\r\n   ],\r\n   \"operationalDepartments\":[\r\n   {\r\n\t\t\"Name\": \"Operational department\"\r\n   }\r\n   ],\r\n   \"typePlannings\":[\r\n   {\r\n\t\t\"Name\": \"Type planning\"\r\n   }\r\n   ],\r\n   \"customers\":[\r\n   {\r\n\t\t\"Name\": \"Customer\"\r\n   }\r\n   ],\r\n   \"productionSites\":[\r\n   {\r\n\t\t\"Name\": \"Production site\"\r\n   }\r\n   ],\r\n   \"transportTypes\": [\r\n   {\r\n\t\t\"Name\": \"Transport type\"\r\n   }\r\n   ],\r\n   \"driverWait\": \"No\"\r\n}" };
            string jsonInString = JsonConvert.SerializeObject(jsonObj);

            // Act
            _client.DefaultRequestHeaders.Add("If-Match", ifmatch);
            await _client.PatchAsync(string.Format("api/flows/{0}?api-version={1}", routeId, apiVersion), new StringContent(jsonInString, Encoding.UTF8, "application/json"));
            var response = await _client.GetAsync($"api/flows?api-version={apiVersion}&$filter={filter}");
            var responseBody = await response.Content.ReadAsStringAsync();
            var flowsModel = JsonConvert.DeserializeObject<FlowsModel>(responseBody);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            flowsModel.Value.Should().NotBeEmpty();

        }

        [TestMethod]
        public async Task ListFlowsWithSourcePorpertyFilterShouldSucceed()
        {
            //Arrange
            var filter = "source eq 'BKAL33%2BKBT%20T' and site eq 'Site' and operation eq 'Unload into warehouse' and department eq 'Operational department' and type-planning eq 'Type planning' and customer eq 'Customer' and prodsite eq 'Production site' and transport-type eq 'Transport type' and driver-wait eq 'No'";
            var name = "TestFilterFlow";
            var description = "Description";
            var image = "azure.com";
            var apiVersion = "1.0";

            //Act
            var flow = await ControllerHelper.CreateFlow(name, description, image);
            var routeId = flow.Id;
            var ifmatch = flow.ETag;

            var jsonObj = new { name = "UpdatedName", diagram = "diagram", filterContent = "" };
            string jsonInString = JsonConvert.SerializeObject(jsonObj);

            // Act
            _client.DefaultRequestHeaders.Add("If-Match", ifmatch);
            await _client.PatchAsync(string.Format("api/flows/{0}?api-version={1}", routeId, apiVersion), new StringContent(jsonInString, Encoding.UTF8, "application/json"));
            var response = await _client.GetAsync($"api/flows?api-version={apiVersion}&$filter={filter}");
            var responseBody = await response.Content.ReadAsStringAsync();
            var flowsModel = JsonConvert.DeserializeObject<FlowsModel>(responseBody);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            flowsModel.Value.Should().NotBeEmpty();

        }

        [TestMethod]
        public async Task ListWithFilterFlowShouldFailIfIsNotSupported()
        {
            //Arrange
            var filter = "source eq 'BKAL33%2BKBT%20T' and site eq 'Site' and operation eq 'Unload into warehouse' and";
            var name = "FilterFlow";
            var description = "Description";
            var image = "azure.com/2434.jpg";
            var apiVersion = "1.0";

            var expectedError = new ResponseError
            {
                Error = new ResponseErrorBody
                {
                    Code = ServiceError.InvalidQueryParameterValue.Code,
                    Message = ServiceError.InvalidQueryParameterValue.Message,
                    Details = new List<ResponseErrorField>
                    {
                        new ResponseErrorField
                        {
                            Code = Consts.Failure.Detail.Code.InvalidQueryFilter,
                            Message = HandlerFailures.InvalidQueryFilter,
                            Target = Consts.Failure.Detail.Target.QueryFilter
                        }
                    }
                }
            };

            //Act
            await ControllerHelper.CreateFlow(name, description, image);
            var response = await _client.GetAsync($"api/flows?api-version={apiVersion}&$filter={filter}");
            var responseBody = await response.Content.ReadAsStringAsync();
            var error = JsonConvert.DeserializeObject<ResponseError>(responseBody);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            error.Should().Be(expectedError);
        }        

        [DataTestMethod]
        [DataRow(0, 10, 10)]
        [DataRow(1, 10, 10)]
        [DataRow(10, 100, 16)]
        public async Task ListWithSkipAndTopShouldSucceed(int skip, int top, int countResult)
        {
            // Arrange
            var name = "Name";
            var description = "Description";
            var image = "Image";

            var apiVersion = "1.0";

            string alphabet = "abcdefghijklmnopqrstuvwxyz";

            foreach (char c in alphabet)
            {
                await ControllerHelper.CreateFlow(name + c, description + c, image + c);
            }


            // Act
            var response = await _client.GetAsync(string.Format("api/flows?api-version={0}&$skip={1}&$top={2}", apiVersion, skip, top));
            var responseBody = await response.Content.ReadAsStringAsync();
            var flowsModel = JsonConvert.DeserializeObject<FlowsModel>(responseBody);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            flowsModel.Value.Should().HaveCount(countResult);
            flowsModel.Count.Should().Be(countResult);
            flowsModel.NextLink.Should().BeNull();
        }

        [DataTestMethod]
        [DataRow("name ne 'Namea'", 0, 10, 10)]
        [DataRow("name ne 'Namea'", 0, 30, 25)]
        [DataRow("name ne 'Namea'", 0, 10, 10)]
        [DataRow("name ne 'Namea'", 20, 10, 5)]
        public async Task ListWithFilterSkipTopShouldSucceed(string filter, int skip, int top, int countResult)
        {
            // Arrange
            var name = "Name";
            var description = "Description";
            var image = "Image";

            var apiVersion = "1.0";

            string alphabet = "abcdefghijklmnopqrstuvwxyz";

            foreach (char c in alphabet)
            {
                await ControllerHelper.CreateFlow(name + c, description + c, image + c);
            }


            // Act
            var response = await _client.GetAsync(string.Format("api/flows?api-version={0}&$filter={1}&$skip={2}&$top={3}", apiVersion, filter, skip, top));
            var responseBody = await response.Content.ReadAsStringAsync();
            var flowsModel = JsonConvert.DeserializeObject<FlowsModel>(responseBody);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            flowsModel.Value.Should().HaveCount(countResult);
            flowsModel.Count.Should().Be(countResult);
            flowsModel.NextLink.Should().BeNull();
        }

        [DataTestMethod]
        [DataRow("version eq 1")]
        [DataRow("image neq 'unexistent'")]
        [DataRow("surname eq 'Namea'")]
        [DataRow("login")]
        public async Task ListShouldFailWhenQueryFilterIsNotValid(string filter)
        {
            // Arrange
            var apiVersion = "1.0";

            var expectedError = new ResponseError
            {
                Error = new ResponseErrorBody
                {
                    Code = ServiceError.InvalidQueryParameterValue.Code,
                    Message = ServiceError.InvalidQueryParameterValue.Message,
                    Details = new List<ResponseErrorField>
                    {
                        new ResponseErrorField
                        {
                            Code = Consts.Failure.Detail.Code.InvalidQueryFilter,
                            Message = HandlerFailures.InvalidQueryFilter,
                            Target = Consts.Failure.Detail.Target.QueryFilter
                        }
                    }
                }
            };

            // Act
            var response = await _client.GetAsync(string.Format("api/flows?api-version={0}&$filter={1}", apiVersion, filter));
            var body = await response.Content.ReadAsStringAsync();
            var error = JsonConvert.DeserializeObject<ResponseError>(body);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            body.Should().NotBeNull();
            error.Should().Be(expectedError);
        }

        [DataTestMethod]
        [DataRow("concat(concat(name, ', '), name) eq 'Name, Name'")]
        [DataRow("length(name) eq 9")]
        [DataRow("replace(name, ' ', '') eq 'Name'")]
        [DataRow("trim(name) eq 'Name'")]
        public async Task ListShouldFailWhenQueryFilterHasUnsupportedFunctions(string filter)
        {
            // Arrange
            var apiVersion = "1.0";

            var expectedError = new ResponseError
            {
                Error = new ResponseErrorBody
                {
                    Code = ServiceError.InvalidQueryParameterValue.Code,
                    Message = ServiceError.InvalidQueryParameterValue.Message,
                    Details = new List<ResponseErrorField>
                    {
                        new ResponseErrorField
                        {
                            Code = Consts.Failure.Detail.Code.InvalidQueryFilter,
                            Message = HandlerFailures.InvalidQueryFilter,
                            Target = Consts.Failure.Detail.Target.QueryFilter
                        }
                    }
                }
            };


            // Act
            var response = await _client.GetAsync(string.Format("api/flows?api-version={0}&$filter={1}", apiVersion, filter));
            var body = await response.Content.ReadAsStringAsync();
            var error = JsonConvert.DeserializeObject<ResponseError>(body);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            body.Should().NotBeNull();
            error.Should().Be(expectedError);
        }

        [DataTestMethod]
        [DataRow("")]
        [DataRow("    ")]
        public async Task ListShouldSucceedWhenQueryTopPresentButUnset(string top)
        {
            // Arrange
            var apiVersion = "1.0";

            // Act
            var response = await _client.GetAsync(string.Format("api/flows?api-version={0}&$top={1}", apiVersion, top));

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [DataTestMethod]
        [DataRow("some invalid value - not a sequence of digits")]
        [DataRow("null")]
        [DataRow("''")]
        [DataRow("'   '")]
        public async Task ListShouldFailWhenQueryTopIsNotValid(string top)
        {
            // Arrange
            var apiVersion = "1.0";

            var expectedError = new ResponseError
            {
                Error = new ResponseErrorBody
                {
                    Code = ServiceError.InvalidQueryParameterValue.Code,
                    Message = ServiceError.InvalidQueryParameterValue.Message,
                    Details = new List<ResponseErrorField>
                            {
                                new ResponseErrorField
                                {
                                    Code = Consts.Failure.Detail.Code.InvalidQueryTop,
                                    Message = CustomFailures.TopInvalid,
                                    Target = Consts.Failure.Detail.Target.QueryTop
                                }
                            }
                }
            };

            // Act
            var response = await _client.GetAsync(string.Format("api/flows?api-version={0}&$top={1}", apiVersion, top));
            var body = await response.Content.ReadAsStringAsync();
            var error = JsonConvert.DeserializeObject<ResponseError>(body);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            body.Should().NotBeNull();
            error.Should().Be(expectedError);
        }

        [DataTestMethod]
        [DataRow("0")]
        [DataRow("-1")]
        [DataRow("99999999999999999999999")]
        public async Task ListShouldFailWhenQueryTopNotInRange(string top)
        {
            // Arrange
            var apiVersion = "1.0";

            var expectedError = new ResponseError
            {
                Error = new ResponseErrorBody
                {
                    Code = ServiceError.InvalidQueryParameterValue.Code,
                    Message = ServiceError.InvalidQueryParameterValue.Message,
                    Details = new List<ResponseErrorField>
                            {
                                new ResponseErrorField
                                {
                                    Code = Consts.Failure.Detail.Code.InvalidQueryTop,
                                    Message = string.Format(CustomFailures.TopRange, Application.Constants.Consts.CqsValidation.TopMaxValue),
                                    Target = Consts.Failure.Detail.Target.QueryTop
                                }
                            }
                }
            };

            // Act
            var response = await _client.GetAsync(string.Format("api/flows?api-version={0}&$top={1}", apiVersion, top));
            var body = await response.Content.ReadAsStringAsync();
            var error = JsonConvert.DeserializeObject<ResponseError>(body);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            body.Should().NotBeNull();
            error.Should().Be(expectedError);
        }

        [DataTestMethod]
        [DataRow("")]
        [DataRow("    ")]
        public async Task ListShouldSucceedWhenQuerySkipPresentButUnset(string skip)
        {
            // Arrange
            var apiVersion = "1.0";

            // Act
            var response = await _client.GetAsync(string.Format("api/flows?api-version={0}&$skip={1}", apiVersion, skip));

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [DataTestMethod]
        [DataRow("some invalid value - not a sequence of digits")]
        [DataRow("null")]
        [DataRow("''")]
        [DataRow("'   '")]
        public async Task ListShouldFailWhenQuerySkipIsNotValid(string skip)
        {
            // Arrange
            var apiVersion = "1.0";

            var expectedError = new ResponseError
            {
                Error = new ResponseErrorBody
                {
                    Code = ServiceError.InvalidQueryParameterValue.Code,
                    Message = ServiceError.InvalidQueryParameterValue.Message,
                    Details = new List<ResponseErrorField>
                            {
                                new ResponseErrorField
                                {
                                    Code = Consts.Failure.Detail.Code.InvalidQuerySkip,
                                    Message = CustomFailures.SkipInvalid,
                                    Target = Consts.Failure.Detail.Target.QuerySkip
                                }
                            }
                }
            };

            // Act
            var response = await _client.GetAsync(string.Format("api/flows?api-version={0}&$skip={1}", apiVersion, skip));
            var body = await response.Content.ReadAsStringAsync();
            var error = JsonConvert.DeserializeObject<ResponseError>(body);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            body.Should().NotBeNull();
            error.Should().Be(expectedError);
        }

        [DataTestMethod]
        [DataRow("-1")]
        [DataRow("99999999999999999999999")]
        public async Task ListShouldFailWhenQuerySkipNotInRange(string skip)
        {
            // Arrange
            var apiVersion = "1.0";

            var expectedError = new ResponseError
            {
                Error = new ResponseErrorBody
                {
                    Code = ServiceError.InvalidQueryParameterValue.Code,
                    Message = ServiceError.InvalidQueryParameterValue.Message,
                    Details = new List<ResponseErrorField>
                            {
                                new ResponseErrorField
                                {
                                    Code = Consts.Failure.Detail.Code.InvalidQuerySkip,
                                    Message = string.Format(CustomFailures.SkipRange, Application.Constants.Consts.CqsValidation.SkipMaxValue),
                                    Target = Consts.Failure.Detail.Target.QuerySkip
                                }
                            }
                }
            };

            // Act
            var response = await _client.GetAsync(string.Format("api/flows?api-version={0}&$skip={1}", apiVersion, skip));
            var body = await response.Content.ReadAsStringAsync();
            var error = JsonConvert.DeserializeObject<ResponseError>(body);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            body.Should().NotBeNull();
            error.Should().Be(expectedError);
        }


        [TestMethod]
        public async Task ListShouldFailWhenQueryApiVersionIsMissing()
        {
            // Arrange
            var expectedError = new ResponseError
            {
                Error = new ResponseErrorBody
                {
                    Code = ServiceError.MissingRequiredQueryParameter.Code,
                    Message = ServiceError.MissingRequiredQueryParameter.Message,
                    Details = new List<ResponseErrorField>
                    {
                        new ResponseErrorField
                        {
                            Code = Consts.Failure.Detail.Code.Missing,
                            Message = string.Format(RequestFailures.QueryParameterRequired, "api-version"),
                            Target = Consts.Failure.Detail.Target.ApiVersion
                        }
                    }
                }
            };

            // Act
            var response = await _client.GetAsync("api/flows");
            var body = await response.Content.ReadAsStringAsync();
            var error = JsonConvert.DeserializeObject<ResponseError>(body);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            body.Should().NotBeNull();
            error.Should().Be(expectedError);
        }

        [TestMethod]
        public async Task ListShouldFailWhenQueryApiVersionIsInvalid()
        {
            // Arrange
            var apiVersion = "4.0";

            var expectedError = new ResponseError
            {
                Error = new ResponseErrorBody
                {
                    Code = ServiceError.InvalidQueryParameterValue.Code,
                    Message = ServiceError.InvalidQueryParameterValue.Message,
                    Details = new List<ResponseErrorField>
                    {
                        new ResponseErrorField
                        {
                            Code = Consts.Failure.Detail.Code.Invalid,
                            Message = string.Format(RequestFailures.QueryParameterInvalidValue, "api-version"),
                            Target = Consts.Failure.Detail.Target.ApiVersion
                        }
                    }
                }
            };

            // Act
            var response = await _client.GetAsync(string.Format("api/flows?api-version={0}", apiVersion));
            var body = await response.Content.ReadAsStringAsync();
            var error = JsonConvert.DeserializeObject<ResponseError>(body);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            body.Should().NotBeNull();
            error.Should().Be(expectedError);
        }

        #endregion

        #region Get

        [TestMethod]
        public async Task GetShouldSucceed()
        {
            // Arrange
            var name = "Name";
            var description = "Description";
            var image = "Image";

            var apiVersion = "1.0";

            var createdRecordResult = await ControllerHelper.CreateFlow(name, description, image);
            var routeId = createdRecordResult.Id;
            var eTag = createdRecordResult.ETag;


            // Act
            var response = await _client.GetAsync(string.Format("api/flows/{0}?api-version={1}", routeId, apiVersion));
            var responseBody = await response.Content.ReadAsStringAsync();
            var businessUnitModel = JsonConvert.DeserializeObject<FlowModel>(responseBody);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            response.Headers.ETag.Should().NotBeNull();
            response.Headers.ETag.Tag.Should().Be(eTag);
            businessUnitModel.Name.Should().Be(name);
        }

        [TestMethod]
        public async Task GetShouldFailWhenRouteIdIsInvalid()
        {
            // Arrange
            var routeId = "someInvalidId";

            var expectedResponseError = new ResponseError
            {
                Error = new ResponseErrorBody
                {
                    Code = ServiceError.ResourceNotFound.Code,
                    Message = ServiceError.ResourceNotFound.Message,
                    Details = new List<ResponseErrorField>
                    {
                        new ResponseErrorField
                        {
                            Code = Consts.Failure.Detail.Code.NotFound,
                            Message = RequestFailures.EntityNotFoundByIdentifier,
                            Target = Consts.Failure.Detail.Target.Id
                        }
                    }
                }
            };

            // Act
            var response = await _client.GetAsync(string.Format("api/flows/{0}", routeId));
            var body = await response.Content.ReadAsStringAsync();
            var error = JsonConvert.DeserializeObject<ResponseError>(body);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
            body.Should().NotBeNull();
            error.Should().Be(expectedResponseError);
        }

        [TestMethod]
        public async Task GetShouldFailWhenRouteIdDoesNotExist()
        {
            // Arrange
            var apiVersion = "1.0";
            var routeId = Guid.NewGuid();

            var expectedResponseError = new ResponseError
            {
                Error = new ResponseErrorBody
                {
                    Code = ServiceError.ResourceNotFound.Code,
                    Message = ServiceError.ResourceNotFound.Message,
                    Details = new List<ResponseErrorField>
                    {
                        new ResponseErrorField
                        {
                            Code = Consts.Failure.Detail.Code.NotFound,
                            Message = string.Format(HandlerFailures.NotFound, "Flow"),
                            Target = Consts.Failure.Detail.Target.Id
                        }
                    }
                }
            };

            // Act
            var response = await _client.GetAsync(string.Format("api/flows/{0}?api-version={1}", routeId, apiVersion));
            var body = await response.Content.ReadAsStringAsync();
            var error = JsonConvert.DeserializeObject<ResponseError>(body);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
            body.Should().NotBeNull();
            error.Should().Be(expectedResponseError);
        }

        [TestMethod]
        public async Task GetShouldFailWhenRouteIdIsEmptyGuid()
        {
            // Arrange
            var apiVersion = "1.0";
            var routeId = Guid.Empty.ToString();

            var expectedError = new ResponseError
            {
                Error = new ResponseErrorBody
                {
                    Code = ServiceError.ResourceNotFound.Code,
                    Message = ServiceError.ResourceNotFound.Message,
                    Details = new List<ResponseErrorField>
                    {
                        new ResponseErrorField
                        {
                            Code = Consts.Failure.Detail.Code.NotFound,
                            Message = CustomFailures.FlowNotFound,
                            Target = Consts.Failure.Detail.Target.Id
                        }
                    }
                }
            };

            // Act
            var response = await _client.GetAsync(string.Format("api/flows/{0}?api-version={1}", routeId, apiVersion));
            var body = await response.Content.ReadAsStringAsync();
            var error = JsonConvert.DeserializeObject<ResponseError>(body);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
            body.Should().NotBeNull();
            error.Should().Be(expectedError);
        }

        [TestMethod]
        public async Task GetShouldFailWhenQueryApiVersionIsMissing()
        {
            // Arrange
            var routeId = Guid.NewGuid().ToString();

            var expectedError = new ResponseError
            {
                Error = new ResponseErrorBody
                {
                    Code = ServiceError.MissingRequiredQueryParameter.Code,
                    Message = ServiceError.MissingRequiredQueryParameter.Message,
                    Details = new List<ResponseErrorField>
                    {
                        new ResponseErrorField
                        {
                            Code = Consts.Failure.Detail.Code.Missing,
                            Message = string.Format(RequestFailures.QueryParameterRequired, "api-version"),
                            Target = Consts.Failure.Detail.Target.ApiVersion
                        }
                    }
                }
            };

            // Act
            var response = await _client.GetAsync(string.Format("api/flows/{0}", routeId));
            var body = await response.Content.ReadAsStringAsync();
            var error = JsonConvert.DeserializeObject<ResponseError>(body);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            body.Should().NotBeNull();
            error.Should().Be(expectedError);
        }

        [TestMethod]
        public async Task GetShouldFailWhenQueryApiVersionIsInvalid()
        {
            // Arrange
            var apiVersion = "4.0";
            var routeId = Guid.NewGuid().ToString();

            var expectedError = new ResponseError
            {
                Error = new ResponseErrorBody
                {
                    Code = ServiceError.InvalidQueryParameterValue.Code,
                    Message = ServiceError.InvalidQueryParameterValue.Message,
                    Details = new List<ResponseErrorField>
                    {
                        new ResponseErrorField
                        {
                            Code = Consts.Failure.Detail.Code.Invalid,
                            Message = string.Format(RequestFailures.QueryParameterInvalidValue, "api-version"),
                            Target = Consts.Failure.Detail.Target.ApiVersion
                        }
                    }
                }
            };

            // Act
            var response = await _client.GetAsync(string.Format("api/flows/{0}?api-version={1}", routeId, apiVersion));
            var body = await response.Content.ReadAsStringAsync();
            var error = JsonConvert.DeserializeObject<ResponseError>(body);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            body.Should().NotBeNull();
            error.Should().Be(expectedError);
        }

        #endregion

        #region Create

        [TestMethod]
        public async Task CreateShouldSucceed()
        {
            // Arrange
            var name = "Name";
            var description = "Description";
            var image = "Image";

            var apiVersion = "1.0";
            var body = new CreateFlowFromBody()
            {
                Name = name,
                Description = description,
                Image = image
            };
            var jsonBody = JsonConvert.SerializeObject(body);

            // Act
            var response = await _client.PostAsync(string.Format("api/flows?api-version={0}", apiVersion), new StringContent(jsonBody, Encoding.UTF8, "application/json"));
            var responseBody = await response.Content.ReadAsStringAsync();

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Created);
            response.Headers.Location.Should().NotBeNull();
            response.Headers.ETag.Should().NotBeNull();
            responseBody.Should().BeNullOrEmpty();
        }

        [TestMethod]
        public async Task CreateShouldFailWhenBodyIsNonJsonContentType()
        {
            // Arrange
            var name = "Name";
            var description = "Description";
            var image = "Image";

            var apiVersion = "1.0";
            var body = new CreateFlowFromBody()
            {
                Name = name,
                Description = description,
                Image = image
            };
            var jsonBody = JsonConvert.SerializeObject(body);

            // Act
            var response = await _client.PostAsync(string.Format("api/flows?api-version={0}", apiVersion), new StringContent(jsonBody, Encoding.UTF8, "text/plain"));
            var responseBody = await response.Content.ReadAsStringAsync();

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.UnsupportedMediaType);
            responseBody.Should().BeNullOrEmpty();
        }

        [TestMethod]
        public async Task CreateShouldFailWhenQueryApiVersionIsMissing()
        {
            // Arrange
            var name = "Name";
            var description = "Description";
            var image = "Image";

            var apiVersion = "1.0";
            var body = new CreateFlowFromBody()
            {
                Name = name,
                Description = description,
                Image = image
            };
            var jsonBody = JsonConvert.SerializeObject(body);

            var expectedError = new ResponseError
            {
                Error = new ResponseErrorBody
                {
                    Code = ServiceError.MissingRequiredQueryParameter.Code,
                    Message = ServiceError.MissingRequiredQueryParameter.Message,
                    Details = new List<ResponseErrorField>
                    {
                        new ResponseErrorField
                        {
                            Code = Consts.Failure.Detail.Code.Missing,
                            Message = string.Format(RequestFailures.QueryParameterRequired, "api-version"),
                            Target = Consts.Failure.Detail.Target.ApiVersion
                        }
                    }
                }
            };

            // Act
            var response = await _client.PostAsync("api/flows", new StringContent(jsonBody, Encoding.UTF8, "application/json"));
            var responseBody = await response.Content.ReadAsStringAsync();
            var error = JsonConvert.DeserializeObject<ResponseError>(responseBody);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            responseBody.Should().NotBeNull();
            error.Should().Be(expectedError);
        }

        [TestMethod]
        public async Task CreateShouldFailWhenQueryApiVersionIsInvalid()
        {
            // Arrange
            var name = "Name";
            var description = "Description";
            var image = "Image";

            var apiVersion = "4.0";
            var body = new CreateFlowFromBody()
            {
                Name = name,
                Description = description,
                Image = image
            };
            var jsonBody = JsonConvert.SerializeObject(body);

            var expectedError = new ResponseError
            {
                Error = new ResponseErrorBody
                {
                    Code = ServiceError.InvalidQueryParameterValue.Code,
                    Message = ServiceError.InvalidQueryParameterValue.Message,
                    Details = new List<ResponseErrorField>
                    {
                        new ResponseErrorField
                        {
                            Code = Consts.Failure.Detail.Code.Invalid,
                            Message = string.Format(RequestFailures.QueryParameterInvalidValue, "api-version"),
                            Target = Consts.Failure.Detail.Target.ApiVersion
                        }
                    }
                }
            };

            // Act
            var response = await _client.PostAsync(string.Format("api/flows?api-version={0}", apiVersion), new StringContent(jsonBody, Encoding.UTF8, "application/json"));
            var responseBody = await response.Content.ReadAsStringAsync();
            var error = JsonConvert.DeserializeObject<ResponseError>(responseBody);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            responseBody.Should().NotBeNull();
            error.Should().Be(expectedError);
        }

        [TestMethod]
        public async Task CreateShouldFailWhenNameIsNull()
        {
            // Arrange
            string name = null;
            var description = "Description";
            var image = "Image";

            var apiVersion = "1.0";
            var body = new CreateFlowFromBody()
            {
                Name = name,
                Description = description,
                Image = image
            };
            var jsonBody = JsonConvert.SerializeObject(body);

            var expectedError = new ResponseError
            {
                Error = new ResponseErrorBody
                {
                    Code = ServiceError.InvalidInput.Code,
                    Message = ServiceError.InvalidInput.Message,
                    Details = new List<ResponseErrorField>
                    {
                        new ResponseErrorField
                        {
                            Code = "invalid-input",
                            Message = ValidationFailures.FlowNameMandatory,
                            Target = "name"
                        }
                    }
                }
            };

            // Act
            var response = await _client.PostAsync(string.Format("api/flows?api-version={0}", apiVersion), new StringContent(jsonBody, Encoding.UTF8, "application/json"));
            var responseBody = await response.Content.ReadAsStringAsync();
            var error = JsonConvert.DeserializeObject<ResponseError>(responseBody);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            responseBody.Should().NotBeNull();
            error.Should().Be(expectedError);
        }

        [TestMethod]
        public async Task CreateShouldFailWhenNameIsEmpty()
        {
            // Arrange
            var name = string.Empty;
            var description = "Description";
            var image = "Image";

            var apiVersion = "1.0";
            var body = new CreateFlowFromBody()
            {
                Name = name,
                Description = description,
                Image = image
            };
            var jsonBody = JsonConvert.SerializeObject(body);

            var expectedError = new ResponseError
            {
                Error = new ResponseErrorBody
                {
                    Code = ServiceError.InvalidInput.Code,
                    Message = ServiceError.InvalidInput.Message,
                    Details = new List<ResponseErrorField>
                    {
                        new ResponseErrorField
                        {
                            Code = "invalid-input",
                            Message = ValidationFailures.FlowNameMandatory,
                            Target = "name"
                        }
                    }
                }
            };

            // Act
            var response = await _client.PostAsync(string.Format("api/flows?api-version={0}", apiVersion), new StringContent(jsonBody, Encoding.UTF8, "application/json"));
            var responseBody = await response.Content.ReadAsStringAsync();
            var error = JsonConvert.DeserializeObject<ResponseError>(responseBody);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            responseBody.Should().NotBeNull();
            error.Should().Be(expectedError);
        }

        [TestMethod]
        public async Task CreateShouldFailWhenNameIsIsWhiteSpace()
        {
            // Arrange
            var name = "   ";
            var description = "Description";
            var image = "Image";

            var apiVersion = "1.0";
            var body = new CreateFlowFromBody()
            {
                Name = name,
                Description = description,
                Image = image
            };
            var jsonBody = JsonConvert.SerializeObject(body);

            var expectedError = new ResponseError
            {
                Error = new ResponseErrorBody
                {
                    Code = ServiceError.InvalidInput.Code,
                    Message = ServiceError.InvalidInput.Message,
                    Details = new List<ResponseErrorField>
                    {
                        new ResponseErrorField
                        {
                            Code = "invalid-input",
                            Message = ValidationFailures.FlowNameMandatory,
                            Target = "name"
                        }
                    }
                }
            };

            // Act
            var response = await _client.PostAsync(string.Format("api/flows?api-version={0}", apiVersion), new StringContent(jsonBody, Encoding.UTF8, "application/json"));
            var responseBody = await response.Content.ReadAsStringAsync();
            var error = JsonConvert.DeserializeObject<ResponseError>(responseBody);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            responseBody.Should().NotBeNull();
            error.Should().Be(expectedError);
        }

        [DataTestMethod]
        [DataRow(" Flow")]
        [DataRow("Flow ")]
        [DataRow(" Flow ")]
        [DataRow("  Flow  ")]
        public async Task CreateShouldFailWhenNameStartsOrEndsWithWhiteSpace(string symbols)
        {
            // Arrange
            var name = symbols;
            var description = "Description";
            var image = "Image";

            var apiVersion = "1.0";
            var body = new CreateFlowFromBody()
            {
                Name = name,
                Description = description,
                Image = image
            };
            var jsonBody = JsonConvert.SerializeObject(body);

            var expectedError = new ResponseError
            {
                Error = new ResponseErrorBody
                {
                    Code = ServiceError.InvalidInput.Code,
                    Message = ServiceError.InvalidInput.Message,
                    Details = new List<ResponseErrorField>
                    {
                        new ResponseErrorField
                        {
                            Code = "invalid-input",
                            Message = ValidationFailures.FlowNameCannotStartOrEndWithWhiteSpace,
                            Target = "name"
                        }
                    }
                }
            };

            // Act
            var response = await _client.PostAsync(string.Format("api/flows?api-version={0}", apiVersion), new StringContent(jsonBody, Encoding.UTF8, "application/json"));
            var responseBody = await response.Content.ReadAsStringAsync();
            var error = JsonConvert.DeserializeObject<ResponseError>(responseBody);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            responseBody.Should().NotBeNull();
            error.Should().Be(expectedError);
        }

        [TestMethod]
        public async Task CreateShouldFailWhenNameAlreadyExists()
        {
            // Arrange
            var name = "Name";
            var description = "Description";
            var image = "Image";

            var apiVersion = "1.0";
            await ControllerHelper.CreateFlow(name, description, image);

            var body = new CreateFlowFromBody()
            {
                Name = name
            };
            var jsonBody = JsonConvert.SerializeObject(body);

            var expectedError = new ResponseError
            {
                Error = new ResponseErrorBody
                {
                    Code = ServiceError.ResourceAlreadyExists.Code,
                    Message = ServiceError.ResourceAlreadyExists.Message,
                    Details = new List<ResponseErrorField>
                    {
                        new ResponseErrorField
                        {
                            Code = "conflict",
                            Message = HandlerFailures.Conflict,
                            Target = "name"
                        }
                    }
                }
            };

            // Act
            var response = await _client.PostAsync(string.Format("api/flows?api-version={0}", apiVersion), new StringContent(jsonBody, Encoding.UTF8, "application/json"));
            var responseBody = await response.Content.ReadAsStringAsync();
            var error = JsonConvert.DeserializeObject<ResponseError>(responseBody);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Conflict);
            responseBody.Should().NotBeNull();
            error.Should().Be(expectedError);
        }

        #endregion

        #region Update

        [TestMethod]
        public async Task UpdateShouldSucceed()
        {
            // Arrange
            var name = "Name";
            var description = "Description";
            var image = "Image";

            var apiVersion = "1.0";
            var createdRecordResult = await ControllerHelper.CreateFlow(name, description, image);
            var routeId = createdRecordResult.Id;
            var ifmatch = createdRecordResult.ETag;

            var jsonObj = new { name = "UpdatedName", diagram = "diagram", filterContent = "{}" };
            string jsonInString = JsonConvert.SerializeObject(jsonObj);

            // Act
            _client.DefaultRequestHeaders.Add("If-Match", ifmatch);
            var response = await _client.PatchAsync(string.Format("api/flows/{0}?api-version={1}", routeId, apiVersion), new StringContent(jsonInString, Encoding.UTF8, "application/json"));
            var body = await response.Content.ReadAsStringAsync();

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
            response.Headers.ETag.Should().NotBeNull();
            body.Should().BeNullOrEmpty();
        }

        [TestMethod]
        public async Task UpdateShouldFailWhenBodyIsNonJsonContentType()
        {
            // Arrange
            var name = "Name";
            var description = "Description";
            var image = "Image";

            var apiVersion = "1.0";
            var createdRecordResult = await ControllerHelper.CreateFlow(name, description, image);
            var routeId = createdRecordResult.Id;
            var ifmatch = createdRecordResult.ETag;

            var jsonObj = new { name = "UpdatedName" };
            string jsonInString = JsonConvert.SerializeObject(jsonObj);

            var expectedResponseError = new ResponseError
            {
                Error = new ResponseErrorBody
                {
                    Code = ServiceError.UnsupportedMediaType.Code,
                    Message = ServiceError.UnsupportedMediaType.Message,
                    Details = new List<ResponseErrorField>
                    {
                        new ResponseErrorField
                        {
                            Code = Consts.Failure.Detail.Code.Unsupported,
                            Message = string.Format(RequestFailures.HeaderUnsupportedValue, "Content-Type"),
                            Target = Consts.Failure.Detail.Target.ContentType
                        }
                    }
                }
            };

            // Act
            _client.DefaultRequestHeaders.Add("If-Match", ifmatch);
            var response = await _client.PatchAsync(string.Format("api/flows/{0}?api-version={1}", routeId, apiVersion), new StringContent(jsonInString, Encoding.UTF8, "text/plain"));
            var body = await response.Content.ReadAsStringAsync();
            var error = JsonConvert.DeserializeObject<ResponseError>(body);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.UnsupportedMediaType);
            body.Should().NotBeNull();
            error.Should().Be(expectedResponseError);
        }

        [TestMethod]
        public async Task UpdateShouldFailWhenRouteIdIsInvalid()
        {
            // Arrange
            var name = "Name";
            var description = "Description";
            var image = "Image";

            var apiVersion = "1.0";
            var createdRecordResult = await ControllerHelper.CreateFlow(name, description, image);
            var routeId = "someInvalidRouteId";
            var ifmatch = createdRecordResult.ETag;

            var jsonObj = new { name = "UpdatedName" };
            string jsonInString = JsonConvert.SerializeObject(jsonObj);

            var expectedResponseError = new ResponseError
            {
                Error = new ResponseErrorBody
                {
                    Code = ServiceError.ResourceNotFound.Code,
                    Message = ServiceError.ResourceNotFound.Message,
                    Details = new List<ResponseErrorField>
                    {
                        new ResponseErrorField
                        {
                            Code = Consts.Failure.Detail.Code.NotFound,
                            Message = RequestFailures.EntityNotFoundByIdentifier,
                            Target = Consts.Failure.Detail.Target.Id
                        }
                    }
                }
            };

            // Act
            _client.DefaultRequestHeaders.Add("If-Match", ifmatch);
            var response = await _client.PatchAsync(string.Format("api/flows/{0}?api-version={1}", routeId, apiVersion), new StringContent(jsonInString, Encoding.UTF8, "application/json"));
            var body = await response.Content.ReadAsStringAsync();
            var error = JsonConvert.DeserializeObject<ResponseError>(body);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
            body.Should().NotBeNull();
            error.Should().Be(expectedResponseError);
        }

        [TestMethod]
        public async Task UpdateShouldFailWhenRouteIdIsEmptyGuid()
        {
            // Arrange
            var name = "Name";
            var description = "Description";
            var image = "Image";

            var apiVersion = "1.0";
            var createdRecordResult = await ControllerHelper.CreateFlow(name, description, image);
            var routeId = Guid.Empty;
            var ifmatch = createdRecordResult.ETag;

            var jsonObj = new { name = "UpdatedName" };
            string jsonInString = JsonConvert.SerializeObject(jsonObj);

            var expectedError = new ResponseError
            {
                Error = new ResponseErrorBody
                {
                    Code = ServiceError.ResourceNotFound.Code,
                    Message = ServiceError.ResourceNotFound.Message,
                    Details = new List<ResponseErrorField>
                    {
                        new ResponseErrorField
                        {
                            Code = Consts.Failure.Detail.Code.NotFound,
                            Message = CustomFailures.FlowNotFound,
                            Target = Consts.Failure.Detail.Target.Id
                        }
                    }
                }
            };

            // Act
            _client.DefaultRequestHeaders.Add("If-Match", ifmatch);
            var response = await _client.PatchAsync(string.Format("api/flows/{0}?api-version={1}", routeId, apiVersion), new StringContent(jsonInString, Encoding.UTF8, "application/json"));
            var body = await response.Content.ReadAsStringAsync();
            var error = JsonConvert.DeserializeObject<ResponseError>(body);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
            body.Should().NotBeNull();
            error.Should().Be(expectedError);
        }

        [TestMethod]
        public async Task UpdateShouldFailWhenRouteIdDoesNotExist()
        {
            // Arrange
            var name = "Name";
            var description = "Description";
            var image = "Image";

            var apiVersion = "1.0";
            var createdRecordResult = await ControllerHelper.CreateFlow(name, description, image);
            var routeId = Guid.NewGuid();
            var ifmatch = createdRecordResult.ETag;

            var jsonObj = new { name = "UpdatedName" };
            string jsonInString = JsonConvert.SerializeObject(jsonObj);

            var expectedResponseError = new ResponseError
            {
                Error = new ResponseErrorBody
                {
                    Code = ServiceError.ResourceNotFound.Code,
                    Message = ServiceError.ResourceNotFound.Message,
                    Details = new List<ResponseErrorField>
                    {
                        new ResponseErrorField
                        {
                            Code = HandlerFaultCode.NotFound.Name,
                            Message = string.Format(HandlerFailures.NotFound, "Flow"),
                            Target = Consts.Failure.Detail.Target.Id
                        }
                    }
                }
            };

            // Act
            _client.DefaultRequestHeaders.Add("If-Match", ifmatch);
            var response = await _client.PatchAsync(string.Format("api/flows/{0}?api-version={1}", routeId, apiVersion), new StringContent(jsonInString, Encoding.UTF8, "application/json"));
            var body = await response.Content.ReadAsStringAsync();
            var error = JsonConvert.DeserializeObject<ResponseError>(body);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
            body.Should().NotBeNull();
            error.Should().Be(expectedResponseError);
        }

        [TestMethod]
        public async Task UpdateShouldFailWhenQueryApiVersionIsMissing()
        {
            // Arrange
            var name = "Name";
            var description = "Description";
            var image = "Image";

            var createdRecordResult = await ControllerHelper.CreateFlow(name, description, image);
            var routeId = createdRecordResult.Id;
            var ifmatch = createdRecordResult.ETag;

            var jsonObj = new { name = "UpdatedName" };
            string jsonInString = JsonConvert.SerializeObject(jsonObj);

            var expectedError = new ResponseError
            {
                Error = new ResponseErrorBody
                {
                    Code = ServiceError.MissingRequiredQueryParameter.Code,
                    Message = ServiceError.MissingRequiredQueryParameter.Message,
                    Details = new List<ResponseErrorField>
                    {
                        new ResponseErrorField
                        {
                            Code = Consts.Failure.Detail.Code.Missing,
                            Message = string.Format(RequestFailures.QueryParameterRequired, "api-version"),
                            Target = Consts.Failure.Detail.Target.ApiVersion
                        }
                    }
                }
            };

            // Act
            _client.DefaultRequestHeaders.Add("If-Match", ifmatch);
            var response = await _client.PatchAsync(string.Format("api/flows/{0}", routeId), new StringContent(jsonInString, Encoding.UTF8, "application/json"));
            var body = await response.Content.ReadAsStringAsync();
            var error = JsonConvert.DeserializeObject<ResponseError>(body);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            body.Should().NotBeNull();
            error.Should().Be(expectedError);
        }

        [TestMethod]
        public async Task UpdateShouldFailWhenQueryApiVersionIsInvalid()
        {
            // Arrange
            var name = "Name";
            var description = "Description";
            var image = "Image";

            var apiVersion = "4.0";
            var createdRecordResult = await ControllerHelper.CreateFlow(name, description, image);
            var routeId = createdRecordResult.Id;
            var ifmatch = createdRecordResult.ETag;

            var jsonObj = new { name = "UpdatedName" };
            string jsonInString = JsonConvert.SerializeObject(jsonObj);

            var expectedError = new ResponseError
            {
                Error = new ResponseErrorBody
                {
                    Code = ServiceError.InvalidQueryParameterValue.Code,
                    Message = ServiceError.InvalidQueryParameterValue.Message,
                    Details = new List<ResponseErrorField>
                    {
                        new ResponseErrorField
                        {
                            Code = Consts.Failure.Detail.Code.Invalid,
                            Message = string.Format(RequestFailures.QueryParameterInvalidValue, "api-version"),
                            Target = Consts.Failure.Detail.Target.ApiVersion
                        }
                    }
                }
            };

            // Act
            _client.DefaultRequestHeaders.Add("If-Match", ifmatch);
            var response = await _client.PatchAsync(string.Format("api/flows/{0}?api-version={1}", routeId, apiVersion), new StringContent(jsonInString, Encoding.UTF8, "application/json"));
            var body = await response.Content.ReadAsStringAsync();
            var error = JsonConvert.DeserializeObject<ResponseError>(body);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            body.Should().NotBeNull();
            error.Should().Be(expectedError);
        }

        [TestMethod]
        public async Task UpdateShouldFailWhenNameIsNull()
        {
            // Arrange
            var name = "Name";
            var description = "Description";
            var image = "Image";

            var apiVersion = "1.0";
            var createdRecordResult = await ControllerHelper.CreateFlow(name, description, image);
            var routeId = createdRecordResult.Id;
            var ifmatch = createdRecordResult.ETag;

            var jsonObj = new { name = (string)null };
            string jsonInString = JsonConvert.SerializeObject(jsonObj);

            var expectedError = new ResponseError
            {
                Error = new ResponseErrorBody
                {
                    Code = ServiceError.InvalidInput.Code,
                    Message = ServiceError.InvalidInput.Message,
                    Details = new List<ResponseErrorField>
                    {
                        new ResponseErrorField
                        {
                            Code = "invalid-input",
                            Message = ValidationFailures.FlowNameCannotBeEmpty,
                            Target = "name"
                        }
                    }
                }
            };

            // Act
            _client.DefaultRequestHeaders.Add("If-Match", ifmatch);
            var response = await _client.PatchAsync(string.Format("api/flows/{0}?api-version={1}", routeId, apiVersion), new StringContent(jsonInString, Encoding.UTF8, "application/json"));
            var responseBody = await response.Content.ReadAsStringAsync();
            var error = JsonConvert.DeserializeObject<ResponseError>(responseBody);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            responseBody.Should().NotBeNull();
            error.Should().Be(expectedError);
        }

        [TestMethod]
        public async Task UpdateShouldFailWhenNameIsEmpty()
        {
            // Arrange
            var name = "Name";
            var description = "Description";
            var image = "Image";

            var apiVersion = "1.0";
            var createdRecordResult = await ControllerHelper.CreateFlow(name, description, image);
            var routeId = createdRecordResult.Id;
            var ifmatch = createdRecordResult.ETag;

            var jsonObj = new { name = "" };
            string jsonInString = JsonConvert.SerializeObject(jsonObj);

            var expectedError = new ResponseError
            {
                Error = new ResponseErrorBody
                {
                    Code = ServiceError.InvalidInput.Code,
                    Message = ServiceError.InvalidInput.Message,
                    Details = new List<ResponseErrorField>
                    {
                        new ResponseErrorField
                        {
                            Code = "invalid-input",
                            Message = ValidationFailures.FlowNameCannotBeEmpty,
                            Target = "name"
                        }
                    }
                }
            };


            // Act
            _client.DefaultRequestHeaders.Add("If-Match", ifmatch);
            var response = await _client.PatchAsync(string.Format("api/flows/{0}?api-version={1}", routeId, apiVersion), new StringContent(jsonInString, Encoding.UTF8, "application/json"));
            var responseBody = await response.Content.ReadAsStringAsync();
            var error = JsonConvert.DeserializeObject<ResponseError>(responseBody);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            responseBody.Should().NotBeNull();
            error.Should().Be(expectedError);
        }

        [TestMethod]
        public async Task UpdateShouldFailWhenNameIsWhiteSpace()
        {
            // Arrange
            var name = "Name";
            var description = "Description";
            var image = "Image";

            var apiVersion = "1.0";
            var createdRecordResult = await ControllerHelper.CreateFlow(name, description, image);
            var routeId = createdRecordResult.Id;
            var ifmatch = createdRecordResult.ETag;

            var jsonObj = new { name = "   " };
            string jsonInString = JsonConvert.SerializeObject(jsonObj);

            var expectedError = new ResponseError
            {
                Error = new ResponseErrorBody
                {
                    Code = ServiceError.InvalidInput.Code,
                    Message = ServiceError.InvalidInput.Message,
                    Details = new List<ResponseErrorField>
                    {
                        new ResponseErrorField
                        {
                            Code = "invalid-input",
                            Message = ValidationFailures.FlowNameCannotBeEmpty,
                            Target = "name"
                        }
                    }
                }
            };


            // Act
            _client.DefaultRequestHeaders.Add("If-Match", ifmatch);
            var response = await _client.PatchAsync(string.Format("api/flows/{0}?api-version={1}", routeId, apiVersion), new StringContent(jsonInString, Encoding.UTF8, "application/json"));
            var responseBody = await response.Content.ReadAsStringAsync();
            var error = JsonConvert.DeserializeObject<ResponseError>(responseBody);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            responseBody.Should().NotBeNull();
            error.Should().Be(expectedError);
        }

        [DataTestMethod]
        [DataRow(" Flow")]
        [DataRow("Flow ")]
        [DataRow(" Flow ")]
        [DataRow("  Flow  ")]
        public async Task UpdateShouldFailWhenNameStartsOrEndsWithWhiteSpace(string symbols)
        {
            // Arrange
            var name = "Name";
            var description = "Description";
            var image = "Image";

            var apiVersion = "1.0";
            var createdRecordResult = await ControllerHelper.CreateFlow(name, description, image);
            var routeId = createdRecordResult.Id;
            var ifmatch = createdRecordResult.ETag;

            var jsonObj = new { name = symbols };
            string jsonInString = JsonConvert.SerializeObject(jsonObj);

            var expectedError = new ResponseError
            {
                Error = new ResponseErrorBody
                {
                    Code = ServiceError.InvalidInput.Code,
                    Message = ServiceError.InvalidInput.Message,
                    Details = new List<ResponseErrorField>
                    {
                        new ResponseErrorField
                        {
                            Code = "invalid-input",
                            Message = ValidationFailures.FlowNameCannotStartOrEndWithWhiteSpace,
                            Target = "name"
                        }
                    }
                }
            };

            // Act
            _client.DefaultRequestHeaders.Add("If-Match", ifmatch);
            var response = await _client.PatchAsync(string.Format("api/flows/{0}?api-version={1}", routeId, apiVersion), new StringContent(jsonInString, Encoding.UTF8, "application/json"));
            var responseBody = await response.Content.ReadAsStringAsync();
            var error = JsonConvert.DeserializeObject<ResponseError>(responseBody);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            responseBody.Should().NotBeNull();
            error.Should().Be(expectedError);
        }

        [TestMethod]
        public async Task UpdateShouldFailWhenNameAlreadyExists()
        {
            // Arrange
            var nameFirst = "NameFirst";
            var nameSecond = "NameSecond";
            var description = "Description";
            var image = "Image";

            var apiVersion = "1.0";
            await ControllerHelper.CreateFlow(nameFirst, description, image);
            var createdRecordResult = await ControllerHelper.CreateFlow(nameSecond, description, image);
            var routeId = createdRecordResult.Id;
            var ifmatch = createdRecordResult.ETag;
            var jsonInString = @"{
                                    ""name"" : ""NameFirst""
                                 }";

            var expectedError = new ResponseError
            {
                Error = new ResponseErrorBody
                {
                    Code = ServiceError.ResourceAlreadyExists.Code,
                    Message = ServiceError.ResourceAlreadyExists.Message,
                    Details = new List<ResponseErrorField>
                    {
                        new ResponseErrorField
                        {
                            Code = "conflict",
                            Message = HandlerFailures.Conflict,
                            Target = "name"
                        }
                    }
                }
            };


            // Act
            _client.DefaultRequestHeaders.Add("If-Match", ifmatch);
            var response = await _client.PatchAsync(string.Format("api/flows/{0}?api-version={1}", routeId, apiVersion), new StringContent(jsonInString, Encoding.UTF8, "application/json"));
            var responseBody = await response.Content.ReadAsStringAsync();
            var error = JsonConvert.DeserializeObject<ResponseError>(responseBody);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Conflict);
            responseBody.Should().NotBeNull();
            error.Should().Be(expectedError);
        }

        [TestMethod]
        public async Task UpdateShouldFailWhenHeaderIfMatchIsMissing()
        {
            // Arrange
            var name = "Name";
            var description = "Description";
            var image = "Image";

            var apiVersion = "1.0";
            var createdRecordResult = await ControllerHelper.CreateFlow(name, description, image);
            var routeId = createdRecordResult.Id;
            var jsonInString = @"{
                                    ""name"" : ""UpdatedName""
                                 }";


            var expectedResponseError = new ResponseError
            {
                Error = new ResponseErrorBody
                {
                    Code = ServiceError.MissingRequiredHeader.Code,
                    Message = ServiceError.MissingRequiredHeader.Message,
                    Details = new List<ResponseErrorField>
                    {
                        new ResponseErrorField
                        {
                            Code = Consts.Failure.Detail.Code.Missing,
                            Message = string.Format(RequestFailures.HeaderRequired, "If-Match"),
                            Target = Consts.Failure.Detail.Target.IfMatch
                        }
                    }
                }
            };

            // Act
            var response = await _client.PatchAsync(string.Format("api/flows/{0}?api-version={1}", routeId, apiVersion), new StringContent(jsonInString, Encoding.UTF8, "application/json"));
            var body = await response.Content.ReadAsStringAsync();
            var error = JsonConvert.DeserializeObject<ResponseError>(body);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            body.Should().NotBeNull();
            error.Should().Be(expectedResponseError);
        }

        [TestMethod]
        public async Task UpdateShouldFailWhenHeaderIfMatchIsWrong()
        {
            // Arrange
            var name = "Name";
            var description = "Description";
            var image = "Image";

            var apiVersion = "1.0";
            var createdRecordResult = await ControllerHelper.CreateFlow(name, description, image);
            var routeId = createdRecordResult.Id;
            var ifmatch = "\"8001\"";

            var jsonInString = @"{
                                    ""name"" : ""UpdatedName""
                                 }";

            var expectedResponseError = new ResponseError
            {
                Error = new ResponseErrorBody
                {
                    Code = ServiceError.ConditionNotMet.Code,
                    Message = ServiceError.ConditionNotMet.Message,
                    Details = new List<ResponseErrorField>
                    {
                        new ResponseErrorField
                        {
                            Code = HandlerFaultCode.NotMet.Name,
                            Message = HandlerFailures.NotMet,
                            Target = Consts.Failure.Detail.Target.IfMatch
                        }
                    }
                }
            };

            // Act
            _client.DefaultRequestHeaders.Add("If-Match", ifmatch);
            var response = await _client.PatchAsync(string.Format("api/flows/{0}?api-version={1}", routeId, apiVersion), new StringContent(jsonInString, Encoding.UTF8, "application/json"));
            var body = await response.Content.ReadAsStringAsync();
            var error = JsonConvert.DeserializeObject<ResponseError>(body);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.PreconditionFailed);
            body.Should().NotBeNull();
            error.Should().Be(expectedResponseError);
        }

        #endregion

        #region Delete

        [TestMethod]
        public async Task DeleteShouldSucceed()
        {
            // Arrange
            var name = "Name";
            var description = "Description";
            var image = "Image";

            var apiVersion = "1.0";
            var createdRecordResult = await ControllerHelper.CreateFlow(name, description, image);
            var routeId = createdRecordResult.Id;
            var ifmatch = createdRecordResult.ETag;

            // Act
            _client.DefaultRequestHeaders.Add("If-Match", ifmatch);
            var response = await _client.DeleteAsync(string.Format("api/flows/{0}?api-version={1}", routeId, apiVersion));
            var body = await response.Content.ReadAsStringAsync();

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
            body.Should().BeNullOrEmpty();
        }

        [TestMethod]
        public async Task DeleteShouldFailWhenRouteIdIsInvalid()
        {
            // Arrange
            var name = "Name";
            var description = "Description";
            var image = "Image";

            var apiVersion = "1.0";
            var createdRecordResult = await ControllerHelper.CreateFlow(name, description, image);
            var routeId = "someInvalidRouteId";
            var ifmatch = createdRecordResult.ETag;

            var expectedResponseError = new ResponseError
            {
                Error = new ResponseErrorBody
                {
                    Code = ServiceError.ResourceNotFound.Code,
                    Message = ServiceError.ResourceNotFound.Message,
                    Details = new List<ResponseErrorField>
                    {
                        new ResponseErrorField
                        {
                            Code = Consts.Failure.Detail.Code.NotFound,
                            Message = RequestFailures.EntityNotFoundByIdentifier,
                            Target = Consts.Failure.Detail.Target.Id
                        }
                    }
                }
            };

            // Act
            _client.DefaultRequestHeaders.Add("If-Match", ifmatch);
            var response = await _client.DeleteAsync(string.Format("api/flows/{0}?api-version={1}", routeId, apiVersion));
            var body = await response.Content.ReadAsStringAsync();
            var error = JsonConvert.DeserializeObject<ResponseError>(body);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
            body.Should().NotBeNull();
            error.Should().Be(expectedResponseError);
        }

        [TestMethod]
        public async Task DeleteShouldFailWhenRouteIdIsEmptyGuid()
        {
            // Arrange
            var name = "Name";
            var description = "Description";
            var image = "Image";

            var apiVersion = "1.0";
            var createdRecordResult = await ControllerHelper.CreateFlow(name, description, image);
            var routeId = Guid.Empty;
            var ifmatch = createdRecordResult.ETag;


            var expectedError = new ResponseError
            {
                Error = new ResponseErrorBody
                {
                    Code = ServiceError.ResourceNotFound.Code,
                    Message = ServiceError.ResourceNotFound.Message,
                    Details = new List<ResponseErrorField>
                    {
                        new ResponseErrorField
                        {
                            Code = Consts.Failure.Detail.Code.NotFound,
                            Message = CustomFailures.FlowNotFound,
                            Target = Consts.Failure.Detail.Target.Id
                        }
                    }
                }
            };

            // Act
            _client.DefaultRequestHeaders.Add("If-Match", ifmatch);
            var response = await _client.DeleteAsync(string.Format("api/flows/{0}?api-version={1}", routeId, apiVersion));
            var body = await response.Content.ReadAsStringAsync();
            var error = JsonConvert.DeserializeObject<ResponseError>(body);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
            body.Should().NotBeNull();
            error.Should().Be(expectedError);
        }

        [TestMethod]
        public async Task DeleteShouldFailWhenRouteIdDoesNotExist()
        {
            // Arrange
            var name = "Name";
            var description = "Description";
            var image = "Image";

            var apiVersion = "1.0";
            var createdRecordResult = await ControllerHelper.CreateFlow(name, description, image);
            var routeId = Guid.NewGuid();
            var ifmatch = createdRecordResult.ETag;

            var expectedResponseError = new ResponseError
            {
                Error = new ResponseErrorBody
                {
                    Code = ServiceError.ResourceNotFound.Code,
                    Message = ServiceError.ResourceNotFound.Message,
                    Details = new List<ResponseErrorField>
                    {
                        new ResponseErrorField
                        {
                            Code = HandlerFaultCode.NotFound.Name,
                            Message = string.Format(HandlerFailures.NotFound, "Flow"),
                            Target = Consts.Failure.Detail.Target.Id
                        }
                    }
                }
            };

            // Act
            _client.DefaultRequestHeaders.Add("If-Match", ifmatch);
            var response = await _client.DeleteAsync(string.Format("api/flows/{0}?api-version={1}", routeId, apiVersion));
            var body = await response.Content.ReadAsStringAsync();
            var error = JsonConvert.DeserializeObject<ResponseError>(body);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
            body.Should().NotBeNull();
            error.Should().Be(expectedResponseError);
        }

        [TestMethod]
        public async Task DeleteShouldFailWhenQueryApiVersionIsMissing()
        {
            // Arrange
            var routeId = Guid.NewGuid().ToString();

            var expectedError = new ResponseError
            {
                Error = new ResponseErrorBody
                {
                    Code = ServiceError.MissingRequiredQueryParameter.Code,
                    Message = ServiceError.MissingRequiredQueryParameter.Message,
                    Details = new List<ResponseErrorField>
                    {
                        new ResponseErrorField
                        {
                            Code = Consts.Failure.Detail.Code.Missing,
                            Message = string.Format(RequestFailures.QueryParameterRequired, "api-version"),
                            Target = Consts.Failure.Detail.Target.ApiVersion
                        }
                    }
                }
            };

            // Act
            var response = await _client.DeleteAsync(string.Format("api/flows/{0}", routeId));
            var body = await response.Content.ReadAsStringAsync();
            var error = JsonConvert.DeserializeObject<ResponseError>(body);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            body.Should().NotBeNull();
            error.Should().Be(expectedError);
        }

        [TestMethod]
        public async Task DeleteShouldFailWhenQueryApiVersionIsInvalid()
        {
            // Arrange
            var apiVersion = "4.0";
            var routeId = Guid.NewGuid().ToString();

            var expectedError = new ResponseError
            {
                Error = new ResponseErrorBody
                {
                    Code = ServiceError.InvalidQueryParameterValue.Code,
                    Message = ServiceError.InvalidQueryParameterValue.Message,
                    Details = new List<ResponseErrorField>
                    {
                        new ResponseErrorField
                        {
                            Code = Consts.Failure.Detail.Code.Invalid,
                            Message = string.Format(RequestFailures.QueryParameterInvalidValue, "api-version"),
                            Target = Consts.Failure.Detail.Target.ApiVersion
                        }
                    }
                }
            };

            // Act
            var response = await _client.DeleteAsync(string.Format("api/flows/{0}?api-version={1}", routeId, apiVersion));
            var body = await response.Content.ReadAsStringAsync();
            var error = JsonConvert.DeserializeObject<ResponseError>(body);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            body.Should().NotBeNull();
            error.Should().Be(expectedError);
        }

        [TestMethod]
        public async Task DeleteShouldFailWhenHeaderIfMatchIsMissing()
        {
            // Arrange
            var routeId = Guid.NewGuid().ToString();
            var apiVersion = "1.0";


            var expectedResponseError = new ResponseError
            {
                Error = new ResponseErrorBody
                {
                    Code = ServiceError.MissingRequiredHeader.Code,
                    Message = ServiceError.MissingRequiredHeader.Message,
                    Details = new List<ResponseErrorField>
                    {
                        new ResponseErrorField
                        {
                            Code = Consts.Failure.Detail.Code.Missing,
                            Message = string.Format(RequestFailures.HeaderRequired,"If-Match"),
                            Target = Consts.Failure.Detail.Target.IfMatch
                        }
                    }
                }
            };

            // Act
            var response = await _client.DeleteAsync(string.Format("api/flows/{0}?api-version={1}", routeId, apiVersion));
            var body = await response.Content.ReadAsStringAsync();
            var error = JsonConvert.DeserializeObject<ResponseError>(body);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            body.Should().NotBeNull();
            error.Should().Be(expectedResponseError);
        }

        [TestMethod]
        public async Task DeleteShouldFailWhenHeaderIfMatchIsInvalid()
        {
            // Arrange
            var routeId = Guid.NewGuid().ToString();
            var apiVersion = "1.0";
            var ifmatch = "\" \"";

            var expectedResponseError = new ResponseError
            {
                Error = new ResponseErrorBody
                {
                    Code = ServiceError.InvalidHeaderValue.Code,
                    Message = ServiceError.InvalidHeaderValue.Message,
                    Details = new List<ResponseErrorField>
                    {
                        new ResponseErrorField
                        {
                            Code = Consts.Failure.Detail.Code.Invalid,
                            Message = string.Format(RequestFailures.HeaderInvalidValue, "If-Match"),
                            Target = Consts.Failure.Detail.Target.IfMatch
                        }
                    }
                }
            };

            // Act
            _client.DefaultRequestHeaders.Add("If-Match", ifmatch);
            var response = await _client.DeleteAsync(string.Format("api/flows/{0}?api-version={1}", routeId, apiVersion));
            var body = await response.Content.ReadAsStringAsync();
            var error = JsonConvert.DeserializeObject<ResponseError>(body);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            body.Should().NotBeNull();
            error.Should().Be(expectedResponseError);
        }

        [TestMethod]
        public async Task DeleteShouldFailWhenHeaderIfMatchIsWrong()
        {
            // Arrange
            var name = "Name";
            var description = "Description";
            var image = "Image";

            var apiVersion = "1.0";
            var createdRecordResult = await ControllerHelper.CreateFlow(name, description, image);
            var routeId = createdRecordResult.Id;
            var ifmatch = "\"8001\"";

            var expectedResponseError = new ResponseError
            {
                Error = new ResponseErrorBody
                {
                    Code = ServiceError.ConditionNotMet.Code,
                    Message = ServiceError.ConditionNotMet.Message,
                    Details = new List<ResponseErrorField>
                    {
                        new ResponseErrorField
                        {
                            Code = HandlerFaultCode.NotMet.Name,
                            Message = HandlerFailures.NotMet,
                            Target = Consts.Failure.Detail.Target.IfMatch
                        }
                    }
                }
            };

            // Act
            _client.DefaultRequestHeaders.Add("If-Match", ifmatch);
            var response = await _client.DeleteAsync(string.Format("api/flows/{0}?api-version={1}", routeId, apiVersion));
            var body = await response.Content.ReadAsStringAsync();
            var error = JsonConvert.DeserializeObject<ResponseError>(body);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.PreconditionFailed);
            body.Should().NotBeNull();
            error.Should().Be(expectedResponseError);
        }

        #endregion

    }
}
