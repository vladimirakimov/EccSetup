using FluentAssertions;
using ITG.Brix.EccSetup.API.Context.Bases;
using ITG.Brix.EccSetup.API.Context.Constants;
using ITG.Brix.EccSetup.API.Context.Resources;
using ITG.Brix.EccSetup.API.Context.Services.Responses.Models.Errors;
using ITG.Brix.EccSetup.Application.Cqs.Queries.Models;
using ITG.Brix.EccSetup.Application.Resources;
using ITG.Brix.EccSetup.IntegrationTests.Bases;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ITG.Brix.EccSetup.IntegrationTests.Controllers
{
    [TestClass]
    [TestCategory("Integration")]
    public class OperatorActivitiesControllerTests
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
            DatabaseHelper.Init("OperatorActivities");
            _client = ControllerTestsHelper.GetClient();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            _client.Dispose();
        }

        [TestMethod]
        public async Task CreateShouldSucceed()
        {
            //Arrange
            var apiVersion = "1.0";
            var json = "{\r\n\t\"Activities\": [\r\n\t\t{\r\n\t\t\"Type\": \"Validation\",\r\n\t\t\"Step\": \"Instruction\",\r\n\t\t\"Name\":\"Test\",\r\n\t\t\"Created\": \"6\\/19\\/2015 10:35:50 AM\",\r\n\t\t\"OperatorName\": \"eOrder\",\r\n\t\t\"OperatorId\": \"a9e5613c-95e8-41a0-a046-ca39b887adbe\",\r\n\t\t\"WorkOrderId\": \"f80d2d56-d130-40b3-bad4-2855960209d6\"\r\n\t\t}\r\n\t]\r\n}";

            var httpContent = new StringContent(json.ToString());
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            //Act
            var response = await _client.PostAsync($"api/operatorActivities?api-version={apiVersion}", httpContent);
            var responseAsString = await response.Content.ReadAsStringAsync();

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [TestMethod]
        public async Task CreateShouldFailWhenBodyIsNonJsonContentType()
        {
            //Arrange
            var apiVersion = "1.0";
            var json = "{\r\n\t\"Activities\": [\r\n\t\t{\r\n\t\t\"Type\": \"Validation\",\r\n\t\t\"Step\": \"Instruction\",\r\n\t\t\"Name\":\"Test\",\r\n\t\t\"Created\": \"6\\/19\\/2015 10:35:50 AM\",\r\n\t\t\"OperatorName\": \"eOrder\",\r\n\t\t\"OperatorId\": \"a9e5613c-95e8-41a0-a046-ca39b887adbe\",\r\n\t\t\"WorkOrderId\": \"f80d2d56-d130-40b3-bad4-2855960209d6\"\r\n\t\t}\r\n\t]\r\n}";

            var httpContent = new StringContent(json.ToString());
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("text/plain");

            //Act
            var response = await _client.PostAsync($"api/operatorActivities?api-version={apiVersion}", httpContent);
            var responseAsString = await response.Content.ReadAsStringAsync();

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.UnsupportedMediaType);
            responseAsString.Should().BeNullOrEmpty();
        }

        [TestMethod]
        public async Task CreateShouldFailWhenQueryApiVersionIsMissing()
        {
            //Arrange
            var apiVersion = "1.0";
            var json = "{\r\n\t\"Activities\": [\r\n\t\t{\r\n\t\t\"Type\": \"Validation\",\r\n\t\t\"Step\": \"Instruction\",\r\n\t\t\"Name\":\"Test\",\r\n\t\t\"Created\": \"6\\/19\\/2015 10:35:50 AM\",\r\n\t\t\"OperatorName\": \"eOrder\",\r\n\t\t\"OperatorId\": \"a9e5613c-95e8-41a0-a046-ca39b887adbe\",\r\n\t\t\"WorkOrderId\": \"f80d2d56-d130-40b3-bad4-2855960209d6\"\r\n\t\t}\r\n\t]\r\n}";

            var httpContent = new StringContent(json.ToString());
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

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

            //Act
            var response = await _client.PostAsync($"api/operatorActivities", httpContent);
            var responseAsString = await response.Content.ReadAsStringAsync();
            var error = JsonConvert.DeserializeObject<ResponseError>(responseAsString);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            responseAsString.Should().NotBeNull();
            error.Should().Be(expectedError);
        }

        [TestMethod]
        public async Task CreateShouldFailWhenApiVersionIsInvalid()
        {
            //Arrange
            var apiVersion = "2.0";
            var json = "{\r\n\t\"Activities\": [\r\n\t\t{\r\n\t\t\"Type\": \"Validation\",\r\n\t\t\"Step\": \"Instruction\",\r\n\t\t\"Name\":\"Test\",\r\n\t\t\"Created\": \"6\\/19\\/2015 10:35:50 AM\",\r\n\t\t\"OperatorName\": \"eOrder\",\r\n\t\t\"OperatorId\": \"a9e5613c-95e8-41a0-a046-ca39b887adbe\",\r\n\t\t\"WorkOrderId\": \"f80d2d56-d130-40b3-bad4-2855960209d6\"\r\n\t\t}\r\n\t]\r\n}";

            var httpContent = new StringContent(json.ToString());
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

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

            //Act
            var response = await _client.PostAsync($"api/operatorActivities?api-version={apiVersion}", httpContent);
            var responseAsString = await response.Content.ReadAsStringAsync();
            var error = JsonConvert.DeserializeObject<ResponseError>(responseAsString);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            responseAsString.Should().NotBeNull();
            error.Should().Be(expectedError);
        }

        [TestMethod]
        public async Task ListAllShouldSucceed()
        {
            //Arrange
            var apiVersion = "1.0";
            await ControllerHelper.CreateOperatorActivity();
            await ControllerHelper.CreateOperatorActivity();
            await ControllerHelper.CreateOperatorActivity();

            //Act
            var response = await _client.GetAsync($"api/operatorActivities?api-version={apiVersion}");
            var responseAsString = await response.Content.ReadAsStringAsync();
            var operatorActivitiesModel = JsonConvert.DeserializeObject<OperatorActivitiesModel>(responseAsString);
            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            operatorActivitiesModel.Value.Should().HaveCount(3);
        }

        [TestMethod]
        public async Task ListOperatorActivitiesFilterByOperatorIdShouldSucceed()
        {
            //Arrange
            var apiVersion = "1.0";
            var filter = "operatorId eq 'a9e5613c-95e8-41a0-a046-ca39b887adbe'";
            await ControllerHelper.CreateOperatorActivity();
            await ControllerHelper.CreateOperatorActivity();

            //Act 
            var response = await _client.GetAsync($"api/operatorActivities?api-version={apiVersion}&$filter={filter}");
            var responseAsString = await response.Content.ReadAsStringAsync();
            var operatorActivitiesModel = JsonConvert.DeserializeObject<OperatorActivitiesModel>(responseAsString);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            operatorActivitiesModel.Value.Should().HaveCount(2);
        }

        [TestMethod]
        public async Task ListOperatorActivitiesFilterByOrderIdShouldSucceed()
        {
            //Arrange
            var apiVersion = "1.0";
            var filter = "workOrderId eq 'f80d2d56-d130-40b3-bad4-2855960209d6'";
            await ControllerHelper.CreateOperatorActivity();
            await ControllerHelper.CreateOperatorActivity();

            //Act 
            var response = await _client.GetAsync($"api/operatorActivities?api-version={apiVersion}&$filter={filter}");
            var responseAsString = await response.Content.ReadAsStringAsync();
            var operatorActivitiesModel = JsonConvert.DeserializeObject<OperatorActivitiesModel>(responseAsString);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            operatorActivitiesModel.Value.Should().HaveCount(2);
        }

        [TestMethod]
        public async Task ListOperatorActivitiesShouldFailIfFilterIsNotSupported()
        {
            //Arrange
            var apiVersion = "1.0";
            var filter = "workOrderId eq 'f80d2d56-d130-40b3-bad4-2855960209d6' and";

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
            var response = await _client.GetAsync($"api/operatorActivities?api-version={apiVersion}&$filter={filter}");
            var responseAsString = await response.Content.ReadAsStringAsync();
            var error = JsonConvert.DeserializeObject<ResponseError>(responseAsString);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            error.Should().Be(expectedError);
        }


        [TestMethod]
        public async Task ListOperatorActivitiesWithSkipandTopShouldSucceed()
        {
            //Arrange
            var apiVersion = "1.0";
            var skip = 1;
            var top = 3;
            await ControllerHelper.CreateOperatorActivity();
            await ControllerHelper.CreateOperatorActivity();
            await ControllerHelper.CreateOperatorActivity();
            await ControllerHelper.CreateOperatorActivity();
            await ControllerHelper.CreateOperatorActivity();

            //Act
            var response = await _client.GetAsync($"api/operatorActivities?api-version={apiVersion}&$skip={skip}&$top={top}");
            var responseAsString = await response.Content.ReadAsStringAsync();
            var operatorActivitiesModel = JsonConvert.DeserializeObject<OperatorActivitiesModel>(responseAsString);
            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            operatorActivitiesModel.Value.Should().HaveCount(3);
        }

        [TestMethod]
        public async Task ListOperatorWithFilterSkipTopShouldSucceed()
        {
            //Arrange
            var apiVersion = "1.0";
            var skip = 1;
            var top = 3;
            var filter = "operatorId eq 'a9e5613c-95e8-41a0-a046-ca39b887adbe'";
            await ControllerHelper.CreateOperatorActivity();
            await ControllerHelper.CreateOperatorActivity();
            await ControllerHelper.CreateOperatorActivity();
            await ControllerHelper.CreateOperatorActivity();
            await ControllerHelper.CreateOperatorActivity();

            //Act
            var response = await _client.GetAsync($"api/operatorActivities?api-version={apiVersion}&$skip={skip}&$top={top}&$filter={filter}");
            var responseAsString = await response.Content.ReadAsStringAsync();
            var operatorActivitiesModel = JsonConvert.DeserializeObject<OperatorActivitiesModel>(responseAsString);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            operatorActivitiesModel.Value.Should().HaveCount(3);
        }

        [TestMethod]
        public async Task ListOperatoractiviesShouldSucceedWhenQueryTopIsNotSet()
        {
            //Arrange
            var apiVersion = "1.0";
            var skip = 1;
            await ControllerHelper.CreateOperatorActivity();

            //Act
            var response = await _client.GetAsync($"api/operatorActivities?api-version={apiVersion}&$skip={skip}&$top=");
            var responseAsString = await response.Content.ReadAsStringAsync();
            var operatorActivitiesModel = JsonConvert.DeserializeObject<OperatorActivitiesModel>(responseAsString);
            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [TestMethod]
        public async Task ListOperatorActivitiesShouldSucceedWhenQuerySkipIsNotSet()
        {
            //Arrange
            var apiVersion = "1.0";
            var top = 3;
            await ControllerHelper.CreateOperatorActivity();

            //Act
            var response = await _client.GetAsync($"api/operatorActivities?api-version={apiVersion}&$skip=&$top={top}");
            var responseAsString = await response.Content.ReadAsStringAsync();
            var operatorActivitiesModel = JsonConvert.DeserializeObject<OperatorActivitiesModel>(responseAsString);
            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [TestMethod]
        public async Task ListOperatorActivitiesShouldFailWhenQueryTopIsNotInRange()
        {
            //Arrange
            var apiVersion = "1.0";
            var top = 99999999999;
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

            //Act
            var response = await _client.GetAsync($"api/operatorActivities?api-version={apiVersion}&$top={top}");
            var responseAsString = await response.Content.ReadAsStringAsync();
            var error = JsonConvert.DeserializeObject<ResponseError>(responseAsString);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            error.Should().Be(expectedError);
        }

        [TestMethod]
        public async Task ListOperatorActivityShouldFailWhenQueryTopIsNotValid()
        {
            // Arrange
            var apiVersion = "1.0";
            var top = "top";

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

            //Act
            var response = await _client.GetAsync($"api/operatorActivities?api-version={apiVersion}&$top={top}");
            var responseAsString = await response.Content.ReadAsStringAsync();
            var error = JsonConvert.DeserializeObject<ResponseError>(responseAsString);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            error.Should().Be(expectedError);
        }

        [TestMethod]
        public async Task ListOperatorActivitiesShouldFailWhenQuerySkipIsNotInRange()
        {
            //Arrange
            var apiVersion = "1.0";
            var skip = 99999999999;
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

            //Act
            var response = await _client.GetAsync($"api/operatorActivities?api-version={apiVersion}&$top={skip}");
            var responseAsString = await response.Content.ReadAsStringAsync();
            var error = JsonConvert.DeserializeObject<ResponseError>(responseAsString);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            error.Should().Be(expectedError);
        }

        [TestMethod]
        public async Task ListOperatorActivitiesShouldFailWhenApiVersionIsMIssing()
        {
            //Arrange
            var apiVersion = "1.0";
            var filter = "workOrderId eq 'f80d2d56-d130-40b3-bad4-2855960209d6' and";

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

            //Act
            var response = await _client.GetAsync($"api/operatorActivities");
            var responseAsString = await response.Content.ReadAsStringAsync();
            var error = JsonConvert.DeserializeObject<ResponseError>(responseAsString);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            error.Should().Be(expectedError);
        }

        [TestMethod]
        public async Task ListOperatorActivitiesShouldfailWhenApiVersionIsInvalid()
        {
            //Arrange
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

            //Act
            var response = await _client.GetAsync($"api/operatorActivities?api-version={apiVersion}");
            var responseAsString = await response.Content.ReadAsStringAsync();
            var error = JsonConvert.DeserializeObject<ResponseError>(responseAsString);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            error.Should().Be(expectedError);
        }
    }
}
