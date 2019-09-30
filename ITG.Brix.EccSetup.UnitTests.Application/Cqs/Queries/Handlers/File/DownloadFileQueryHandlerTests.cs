using AutoMapper;
using FluentAssertions;
using ITG.Brix.EccSetup.Application.Bases;
using ITG.Brix.EccSetup.Application.Cqs.Queries.Definitions.File;
using ITG.Brix.EccSetup.Application.Cqs.Queries.Handlers.File;
using ITG.Brix.EccSetup.Application.Resources;
using ITG.Brix.EccSetup.Infrastructure.Exceptions;
using ITG.Brix.EccSetup.Infrastructure.Storage;
using ITG.Brix.EccSetup.Infrastructure.Storage.Dtos;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ITG.Brix.EccSetup.UnitTests.Application.Cqs.Queries.Handlers.File
{
    [TestClass]
    public class DownloadFileQueryHandlerTests
    {
        [TestMethod]
        public void ConstructorShouldSucceed()
        {
            //Arrange
            var blobStorageMock = new Mock<IBlobStorage>().Object;
            var mapperMock = new Mock<IMapper>().Object;

            //Act
            var handler = new DownloadFileQueryHandler(blobStorageMock, mapperMock);

            //Assert
            handler.Should().NotBeNull();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConstructorShouldFailWhenBlobStorageIsNull()
        {
            //Arrange
            IBlobStorage blobStorage = null;
            var mapperMock = new Mock<IMapper>().Object;

            //Act
            var handler = new DownloadFileQueryHandler(blobStorage, mapperMock);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConstructorShouldFailWhenMapperIsNull()
        {
            //Arrange
            var blobStorageMock = new Mock<IBlobStorage>().Object;
            IMapper mapper = null;

            //Act
            var handler = new DownloadFileQueryHandler(blobStorageMock, mapper);
        }

        [TestMethod]
        public async Task HandleShouldSucceed()
        {
            //Arrange
            var blobStorageMock = new Mock<IBlobStorage>();
            blobStorageMock.Setup(x => x.DownloadFileAsync(It.IsAny<string>())).Returns(Task.FromResult(new FileDownloadDto()));
            var blobStorage = blobStorageMock.Object;
            var mapperMock = new Mock<IMapper>().Object;
            var query = new DownloadFileQuery("test");

            //Act
            var handler = new DownloadFileQueryHandler(blobStorage, mapperMock);
            var result = await handler.Handle(query, CancellationToken.None);

            //Assert
            result.Should().NotBeNull();
        }

        [TestMethod]
        public async Task HandleShouldReturnFailIfFileNotFound()
        {
            //Arrange
            var blobStorageMock = new Mock<IBlobStorage>();
            blobStorageMock.Setup(x => x.DownloadFileAsync(It.IsAny<string>())).Throws(new BlobNotFoundException(new Exception("test")));
            var blobStorage = blobStorageMock.Object;
            var mapperMock = new Mock<IMapper>().Object;
            var query = new DownloadFileQuery("test");
            var expectedError = new HandlerFault()
            {
                Code = HandlerFaultCode.NotFound.Name,
                Message = string.Format(HandlerFailures.NotFound, "File"),
                Target = "file"
            };

            //Act
            var handler = new DownloadFileQueryHandler(blobStorage, mapperMock);
            var result = await handler.Handle(query, CancellationToken.None);

            //Assert
            result.IsFailure.Should().BeTrue();
            result.Failures.Should().ContainEquivalentOf(expectedError);
        }

        [TestMethod]
        public async Task HandleShouldReturnFailWhenExceptionThrown()
        {
            //Arrange
            var blobStorageMock = new Mock<IBlobStorage>();
            blobStorageMock.Setup(x => x.DownloadFileAsync(It.IsAny<string>())).Throws(new Exception("Test"));
            var blobStorage = blobStorageMock.Object;
            var mapperMock = new Mock<IMapper>().Object;
            var query = new DownloadFileQuery("test");
            var expectedError = new CustomFault
            {
                Message = CustomFailures.DownloadFileFailure
            };

            //Act
            var handler = new DownloadFileQueryHandler(blobStorage, mapperMock);
            var result = await handler.Handle(query, CancellationToken.None);

            //Assert
            result.IsFailure.Should().BeTrue();
            result.Failures.Should().ContainEquivalentOf(expectedError);
        }
    }
}
