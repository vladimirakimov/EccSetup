using AutoMapper;
using FluentAssertions;
using ITG.Brix.EccSetup.Application.Bases;
using ITG.Brix.EccSetup.Application.Cqs.Queries.Definitions;
using ITG.Brix.EccSetup.Application.Cqs.Queries.Handlers;
using ITG.Brix.EccSetup.Application.Cqs.Queries.Models;
using ITG.Brix.EccSetup.Application.Resources;
using ITG.Brix.EccSetup.Domain;
using ITG.Brix.EccSetup.Domain.Repositories;
using ITG.Brix.EccSetup.Infrastructure.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ITG.Brix.EccSetup.UnitTests.Application.Cqs.Queries.Handlers
{
    [TestClass]
    public class GetIconQueryHandlerTests
    {
        [TestMethod]
        public void ConstructorShouldSucceed()
        {
            // Arrange
            var mapper = new Mock<IMapper>().Object;
            var iconReadRepository = new Mock<IIconReadRepository>().Object;

            // Act
            Action ctor = () =>
            {
                new GetIconQueryHandler(mapper, iconReadRepository);
            };

            // Assert
            ctor.Should().NotThrow();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenMapperIsNull()
        {
            // Arrange
            IMapper mapper = null;
            var iconReadRepository = new Mock<IIconReadRepository>().Object;

            // Act
            Action ctor = () =>
            {
                new GetIconQueryHandler(mapper, iconReadRepository);
            };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenIconReadRepositoryIsNull()
        {
            // Arrange
            var mapper = new Mock<IMapper>().Object;
            IIconReadRepository iconReadRepository = null;

            // Act
            Action ctor = () =>
            {
                new GetIconQueryHandler(mapper, iconReadRepository);
            };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public async Task HandleShouldReturnOk()
        {
            // Arrange
            var id = Guid.NewGuid();
            string name = "name";
            string dataPath = "datapath";

            var iconReadRepositoryMock = new Mock<IIconReadRepository>();
            iconReadRepositoryMock.Setup(x => x.GetAsync(id)).Returns(Task.FromResult(new Icon(id, name, dataPath)));
            var iconReadRepository = iconReadRepositoryMock.Object;

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(x => x.Map<IconModel>(It.IsAny<object>())).Returns(new IconModel());
            var mapper = mapperMock.Object;

            var query = new GetIconQuery(id);

            var handler = new GetIconQueryHandler(mapper, iconReadRepository);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsFailure.Should().BeFalse();
            result.Should().BeOfType(typeof(Result<IconModel>));
        }

        [TestMethod]
        public async Task HandleShouldReturnFailWhenNotFound()
        {
            // Arrange
            var id = Guid.NewGuid();

            var iconReadRepositoryMock = new Mock<IIconReadRepository>();
            iconReadRepositoryMock.Setup(x => x.GetAsync(id)).Throws<EntityNotFoundDbException>();
            var iconReadRepository = iconReadRepositoryMock.Object;

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(x => x.Map<IconModel>(It.IsAny<object>())).Returns(new IconModel());
            var mapper = mapperMock.Object;

            var query = new GetIconQuery(id);

            var handler = new GetIconQueryHandler(mapper, iconReadRepository);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Failures.Should().OnlyContain(x => x.Code == HandlerFaultCode.NotFound.Name);
        }

        [TestMethod]
        public async Task HandleShouldReturnFailWhenDatabaseSpecificErrorOccurs()
        {
            // Arrange
            var id = Guid.NewGuid();

            var iconReadRepositoryMock = new Mock<IIconReadRepository>();
            iconReadRepositoryMock.Setup(x => x.GetAsync(id)).Throws<SomeDatabaseSpecificException>();
            var iconReadRepository = iconReadRepositoryMock.Object;

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(x => x.Map<IconModel>(It.IsAny<object>())).Returns(new IconModel());
            var mapper = mapperMock.Object;

            var query = new GetIconQuery(id);

            var handler = new GetIconQueryHandler(mapper, iconReadRepository);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Failures.Should().OnlyContain(x => x.Message == CustomFailures.GetIconFailure);
        }

        public class SomeDatabaseSpecificException : Exception { }
    }
}
