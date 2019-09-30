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
    public class GetInstructionQueryHandlerTests
    {
        [TestMethod]
        public void ConstructorShouldSucceed()
        {
            // Arrange
            var mapper = new Mock<IMapper>().Object;
            var instructionReadRepository = new Mock<IInstructionReadRepository>().Object;

            // Act
            Action ctor = () =>
            {
                new GetInstructionQueryHandler(mapper, instructionReadRepository);
            };

            // Assert
            ctor.Should().NotThrow();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenMapperIsNull()
        {
            // Arrange
            IMapper mapper = null;
            var instructionReadRepository = new Mock<IInstructionReadRepository>().Object;

            // Act
            Action ctor = () =>
            {
                new GetInstructionQueryHandler(mapper, instructionReadRepository);
            };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenInstructionReadRepositoryIsNull()
        {
            // Arrange
            var mapper = new Mock<IMapper>().Object;
            IInstructionReadRepository instructionReadRepository = null;

            // Act
            Action ctor = () =>
            {
                new GetInstructionQueryHandler(mapper, instructionReadRepository);
            };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public async Task HandleShouldReturnOk()
        {
            // Arrange
            var id = Guid.NewGuid();

            var instructionReadRepositoryMock = new Mock<IInstructionReadRepository>();
            instructionReadRepositoryMock.Setup(x => x.GetAsync(id)).Returns(Task.FromResult(new Instruction(id, "name", "description", "icon", "content", "image", "video")));
            var instructionReadRepository = instructionReadRepositoryMock.Object;

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(x => x.Map<InstructionModel>(It.IsAny<object>())).Returns(new InstructionModel());
            var mapper = mapperMock.Object;

            var query = new GetInstructionQuery(id);

            var handler = new GetInstructionQueryHandler(mapper, instructionReadRepository);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsFailure.Should().BeFalse();
            result.Should().BeOfType(typeof(Result<InstructionModel>));
        }

        [TestMethod]
        public async Task HandleShouldReturnFailWhenNotFound()
        {
            // Arrange
            var id = Guid.NewGuid();

            var instructionReadRepositoryMock = new Mock<IInstructionReadRepository>();
            instructionReadRepositoryMock.Setup(x => x.GetAsync(id)).Throws<EntityNotFoundDbException>();
            var instructionReadRepository = instructionReadRepositoryMock.Object;

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(x => x.Map<InstructionModel>(It.IsAny<object>())).Returns(new InstructionModel());
            var mapper = mapperMock.Object;

            var query = new GetInstructionQuery(id);

            var handler = new GetInstructionQueryHandler(mapper, instructionReadRepository);

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

            var instructionReadRepositoryMock = new Mock<IInstructionReadRepository>();
            instructionReadRepositoryMock.Setup(x => x.GetAsync(id)).Throws<SomeDatabaseSpecificException>();
            var instructionReadRepository = instructionReadRepositoryMock.Object;

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(x => x.Map<InstructionModel>(It.IsAny<object>())).Returns(new InstructionModel());
            var mapper = mapperMock.Object;

            var query = new GetInstructionQuery(id);

            var handler = new GetInstructionQueryHandler(mapper, instructionReadRepository);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Failures.Should().OnlyContain(x => x.Message == CustomFailures.GetInstructionFailure);
        }

        public class SomeDatabaseSpecificException : Exception { }
    }
}
