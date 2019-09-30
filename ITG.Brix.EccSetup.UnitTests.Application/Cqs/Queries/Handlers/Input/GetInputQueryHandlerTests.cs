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
    public class GetInputQueryHandlerTests
    {
        [TestMethod]
        public void ConstructorShouldSucceed()
        {
            // Arrange
            var mapper = new Mock<IMapper>().Object;
            var inputReadRepository = new Mock<IInputReadRepository>().Object;

            // Act
            Action ctor = () =>
            {
                new GetInputQueryHandler(mapper, inputReadRepository);
            };

            // Assert
            ctor.Should().NotThrow();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenMapperIsNull()
        {
            // Arrange
            IMapper mapper = null;
            var inputReadRepository = new Mock<IInputReadRepository>().Object;

            // Act
            Action ctor = () =>
            {
                new GetInputQueryHandler(mapper, inputReadRepository);
            };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenInputReadRepositoryIsNull()
        {
            // Arrange
            var mapper = new Mock<IMapper>().Object;
            IInputReadRepository inputReadRepository = null;

            // Act
            Action ctor = () =>
            {
                new GetInputQueryHandler(mapper, inputReadRepository);
            };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public async Task HandleShouldReturnOk()
        {
            // Arrange
            var id = Guid.NewGuid();
            var name = "name";
            var description = "description";
            Guid icon = Guid.NewGuid();
            var instruction = "instruction";

            var inputReadRepositoryMock = new Mock<IInputReadRepository>();
            inputReadRepositoryMock.Setup(x => x.GetAsync(id)).Returns(Task.FromResult(new Input(id, name, description, icon, instruction)));
            var inputReadRepository = inputReadRepositoryMock.Object;

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(x => x.Map<InputModel>(It.IsAny<object>())).Returns(new InputModel());
            var mapper = mapperMock.Object;

            var query = new GetInputQuery(id);

            var handler = new GetInputQueryHandler(mapper, inputReadRepository);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsFailure.Should().BeFalse();
            result.Should().BeOfType(typeof(Result<InputModel>));
        }

        [TestMethod]
        public async Task HandleShouldReturnFailWhenNotFound()
        {
            // Arrange
            var id = Guid.NewGuid();

            var inputReadRepositoryMock = new Mock<IInputReadRepository>();
            inputReadRepositoryMock.Setup(x => x.GetAsync(id)).Throws<EntityNotFoundDbException>();
            var inputReadRepository = inputReadRepositoryMock.Object;

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(x => x.Map<InputModel>(It.IsAny<object>())).Returns(new InputModel());
            var mapper = mapperMock.Object;

            var query = new GetInputQuery(id);

            var handler = new GetInputQueryHandler(mapper, inputReadRepository);

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

            var inputReadRepositoryMock = new Mock<IInputReadRepository>();
            inputReadRepositoryMock.Setup(x => x.GetAsync(id)).Throws<SomeDatabaseSpecificException>();
            var inputReadRepository = inputReadRepositoryMock.Object;

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(x => x.Map<InputModel>(It.IsAny<object>())).Returns(new InputModel());
            var mapper = mapperMock.Object;

            var query = new GetInputQuery(id);

            var handler = new GetInputQueryHandler(mapper, inputReadRepository);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Failures.Should().OnlyContain(x => x.Message == CustomFailures.GetInputFailure);
        }

        public class SomeDatabaseSpecificException : Exception { }
    }
}
