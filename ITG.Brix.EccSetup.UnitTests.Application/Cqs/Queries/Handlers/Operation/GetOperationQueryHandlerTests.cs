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
    public class GetOperationQueryHandlerTests
    {
        [TestMethod]
        public void ConstructorShouldSucceed()
        {
            // Arrange
            var mapper = new Mock<IMapper>().Object;
            var operationReadRepository = new Mock<IOperationReadRepository>().Object;
            var iconReadRepository = new Mock<IIconReadRepository>().Object;

            // Act
            Action ctor = () =>
            {
                new GetOperationQueryHandler(mapper, operationReadRepository, iconReadRepository);
            };

            // Assert
            ctor.Should().NotThrow();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenMapperIsNull()
        {
            // Arrange
            IMapper mapper = null;
            var operationReadRepository = new Mock<IOperationReadRepository>().Object;
            var iconReadRepository = new Mock<IIconReadRepository>().Object;

            // Act
            Action ctor = () =>
            {
                new GetOperationQueryHandler(mapper, operationReadRepository, iconReadRepository);
            };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenOperationReadRepositoryIsNull()
        {
            // Arrange
            var mapper = new Mock<IMapper>().Object;
            IOperationReadRepository operationReadRepository = null;
            var iconReadRepository = new Mock<IIconReadRepository>().Object;

            // Act
            Action ctor = () =>
            {
                new GetOperationQueryHandler(mapper, operationReadRepository, iconReadRepository);
            };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenIconReadRepositoryIsNull()
        {
            // Arrange
            var mapper = new Mock<IMapper>().Object;
            var operationReadRepository = new Mock<IOperationReadRepository>().Object;
            IIconReadRepository iconReadRepository = null;

            // Act
            Action ctor = () =>
            {
                new GetOperationQueryHandler(mapper, operationReadRepository, iconReadRepository);
            };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }

        //[TestMethod]
        //public async Task HandleShouldReturnOk()
        //{
        //    // Arrange
        //    var id = Guid.NewGuid();
        //    string name = "name";
        //    string description = "description";
        //    var icon = new ColoredIcon(Guid.NewGuid(), "red");

        //    var iconId = Guid.NewGuid();
        //    var iconName = "name";
        //    var iconDataPath = "dataPath";

        //    var operationReadRepositoryMock = new Mock<IOperationReadRepository>();
        //    operationReadRepositoryMock.Setup(x => x.GetAsync(id)).Returns(Task.FromResult(new Operation(id, name)));
        //    var operationReadRepository = operationReadRepositoryMock.Object;

        //    var iconReadRepositoryMock = new Mock<IIconReadRepository>();
        //    iconReadRepositoryMock.Setup(x => x.GetAsync(id)).Returns(Task.FromResult(new Icon(iconId, iconName, iconDataPath)));
        //    var iconReadRepository = iconReadRepositoryMock.Object;

        //    var mapperMock = new Mock<IMapper>();
        //    mapperMock.Setup(x => x.Map<OperationModel>(It.IsAny<object>())).Returns(new OperationModel());
        //    var mapper = mapperMock.Object;

        //    var query = new GetOperationQuery(id);

        //    var handler = new GetOperationQueryHandler(mapper, operationReadRepository, iconReadRepository);

        //    // Act
        //    var result = await handler.Handle(query, CancellationToken.None);

        //    // Assert
        //    result.IsFailure.Should().BeFalse();
        //    result.Should().BeOfType(typeof(Result<OperationModel>));
        //}

        [TestMethod]
        public async Task HandleShouldReturnFailWhenNotFound()
        {
            // Arrange
            var id = Guid.NewGuid();

            var iconId = Guid.NewGuid();
            var iconName = "name";
            var iconDataPath = "dataPath";

            var operationReadRepositoryMock = new Mock<IOperationReadRepository>();
            operationReadRepositoryMock.Setup(x => x.GetAsync(id)).Throws<EntityNotFoundDbException>();
            var operationReadRepository = operationReadRepositoryMock.Object;

            var iconReadRepositoryMock = new Mock<IIconReadRepository>();
            iconReadRepositoryMock.Setup(x => x.GetAsync(id)).Returns(Task.FromResult(new Icon(iconId, iconName, iconDataPath)));
            var iconReadRepository = iconReadRepositoryMock.Object;

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(x => x.Map<FlowModel>(It.IsAny<object>())).Returns(new FlowModel());
            var mapper = mapperMock.Object;

            var query = new GetOperationQuery(id);

            var handler = new GetOperationQueryHandler(mapper, operationReadRepository, iconReadRepository);

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

            var iconId = Guid.NewGuid();
            var iconName = "name";
            var iconDataPath = "dataPath";

            var operationReadRepositoryMock = new Mock<IOperationReadRepository>();
            operationReadRepositoryMock.Setup(x => x.GetAsync(id)).Throws<SomeDatabaseSpecificException>();
            var operationReadRepository = operationReadRepositoryMock.Object;

            var iconReadRepositoryMock = new Mock<IIconReadRepository>();
            iconReadRepositoryMock.Setup(x => x.GetAsync(id)).Returns(Task.FromResult(new Icon(iconId, iconName, iconDataPath)));
            var iconReadRepository = iconReadRepositoryMock.Object;

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(x => x.Map<FlowModel>(It.IsAny<object>())).Returns(new FlowModel());
            var mapper = mapperMock.Object;

            var query = new GetOperationQuery(id);

            var handler = new GetOperationQueryHandler(mapper, operationReadRepository, iconReadRepository);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Failures.Should().OnlyContain(x => x.Message == CustomFailures.GetOperationFailure);
        }

        public class SomeDatabaseSpecificException : Exception { }
    }
}
