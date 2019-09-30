using FluentAssertions;
using ITG.Brix.EccSetup.API.Context.Bases;
using ITG.Brix.EccSetup.API.Context.Constants;
using ITG.Brix.EccSetup.API.Context.Resources;
using ITG.Brix.EccSetup.API.Context.Services.Responses.Models.Errors;
using ITG.Brix.EccSetup.Application.Bases;
using ITG.Brix.EccSetup.Application.Cqs.Queries.Models.Files;
using ITG.Brix.EccSetup.Application.Resources;
using ITG.Brix.EccSetup.IntegrationTests.Bases;
using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ITG.Brix.EccSetup.IntegrationTests.Controllers
{
    [TestClass]
    [TestCategory("Integration")]
    public class FilesControllerTests
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
            _client = ControllerTestsHelper.GetClient();
        }

        [TestMethod]
        public async Task UploadShouldFailWhenQueryApiVersionIsMissing()
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

            //Act
            var response = await _client.PostAsync($"api/files/upload", new StringContent("test"));
            var responseAsString = await response.Content.ReadAsStringAsync();
            var error = JsonConvert.DeserializeObject<ResponseError>(responseAsString);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            error.Should().Be(expectedError);
        }

        [TestMethod]
        public async Task UploadShouldFailWhenApiVersionIsInvalid()
        {
            // Arrange
            var apiVersion = "4.0";
            //HttpResponseMessage response = null;

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
            var response = await _client.PostAsync($"api/files/upload?api-version={apiVersion}", new StringContent("test"));
            var responseAsString = await response.Content.ReadAsStringAsync();
            var error = JsonConvert.DeserializeObject<ResponseError>(responseAsString);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            error.Should().Be(expectedError);
        }

        [TestMethod]
        public async Task UploadShouldFailWhenFileExtensionIsInvalid()
        {
            //Arrange
            HttpResponseMessage response = null;
            var apiVersion = "1.0";
            var expectedError = new ResponseError
            {
                Error = new ResponseErrorBody
                {
                    Code = ServiceError.InvalidFileExtension.Code,
                    Message = ServiceError.InvalidFileExtension.Message,
                    Details = new List<ResponseErrorField>
                    {
                        new ResponseErrorField
                        {
                            Code = Consts.Failure.Detail.Code.InvalidFileExtension,
                            Message = RequestFailures.InvalidFileExtension,
                            Target = Consts.Failure.Detail.Target.Extension
                        }
                    }
                }
            };

            var fileMock = new Mock<IFormFile>();
            //Setup mock file using a memory stream
            var content = "Hello World from a Fake File";
            var fileName = "test.txt";
            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            writer.Write(content);
            writer.Flush();
            ms.Position = 0;
            fileMock.Setup(_ => _.OpenReadStream()).Returns(ms);
            fileMock.Setup(_ => _.FileName).Returns(fileName);
            fileMock.Setup(_ => _.Length).Returns(ms.Length);

            var file = fileMock.Object;

            //Act
            using (var formData = new MultipartFormDataContent("boundary=--------------------------164116569546715533816671"))
            {
                var fileToUpload = new StreamContent(file.OpenReadStream());
                fileToUpload.Headers.ContentType = new MediaTypeHeaderValue("multipart/form-data");
                formData.Add(fileToUpload, "File", "test.txt");
                response = await _client.PostAsync($"api/files/upload?api-version={apiVersion}", formData);
            }
            var responseAsString = await response.Content.ReadAsStringAsync();
            var error = JsonConvert.DeserializeObject<ResponseError>(responseAsString);

            //Assert
            response.StatusCode.Should().Be((HttpStatusCode)422);
            error.Should().Be(expectedError);
        }

        [TestMethod]
        public async Task UploadShouldSucceed()
        {
            //Arrange
            HttpResponseMessage response = null;
            var apiVersion = "1.0";

            var fileMock = new Mock<IFormFile>();
            //Setup mock file using a memory stream
            var content = "Hello World from a Fake File";
            var fileName = "test.jpg";
            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            writer.Write(content);
            writer.Flush();
            ms.Position = 0;
            fileMock.Setup(_ => _.OpenReadStream()).Returns(ms);
            fileMock.Setup(_ => _.FileName).Returns(fileName);
            fileMock.Setup(_ => _.Length).Returns(ms.Length);

            var file = fileMock.Object;

            //Act
            using (var formData = new MultipartFormDataContent("boundary=--------------------------164116569546715533816671"))
            {
                var fileToUpload = new StreamContent(file.OpenReadStream());
                fileToUpload.Headers.ContentType = new MediaTypeHeaderValue("multipart/form-data");
                formData.Add(fileToUpload, "File", "test.jpg");
                response = await _client.PostAsync($"api/files/upload?api-version={apiVersion}", formData);
            }
            var responseAsString = await response.Content.ReadAsStringAsync();
            var error = JsonConvert.DeserializeObject<ResponseError>(responseAsString);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.Created);
            response.Headers.Location.Should().NotBeNull();
        }

        [TestMethod]
        public async Task DownloadFileShouldFailWhenQueryApiVersionIsMissing()
        {
            //Arrange
            var fileName = "855f5e77-0583-4bac-954a-7f0aca6067d1";
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
            var response = await _client.GetAsync($"api/files/download/{fileName}");
            var responseAsString = await response.Content.ReadAsStringAsync();
            var error = JsonConvert.DeserializeObject<ResponseError>(responseAsString);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            error.Should().Be(expectedError);
        }

        [TestMethod]
        public async Task DownloadShouldFailWhenApiVersionIsInvalid()
        {
            //Arrange
            var apiVersion = "5.0";
            var fileName = "855f5e77-0583-4bac-954a-7f0aca6067d1.jpg";

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
            var response = await _client.GetAsync($"api/files/download/{fileName}?api-version={apiVersion}");
            var responseAsString = await response.Content.ReadAsStringAsync();
            var error = JsonConvert.DeserializeObject<ResponseError>(responseAsString);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            error.Should().Be(expectedError);
        }

        [TestMethod]
        public async Task DownloadShouldReturnNotFoundIfFileNotExist()
        {
            //Arrange
            var apiVersion = "1.0";
            var fileName = $"{Guid.NewGuid()}.jpg";

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
                            Message = string.Format(HandlerFailures.NotFound, "File"),
                            Target = "file"
                        }
                    }
                }
            };

            //Act
            var response = await _client.GetAsync($"api/files/download/{fileName}?api-version={apiVersion}");
            var responseAsString = await response.Content.ReadAsStringAsync();
            var error = JsonConvert.DeserializeObject<ResponseError>(responseAsString);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
            error.Should().Be(expectedError);

        }

        [TestMethod]
        public async Task DownloadShouldSucceed()
        {
            //Arrange 

            var apiVersion = "1.0";
            var fileName = "bde7b772-227b-418b-b958-15ec9ab12c49.jpg";
            HttpResponseMessage responseUpload = null;

            var fileMock = new Mock<IFormFile>();
            //Setup mock file using a memory stream
            var content = "Hello World from a Fake File";
            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            writer.Write(content);
            writer.Flush();
            ms.Position = 0;
            fileMock.Setup(_ => _.OpenReadStream()).Returns(ms);
            fileMock.Setup(_ => _.FileName).Returns(fileName);
            fileMock.Setup(_ => _.Length).Returns(ms.Length);

            var file = fileMock.Object;

            //Act
            using (var formData = new MultipartFormDataContent("boundary=--------------------------164116569546715533816671"))
            {
                var fileToUpload = new StreamContent(file.OpenReadStream());
                fileToUpload.Headers.ContentType = new MediaTypeHeaderValue("multipart/form-data");
                formData.Add(fileToUpload, "File", "test.jpg");
                responseUpload = await _client.PostAsync($"api/files/upload?api-version={apiVersion}", formData);
            }
            var responseUploadAsString = await responseUpload.Content.ReadAsStringAsync();
            var error = JsonConvert.DeserializeObject<ResponseError>(responseUploadAsString);

            //Act
            var response = await _client.GetAsync($"api/files/download/{fileName}?api-version={apiVersion}");
            var responseAsString = await response.Content.ReadAsStringAsync();
            var fileDownloadModel = JsonConvert.DeserializeObject<FileDownloadModel>(responseAsString);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
