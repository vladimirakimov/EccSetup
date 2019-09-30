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
    public class LocationsControllerTests
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
            DatabaseHelper.Init("Locations");
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
            var site = "Site";
            string source = "Source";
            string warehouse = "Warehouse";
            string gate = "Gate";
            string row = "Row";
            string position = "Position";
            string type = "Type";
            string isRack = "true";
            var apiVersion = "1.0";

            await ControllerHelper.CreateLocation(source, site, warehouse, gate, row, position, type, isRack);

            // Act
            var response = await _client.GetAsync(string.Format("api/locations?api-version={0}", apiVersion));
            var responseBody = await response.Content.ReadAsStringAsync();
            var locationsModel = JsonConvert.DeserializeObject<LocationsModel>(responseBody);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            locationsModel.Value.Should().HaveCount(1);
            locationsModel.Count.Should().Be(1);
            locationsModel.NextLink.Should().BeNull();
        }


        [DataTestMethod]
        [DataRow("st eq 'Sitea'", 1)]
        [DataRow("st ne 'Sitea'", 25)]
        [DataRow("startswith(st, 'Site') eq true", 26)]
        [DataRow("startswith(st, 'Hello') eq false", 26)]
        [DataRow("startswith(st, 'Site') eq true and endswith(st, 'z') eq true", 1)]
        [DataRow("endswith(st, 'z') eq true", 1)]
        [DataRow("endswith(st, 'z') eq false", 25)]
        [DataRow("substringof('itea', st) eq true", 1)]
        [DataRow("tolower(st) eq 'sitea'", 1)]
        [DataRow("toupper(st) eq 'SITEA'", 1)]
        public async Task ListWithFilterShouldSucceed(string filter, int countResult)
        {
            // Arrange
            var site = "Site";
            string source = "Source";
            string warehouse = "Warehouse";
            string gate = "Gate";
            string row = "Row";
            string position = "Position";
            string type = "Type";
            string isRack = "true";
            var apiVersion = "1.0";

            string alphabet = "abcdefghijklmnopqrstuvwxyz";

            foreach (char c in alphabet)
            {
                await ControllerHelper.CreateLocation(source, site + c, warehouse, gate, row, position, type, isRack);
            }

            // Act
            var response = await _client.GetAsync(string.Format("api/locations?api-version={0}&$filter={1}", apiVersion, filter));
            var responseBody = await response.Content.ReadAsStringAsync();
            var locationsModel = JsonConvert.DeserializeObject<LocationsModel>(responseBody);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            locationsModel.Value.Should().HaveCount(countResult);
            locationsModel.Count.Should().Be(countResult);
            locationsModel.NextLink.Should().BeNull();
        }

        [DataTestMethod]
        [DataRow(0, 10, 10)]
        [DataRow(1, 10, 10)]
        [DataRow(10, 100, 16)]
        public async Task ListWithSkipAndTopShouldSucceed(int skip, int top, int countResult)
        {
            // Arrange
            var site = "Site";
            string source = "Source";
            string warehouse = "Warehouse";
            string gate = "Gate";
            string row = "Row";
            string position = "Position";
            string type = "Type";
            string isRack = "true";
            var apiVersion = "1.0";

            string alphabet = "abcdefghijklmnopqrstuvwxyz";

            foreach (char c in alphabet)
            {
                await ControllerHelper.CreateLocation(source, site + c, warehouse, gate, row, position, type, isRack);
            }

            // Act
            var response = await _client.GetAsync(string.Format("api/locations?api-version={0}&$skip={1}&$top={2}", apiVersion, skip, top));
            var responseBody = await response.Content.ReadAsStringAsync();
            var locationsModel = JsonConvert.DeserializeObject<LocationsModel>(responseBody);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            locationsModel.Value.Should().HaveCount(countResult);
            locationsModel.Count.Should().Be(countResult);
            locationsModel.NextLink.Should().BeNull();
        }

        [DataTestMethod]
        [DataRow("st ne 'Sitea'", 0, 10, 10)]
        [DataRow("st ne 'Sitea'", 0, 30, 25)]
        [DataRow("st ne 'Sitea'", 0, 10, 10)]
        [DataRow("st ne 'Sitea'", 20, 10, 5)]
        public async Task ListWithFilterSkipTopShouldSucceed(string filter, int skip, int top, int countResult)
        {
            // Arrange
            var site = "Site";
            string source = "Source";
            string warehouse = "Warehouse";
            string gate = "Gate";
            string row = "Row";
            string position = "Position";
            string type = "Type";
            string isRack = "true";
            var apiVersion = "1.0";

            string alphabet = "abcdefghijklmnopqrstuvwxyz";

            foreach (char c in alphabet)
            {
                await ControllerHelper.CreateLocation(source, site + c, warehouse, gate, row, position, type, isRack);
            }


            // Act
            var response = await _client.GetAsync(string.Format("api/locations?api-version={0}&$filter={1}&$skip={2}&$top={3}", apiVersion, filter, skip, top));
            var responseBody = await response.Content.ReadAsStringAsync();
            var businessUnitsModel = JsonConvert.DeserializeObject<LocationsModel>(responseBody);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            businessUnitsModel.Value.Should().HaveCount(countResult);
            businessUnitsModel.Count.Should().Be(countResult);
            businessUnitsModel.NextLink.Should().BeNull();
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
            var response = await _client.GetAsync(string.Format("api/locations?api-version={0}&$filter={1}", apiVersion, filter));
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
            var response = await _client.GetAsync(string.Format("api/locations?api-version={0}&$filter={1}", apiVersion, filter));
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
            var response = await _client.GetAsync(string.Format("api/locations?api-version={0}&$top={1}", apiVersion, top));

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
            var response = await _client.GetAsync(string.Format("api/locations?api-version={0}&$top={1}", apiVersion, top));
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
            var response = await _client.GetAsync(string.Format("api/locations?api-version={0}&$top={1}", apiVersion, top));
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
            var response = await _client.GetAsync(string.Format("api/locations?api-version={0}&$skip={1}", apiVersion, skip));

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
            var response = await _client.GetAsync(string.Format("api/locations?api-version={0}&$skip={1}", apiVersion, skip));
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
            var response = await _client.GetAsync(string.Format("api/locations?api-version={0}&$skip={1}", apiVersion, skip));
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
            var response = await _client.GetAsync("api/locations");
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
            var response = await _client.GetAsync(string.Format("api/locations?api-version={0}", apiVersion));
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
            var site = "Site";
            string source = "Source";
            string warehouse = "Warehouse";
            string gate = "Gate";
            string row = "Row";
            string position = "Position";
            string type = "Type";
            string isRack = "true";
            var apiVersion = "1.0";

            var body = new CreateLocationFromBody()
            {
                Gate = gate,
                IsRack = isRack,
                Position = position,
                Row = row,
                Site = site,
                Source = source,
                Type = type,
                Warehouse = warehouse
            };
            var jsonBody = JsonConvert.SerializeObject(body);

            // Act
            var response = await _client.PostAsync(string.Format("api/locations?api-version={0}", apiVersion), new StringContent(jsonBody, Encoding.UTF8, "application/json"));
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
            var site = "Site";
            string source = "Source";
            string warehouse = "Warehouse";
            string gate = "Gate";
            string row = "Row";
            string position = "Position";
            string type = "Type";
            string isRack = "true";
            var apiVersion = "1.0";

            var body = new CreateLocationFromBody()
            {
                Gate = gate,
                IsRack = isRack,
                Position = position,
                Row = row,
                Site = site,
                Source = source,
                Type = type,
                Warehouse = warehouse
            };
            var jsonBody = JsonConvert.SerializeObject(body);

            // Act
            var response = await _client.PostAsync(string.Format("api/locations?api-version={0}", apiVersion), new StringContent(jsonBody, Encoding.UTF8, "text/plain"));
            var responseBody = await response.Content.ReadAsStringAsync();

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.UnsupportedMediaType);
            responseBody.Should().BeNullOrEmpty();
        }

        [TestMethod]
        public async Task CreateShouldFailWhenQueryApiVersionIsMissing()
        {
            // Arrange
            var site = "Site";
            string source = "Source";
            string warehouse = "Warehouse";
            string gate = "Gate";
            string row = "Row";
            string position = "Position";
            string type = "Type";
            string isRack = "true";
            var apiVersion = "1.0";

            var body = new CreateLocationFromBody()
            {
                Gate = gate,
                IsRack = isRack,
                Position = position,
                Row = row,
                Site = site,
                Source = source,
                Type = type,
                Warehouse = warehouse
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
            var response = await _client.PostAsync("api/locations", new StringContent(jsonBody, Encoding.UTF8, "application/json"));
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
            var site = "Site";
            string source = "Source";
            string warehouse = "Warehouse";
            string gate = "Gate";
            string row = "Row";
            string position = "Position";
            string type = "Type";
            string isRack = "true";
            var apiVersion = "4.0";

            var body = new CreateLocationFromBody()
            {
                Gate = gate,
                IsRack = isRack,
                Position = position,
                Row = row,
                Site = site,
                Source = source,
                Type = type,
                Warehouse = warehouse
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
            var response = await _client.PostAsync(string.Format("api/locations?api-version={0}", apiVersion), new StringContent(jsonBody, Encoding.UTF8, "application/json"));
            var responseBody = await response.Content.ReadAsStringAsync();
            var error = JsonConvert.DeserializeObject<ResponseError>(responseBody);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            responseBody.Should().NotBeNull();
            error.Should().Be(expectedError);
        }

        [TestMethod]
        public async Task CreateShouldFailWhenSiteIsNull()
        {
            // Arrange
            string site = null;
            string source = "Source";
            string warehouse = "Warehouse";
            string gate = "Gate";
            string row = "Row";
            string position = "Position";
            string type = "Type";
            string isRack = "true";
            var apiVersion = "1.0";

            var body = new CreateLocationFromBody()
            {
                Gate = gate,
                IsRack = isRack,
                Position = position,
                Row = row,
                Site = site,
                Source = source,
                Type = type,
                Warehouse = warehouse
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
                            Message = ValidationFailures.LocationSiteMandatory,
                            Target = "site"
                        }
                    }
                }
            };

            // Act
            var response = await _client.PostAsync(string.Format("api/locations?api-version={0}", apiVersion), new StringContent(jsonBody, Encoding.UTF8, "application/json"));
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
            string site = "Site";
            string source = null;
            string warehouse = "Warehouse";
            string gate = "Gate";
            string row = "Row";
            string position = "Position";
            string type = "Type";
            string isRack = "true";
            var apiVersion = "1.0";

            var body = new CreateLocationFromBody()
            {
                Gate = gate,
                IsRack = isRack,
                Position = position,
                Row = row,
                Site = site,
                Source = source,
                Type = type,
                Warehouse = warehouse
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
                            Message = ValidationFailures.LocationSourceMandatory,
                            Target = "source"
                        }
                    }
                }
            };

            // Act
            var response = await _client.PostAsync(string.Format("api/locations?api-version={0}", apiVersion), new StringContent(jsonBody, Encoding.UTF8, "application/json"));
            var responseBody = await response.Content.ReadAsStringAsync();
            var error = JsonConvert.DeserializeObject<ResponseError>(responseBody);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            responseBody.Should().NotBeNull();
            error.Should().Be(expectedError);
        }

        [TestMethod]
        public async Task CreateShouldFailWhenWarehouseIsNull()
        {
            // Arrange
            string site = "Site";
            string source = "Source";
            string warehouse = null;
            string gate = "Gate";
            string row = "Row";
            string position = "Position";
            string type = "Type";
            string isRack = "true";
            var apiVersion = "1.0";

            var body = new CreateLocationFromBody()
            {
                Gate = gate,
                IsRack = isRack,
                Position = position,
                Row = row,
                Site = site,
                Source = source,
                Type = type,
                Warehouse = warehouse
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
                            Message = ValidationFailures.LocationWarehouseMandatory,
                            Target = "warehouse"
                        }
                    }
                }
            };

            // Act
            var response = await _client.PostAsync(string.Format("api/locations?api-version={0}", apiVersion), new StringContent(jsonBody, Encoding.UTF8, "application/json"));
            var responseBody = await response.Content.ReadAsStringAsync();
            var error = JsonConvert.DeserializeObject<ResponseError>(responseBody);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            responseBody.Should().NotBeNull();
            error.Should().Be(expectedError);
        }

        [TestMethod]
        public async Task CreateShouldFailWhenGateIsNull()
        {
            // Arrange
            string site = "Site";
            string source = "Source";
            string warehouse = "Warehouse";
            string gate = null;
            string row = "Row";
            string position = "Position";
            string type = "Type";
            string isRack = "true";
            var apiVersion = "1.0";

            var body = new CreateLocationFromBody()
            {
                Gate = gate,
                IsRack = isRack,
                Position = position,
                Row = row,
                Site = site,
                Source = source,
                Type = type,
                Warehouse = warehouse
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
                            Message = ValidationFailures.LocationGateMandatory,
                            Target = "gate"
                        }
                    }
                }
            };

            // Act
            var response = await _client.PostAsync(string.Format("api/locations?api-version={0}", apiVersion), new StringContent(jsonBody, Encoding.UTF8, "application/json"));
            var responseBody = await response.Content.ReadAsStringAsync();
            var error = JsonConvert.DeserializeObject<ResponseError>(responseBody);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            responseBody.Should().NotBeNull();
            error.Should().Be(expectedError);
        }

        [TestMethod]
        public async Task CreateShouldFailWhenRowIsNull()
        {
            // Arrange
            string site = "Site";
            string source = "Source";
            string warehouse = "Warehouse";
            string gate = "Gate";
            string row = null;
            string position = "Position";
            string type = "Type";
            string isRack = "true";
            var apiVersion = "1.0";

            var body = new CreateLocationFromBody()
            {
                Gate = gate,
                IsRack = isRack,
                Position = position,
                Row = row,
                Site = site,
                Source = source,
                Type = type,
                Warehouse = warehouse
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
                            Message = ValidationFailures.LocationRowMandatory,
                            Target = "row"
                        }
                    }
                }
            };

            // Act
            var response = await _client.PostAsync(string.Format("api/locations?api-version={0}", apiVersion), new StringContent(jsonBody, Encoding.UTF8, "application/json"));
            var responseBody = await response.Content.ReadAsStringAsync();
            var error = JsonConvert.DeserializeObject<ResponseError>(responseBody);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            responseBody.Should().NotBeNull();
            error.Should().Be(expectedError);
        }

        [TestMethod]
        public async Task CreateShouldFailWhenPositionIsNull()
        {
            // Arrange
            string site = "Site";
            string source = "Source";
            string warehouse = "Warehouse";
            string gate = "Gate";
            string row = "Row";
            string position = null;
            string type = "Type";
            string isRack = "true";
            var apiVersion = "1.0";

            var body = new CreateLocationFromBody()
            {
                Gate = gate,
                IsRack = isRack,
                Position = position,
                Row = row,
                Site = site,
                Source = source,
                Type = type,
                Warehouse = warehouse
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
                            Message = ValidationFailures.LocationPositionMandatory,
                            Target = "position"
                        }
                    }
                }
            };

            // Act
            var response = await _client.PostAsync(string.Format("api/locations?api-version={0}", apiVersion), new StringContent(jsonBody, Encoding.UTF8, "application/json"));
            var responseBody = await response.Content.ReadAsStringAsync();
            var error = JsonConvert.DeserializeObject<ResponseError>(responseBody);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            responseBody.Should().NotBeNull();
            error.Should().Be(expectedError);
        }

        [TestMethod]
        public async Task CreateShouldFailWhenTypeIsNull()
        {
            // Arrange
            string site = "Site";
            string source = "Source";
            string warehouse = "Warehouse";
            string gate = "Gate";
            string row = "Row";
            string position = "Position";
            string type = null;
            string isRack = "true";
            var apiVersion = "1.0";

            var body = new CreateLocationFromBody()
            {
                Gate = gate,
                IsRack = isRack,
                Position = position,
                Row = row,
                Site = site,
                Source = source,
                Type = type,
                Warehouse = warehouse
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
                            Message = ValidationFailures.LocationTypeMandatory,
                            Target = "type"
                        }
                    }
                }
            };

            // Act
            var response = await _client.PostAsync(string.Format("api/locations?api-version={0}", apiVersion), new StringContent(jsonBody, Encoding.UTF8, "application/json"));
            var responseBody = await response.Content.ReadAsStringAsync();
            var error = JsonConvert.DeserializeObject<ResponseError>(responseBody);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            responseBody.Should().NotBeNull();
            error.Should().Be(expectedError);
        }

        [TestMethod]
        public async Task CreateShouldFailWhenIsRackIsNull()
        {
            // Arrange
            string site = "Site";
            string source = "Source";
            string warehouse = "Warehouse";
            string gate = "Gate";
            string row = "Row";
            string position = "Position";
            string type = "Type";
            string isRack = null;
            var apiVersion = "1.0";

            var body = new CreateLocationFromBody()
            {
                Gate = gate,
                IsRack = isRack,
                Position = position,
                Row = row,
                Site = site,
                Source = source,
                Type = type,
                Warehouse = warehouse
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
                            Message = ValidationFailures.LocationIsRackMandatory,
                            Target = "isRack"
                        }
                    }
                }
            };

            // Act
            var response = await _client.PostAsync(string.Format("api/locations?api-version={0}", apiVersion), new StringContent(jsonBody, Encoding.UTF8, "application/json"));
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

            string site = "Site";
            string source = "Source";
            string warehouse = "Warehouse";
            string gate = "Gate";
            string row = "Row";
            string position = "Position";
            string type = "Type";
            string isRack = "true";
            var apiVersion = "1.0";

            await ControllerHelper.CreateLocation(source, site, warehouse, gate, row, position, type, isRack);

            var body = new CreateLocationFromBody()
            {
                Gate = gate,
                IsRack = isRack,
                Position = position,
                Row = row,
                Site = site,
                Source = source,
                Type = type,
                Warehouse = warehouse
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
                            Message = HandlerFailures.SourceSiteWarehouseGateRowPositionConflict,
                            Target = "source-site-warehouse-gate-row-position"
                        }
                    }
                }
            };

            // Act
            var response = await _client.PostAsync(string.Format("api/locations?api-version={0}", apiVersion), new StringContent(jsonBody, Encoding.UTF8, "application/json"));
            var responseBody = await response.Content.ReadAsStringAsync();
            var error = JsonConvert.DeserializeObject<ResponseError>(responseBody);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Conflict);
            responseBody.Should().NotBeNull();
            error.Should().Be(expectedError);
        }
    }
}

#endregion