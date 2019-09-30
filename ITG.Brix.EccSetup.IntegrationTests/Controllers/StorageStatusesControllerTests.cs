using FluentAssertions;
using ITG.Brix.EccSetup.API.Context.Bases;
using ITG.Brix.EccSetup.API.Context.Constants;
using ITG.Brix.EccSetup.API.Context.Resources;
using ITG.Brix.EccSetup.API.Context.Services.Requests.Models.From;
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
using System.Text;
using System.Threading.Tasks;

namespace ITG.Brix.EccSetup.IntegrationTests.Controllers
{
    [TestClass]
    [TestCategory("Integration")]
    public class StorageStatusesControllerTests
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
            DatabaseHelper.Init("StorageStatuses");
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
            await ControllerHelper.CreateStorageStatus("TestCode", "TestName", "true", "TestSource");

            // Act
            var response = await _client.GetAsync(string.Format("api/storagestatuses?api-version={0}", apiVersion));
            var responseBody = await response.Content.ReadAsStringAsync();
            var storageStatusesModel = JsonConvert.DeserializeObject<StorageStatusesModel>(responseBody);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            storageStatusesModel.Value.Should().HaveCount(1);
            storageStatusesModel.Count.Should().Be(1);
            storageStatusesModel.NextLink.Should().BeNull();
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
            var response = await _client.GetAsync("api/storagestatuses");
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
            var response = await _client.GetAsync(string.Format("api/storagestatuses?api-version={0}", apiVersion));
            var body = await response.Content.ReadAsStringAsync();
            var error = JsonConvert.DeserializeObject<ResponseError>(body);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            body.Should().NotBeNull();
            error.Should().Be(expectedError);
        }

        [DataTestMethod]
        [DataRow("code eq 'Codea'", 1)]
        [DataRow("code ne 'Codea'", 25)]
        [DataRow("startswith(code, 'Code') eq true", 26)]
        [DataRow("startswith(code, 'Hello') eq false", 26)]
        [DataRow("startswith(code, 'Code') eq true and endswith(code, 'z') eq true", 1)]
        [DataRow("endswith(code, 'z') eq true", 1)]
        [DataRow("endswith(code, 'z') eq false", 25)]
        [DataRow("substringof('odea', code) eq true", 1)]
        [DataRow("tolower(code) eq 'codea'", 1)]
        [DataRow("toupper(code) eq 'CODEA'", 1)]
        public async Task ListWithFilterShouldSucceed(string filter, int countResult)
        {
            // Arrange
            var name = "Name";
            var code = "Code";
            var source = "Source";
            var damagedQuantityRequired = "true";
            var apiVersion = "1.0";

            string alphabet = "abcdefghijklmnopqrstuvwxyz";

            foreach (char c in alphabet)
            {
                await ControllerHelper.CreateStorageStatus(code + c, name, damagedQuantityRequired, source);
            }

            // Act
            var response = await _client.GetAsync(string.Format("api/storagestatuses?api-version={0}&$filter={1}", apiVersion, filter));
            var responseBody = await response.Content.ReadAsStringAsync();
            var storageStatusesModel = JsonConvert.DeserializeObject<StorageStatusesModel>(responseBody);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            storageStatusesModel.Value.Should().HaveCount(countResult);
            storageStatusesModel.Count.Should().Be(countResult);
            storageStatusesModel.NextLink.Should().BeNull();
        }

        [DataTestMethod]
        [DataRow(0, 10, 10)]
        [DataRow(1, 10, 10)]
        [DataRow(10, 100, 16)]
        public async Task ListWithSkipAndTopShouldSucceed(int skip, int top, int countResult)
        {
            // Arrange
            var name = "Name";
            var code = "Code";
            var source = "Source";
            var damagedQuantityRequired = "true";
            var apiVersion = "1.0";

            string alphabet = "abcdefghijklmnopqrstuvwxyz";

            foreach (char c in alphabet)
            {
                await ControllerHelper.CreateStorageStatus(code + c, name, damagedQuantityRequired, source);
            }

            // Act
            var response = await _client.GetAsync(string.Format("api/storagestatuses?api-version={0}&$skip={1}&$top={2}", apiVersion, skip, top));
            var responseBody = await response.Content.ReadAsStringAsync();
            var storageStatusesModel = JsonConvert.DeserializeObject<StorageStatusesModel>(responseBody);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            storageStatusesModel.Value.Should().HaveCount(countResult);
            storageStatusesModel.Count.Should().Be(countResult);
            storageStatusesModel.NextLink.Should().BeNull();
        }

        [DataTestMethod]
        [DataRow("code ne 'Codea'", 0, 10, 10)]
        [DataRow("code ne 'Codea'", 0, 30, 25)]
        [DataRow("code ne 'Codea'", 0, 10, 10)]
        [DataRow("code ne 'Codea'", 20, 10, 5)]
        public async Task ListWithFilterSkipTopShouldSucceed(string filter, int skip, int top, int countResult)
        {
            // Arrange
            var name = "Name";
            var code = "Code";
            var source = "Source";
            var damagedQuantityRequired = "true";
            var apiVersion = "1.0";

            string alphabet = "abcdefghijklmnopqrstuvwxyz";

            foreach (char c in alphabet)
            {
                await ControllerHelper.CreateStorageStatus(code + c, name, damagedQuantityRequired, source);
            }


            // Act
            var response = await _client.GetAsync(string.Format("api/storagestatuses?api-version={0}&$filter={1}&$skip={2}&$top={3}", apiVersion, filter, skip, top));
            var responseBody = await response.Content.ReadAsStringAsync();
            var storageStatusesModel = JsonConvert.DeserializeObject<StorageStatusesModel>(responseBody);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            storageStatusesModel.Value.Should().HaveCount(countResult);
            storageStatusesModel.Count.Should().Be(countResult);
            storageStatusesModel.NextLink.Should().BeNull();
        }

        [DataTestMethod]
        [DataRow("version eq 1")]
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
            var response = await _client.GetAsync(string.Format("api/storagestatuses?api-version={0}&$filter={1}", apiVersion, filter));
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
            var response = await _client.GetAsync(string.Format("api/storagestatuses?api-version={0}&$filter={1}", apiVersion, filter));
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
            var response = await _client.GetAsync(string.Format("api/storagestatuses?api-version={0}&$top={1}", apiVersion, top));

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
            var response = await _client.GetAsync(string.Format("api/storagestatuses?api-version={0}&$top={1}", apiVersion, top));
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
            var response = await _client.GetAsync(string.Format("api/storagestatuses?api-version={0}&$top={1}", apiVersion, top));
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
            var response = await _client.GetAsync(string.Format("api/storagestatuses?api-version={0}&$skip={1}", apiVersion, skip));

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
            var response = await _client.GetAsync(string.Format("api/storagestatuses?api-version={0}&$skip={1}", apiVersion, skip));
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
            var response = await _client.GetAsync(string.Format("api/storagestatuses?api-version={0}&$skip={1}", apiVersion, skip));
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
            var name = "TName";
            var code = "TCode";
            var source = "TSource";
            var @default = "true";
            var apiVersion = "1.0";
            var body = new CreateStorageStatusFromBody()
            {
                Default = @default,
                Source = source,
                Code = code,
                Name = name
            };
            var jsonBody = JsonConvert.SerializeObject(body);

            // Act
            var response = await _client.PostAsync(string.Format("api/storagestatuses?api-version={0}", apiVersion), new StringContent(jsonBody, Encoding.UTF8, "application/json"));
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
            var name = "TName";
            var code = "TCode";
            var source = "TSource";
            var @default = "true";

            var apiVersion = "1.0";
            var body = new CreateStorageStatusFromBody()
            {
                Default = @default,
                Source = source,
                Code = code,
                Name = name
            };
            var jsonBody = JsonConvert.SerializeObject(body);

            // Act
            var response = await _client.PostAsync(string.Format("api/storagestatuses?api-version={0}", apiVersion), new StringContent(jsonBody, Encoding.UTF8, "text/plain"));
            var responseBody = await response.Content.ReadAsStringAsync();

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.UnsupportedMediaType);
            responseBody.Should().BeNullOrEmpty();
        }

        [TestMethod]
        public async Task CreateShouldFailWhenQueryApiVersionIsMissing()
        {
            // Arrange
            var name = "TName";
            var code = "TCode";
            var source = "TSource";
            var @default = "true";

            var apiVersion = "1.0";
            var body = new CreateStorageStatusFromBody()
            {
                Default = @default,
                Source = source,
                Code = code,
                Name = name
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
            var response = await _client.PostAsync("api/storagestatuses", new StringContent(jsonBody, Encoding.UTF8, "application/json"));
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

            var apiVersion = "4.0";
            var body = new CreateStorageStatusFromBody()
            {
                Name = name
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
            var response = await _client.PostAsync(string.Format("api/storagestatuses?api-version={0}", apiVersion), new StringContent(jsonBody, Encoding.UTF8, "application/json"));
            var responseBody = await response.Content.ReadAsStringAsync();
            var error = JsonConvert.DeserializeObject<ResponseError>(responseBody);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            responseBody.Should().NotBeNull();
            error.Should().Be(expectedError);
        }


        [TestMethod]
        public async Task CreateShouldFailWhenCodeIsNull()
        {
            // Arrange
            var name = "TName";
            string code = null;
            var source = "TSource";
            var @default = "true";

            var apiVersion = "1.0";
            var body = new CreateStorageStatusFromBody()
            {
                Default = @default,
                Source = source,
                Code = code,
                Name = name
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
                            Message = ValidationFailures.StorageStatusCodeMandatory,
                            Target = "code"
                        }
                    }
                }
            };

            // Act
            var response = await _client.PostAsync(string.Format("api/storagestatuses?api-version={0}", apiVersion), new StringContent(jsonBody, Encoding.UTF8, "application/json"));
            var responseBody = await response.Content.ReadAsStringAsync();
            var error = JsonConvert.DeserializeObject<ResponseError>(responseBody);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            responseBody.Should().NotBeNull();
            error.Should().Be(expectedError);
        }

        [TestMethod]
        public async Task CreateShouldFailWhenCodeIsEmpty()
        {
            // Arrange
            var name = "TName";
            string code = string.Empty;
            var source = "TSource";
            var @default = "true";
            var apiVersion = "1.0";
            var body = new CreateStorageStatusFromBody()
            {
                Default = @default,
                Source = source,
                Code = code,
                Name = name
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
                            Message = ValidationFailures.StorageStatusCodeMandatory,
                            Target = "code"
                        }
                    }
                }
            };

            // Act
            var response = await _client.PostAsync(string.Format("api/storagestatuses?api-version={0}", apiVersion), new StringContent(jsonBody, Encoding.UTF8, "application/json"));
            var responseBody = await response.Content.ReadAsStringAsync();
            var error = JsonConvert.DeserializeObject<ResponseError>(responseBody);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            responseBody.Should().NotBeNull();
            error.Should().Be(expectedError);
        }

        [TestMethod]
        public async Task CreateShouldFailWhenCodeIsWhiteSpace()
        {
            // Arrange
            var name = "TName";
            string code = "   ";
            var source = "TSource";
            var @default = "true";
            var apiVersion = "1.0";
            var body = new CreateStorageStatusFromBody()
            {
                Default = @default,
                Source = source,
                Code = code,
                Name = name
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
                            Message = ValidationFailures.StorageStatusCodeMandatory,
                            Target = "code"
                        }
                    }
                }
            };

            // Act
            var response = await _client.PostAsync(string.Format("api/storagestatuses?api-version={0}", apiVersion), new StringContent(jsonBody, Encoding.UTF8, "application/json"));
            var responseBody = await response.Content.ReadAsStringAsync();
            var error = JsonConvert.DeserializeObject<ResponseError>(responseBody);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            responseBody.Should().NotBeNull();
            error.Should().Be(expectedError);
        }

        [TestMethod]
        public async Task CreateShouldFailWhenSourceIsNull()
        {
            // Arrange
            var name = "TName";
            var code = "TCode";
            string source = null;
            var @default = "true";

            var apiVersion = "1.0";
            var body = new CreateStorageStatusFromBody()
            {
                Default = @default,
                Source = source,
                Code = code,
                Name = name
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
                            Message = ValidationFailures.StorageStatusSourceMandatory,
                            Target = "source"
                        }
                    }
                }
            };

            // Act
            var response = await _client.PostAsync(string.Format("api/storagestatuses?api-version={0}", apiVersion), new StringContent(jsonBody, Encoding.UTF8, "application/json"));
            var responseBody = await response.Content.ReadAsStringAsync();
            var error = JsonConvert.DeserializeObject<ResponseError>(responseBody);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            responseBody.Should().NotBeNull();
            error.Should().Be(expectedError);
        }

        [TestMethod]
        public async Task CreateShouldFailWhenSourceIsEmpty()
        {
            // Arrange
            var name = "TName";
            var code = "TCode";
            string source = string.Empty;
            var @default = "true";
            var apiVersion = "1.0";
            var body = new CreateStorageStatusFromBody()
            {
                Default = @default,
                Source = source,
                Code = code,
                Name = name
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
                            Message = ValidationFailures.StorageStatusSourceMandatory,
                            Target = "source"
                        }
                    }
                }
            };

            // Act
            var response = await _client.PostAsync(string.Format("api/storagestatuses?api-version={0}", apiVersion), new StringContent(jsonBody, Encoding.UTF8, "application/json"));
            var responseBody = await response.Content.ReadAsStringAsync();
            var error = JsonConvert.DeserializeObject<ResponseError>(responseBody);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            responseBody.Should().NotBeNull();
            error.Should().Be(expectedError);
        }

        [TestMethod]
        public async Task CreateShouldFailWhenSourceIsWhiteSpace()
        {
            // Arrange
            var name = "TName";
            var code = "TCode";
            string source = "    ";
            var @default = "true";
            var apiVersion = "1.0";
            var body = new CreateStorageStatusFromBody()
            {
                Default = @default,
                Source = source,
                Code = code,
                Name = name
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
                            Message = ValidationFailures.StorageStatusSourceMandatory,
                            Target = "source"
                        }
                    }
                }
            };

            // Act
            var response = await _client.PostAsync(string.Format("api/storagestatuses?api-version={0}", apiVersion), new StringContent(jsonBody, Encoding.UTF8, "application/json"));
            var responseBody = await response.Content.ReadAsStringAsync();
            var error = JsonConvert.DeserializeObject<ResponseError>(responseBody);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            responseBody.Should().NotBeNull();
            error.Should().Be(expectedError);
        }

        [TestMethod]
        public async Task CreateShouldFailWhenDefaultIsNull()
        {
            // Arrange
            var name = "TName";
            var code = "TCode";
            string source = "TSource";
            string @default = null;

            var apiVersion = "1.0";
            var body = new CreateStorageStatusFromBody()
            {
                Default = @default,
                Source = source,
                Code = code,
                Name = name
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
                            Message = ValidationFailures.StorageStatusDefaultMandatory,
                            Target = "default"
                        }
                    }
                }
            };

            // Act
            var response = await _client.PostAsync(string.Format("api/storagestatuses?api-version={0}", apiVersion), new StringContent(jsonBody, Encoding.UTF8, "application/json"));
            var responseBody = await response.Content.ReadAsStringAsync();
            var error = JsonConvert.DeserializeObject<ResponseError>(responseBody);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            responseBody.Should().NotBeNull();
            error.Should().Be(expectedError);
        }

        [TestMethod]
        public async Task CreateShouldFailWhenDefaultIsEmpty()
        {
            // Arrange
            var name = "TName";
            var code = "TCode";
            string source = "TSource";
            string @default = string.Empty;

            var apiVersion = "1.0";
            var body = new CreateStorageStatusFromBody()
            {
                Default = @default,
                Source = source,
                Code = code,
                Name = name
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
                            Message = ValidationFailures.StorageStatusDefaultMandatory,
                            Target = "default"
                        }
                    }
                }
            };

            // Act
            var response = await _client.PostAsync(string.Format("api/storagestatuses?api-version={0}", apiVersion), new StringContent(jsonBody, Encoding.UTF8, "application/json"));
            var responseBody = await response.Content.ReadAsStringAsync();
            var error = JsonConvert.DeserializeObject<ResponseError>(responseBody);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            responseBody.Should().NotBeNull();
            error.Should().Be(expectedError);
        }

        [TestMethod]
        public async Task CreateShouldFailWhenDefaultIsWhiteSpace()
        {
            // Arrange
            var name = "TName";
            var code = "TCode";
            string source = "TSource";
            string @default = "  ";

            var apiVersion = "1.0";
            var body = new CreateStorageStatusFromBody()
            {
                Default = @default,
                Source = source,
                Code = code,
                Name = name
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
                            Message = ValidationFailures.StorageStatusDefaultMandatory,
                            Target = "default"
                        }
                    }
                }
            };

            // Act
            var response = await _client.PostAsync(string.Format("api/storagestatuses?api-version={0}", apiVersion), new StringContent(jsonBody, Encoding.UTF8, "application/json"));
            var responseBody = await response.Content.ReadAsStringAsync();
            var error = JsonConvert.DeserializeObject<ResponseError>(responseBody);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            responseBody.Should().NotBeNull();
            error.Should().Be(expectedError);
        }

        [TestMethod]
        public async Task CreateShouldFailWhenCodeAndSourceAlreadyExists()
        {
            // Arrange
            var name = "TName";
            var code = "TCode";
            string source = "TSource";
            var @default = "true";
            var apiVersion = "1.0";
            await ControllerHelper.CreateStorageStatus(code, name, @default, source);

            var body = new CreateStorageStatusFromBody()
            {
                Default = @default,
                Source = source,
                Code = code,
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
                            Message = HandlerFailures.CodeSourceConflict,
                            Target = "code-source"
                        }
                    }
                }
            };

            // Act
            var response = await _client.PostAsync(string.Format("api/storagestatuses?api-version={0}", apiVersion), new StringContent(jsonBody, Encoding.UTF8, "application/json"));
            var responseBody = await response.Content.ReadAsStringAsync();
            var error = JsonConvert.DeserializeObject<ResponseError>(responseBody);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Conflict);
            responseBody.Should().NotBeNull();
            error.Should().Be(expectedError);
        }
        #endregion
    }
}
