using AutoMapper;
using FluentAssertions;
using ITG.Brix.EccSetup.Application.Bases;
using ITG.Brix.EccSetup.Application.Cqs.Queries.Definitions;
using ITG.Brix.EccSetup.Application.Cqs.Queries.Handlers;
using ITG.Brix.EccSetup.Application.Cqs.Queries.Models;
using ITG.Brix.EccSetup.Application.Resources;
using ITG.Brix.EccSetup.Domain;
using ITG.Brix.EccSetup.Domain.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace ITG.Brix.EccSetup.UnitTests.Application.Cqs.Queries.Handlers
{
    [TestClass]
    public class ListInstructionQueryHandlerTests
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
                new ListInstructionQueryHandler(mapper, instructionReadRepository);
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
                new ListInstructionQueryHandler(mapper, instructionReadRepository);
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
                new ListInstructionQueryHandler(mapper, instructionReadRepository);
            };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public async Task HandleShouldReturnOk()
        {
            // Arrange
            var instructionReadRepositoryMock = new Mock<IInstructionReadRepository>();
            instructionReadRepositoryMock.Setup(x => x.ListAsync(It.IsAny<Expression<Func<Instruction, bool>>>(), It.IsAny<int?>(), It.IsAny<int?>())).Returns(Task.FromResult(Enumerable.Empty<Instruction>()));
            var instructionReadRepository = instructionReadRepositoryMock.Object;

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(x => x.Map<IEnumerable<InstructionModel>>(It.IsAny<object>())).Returns(Enumerable.Empty<InstructionModel>());
            var mapper = mapperMock.Object;

            var query = new ListInstructionQuery(null, null, null);

            var handler = new ListInstructionQueryHandler(mapper, instructionReadRepository);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsFailure.Should().BeFalse();
            result.Should().BeOfType(typeof(Result<InstructionsModel>));
        }

        [TestMethod]
        public async Task HandleShouldReturnFailWhenDatabaseSpecificErrorOccurs()
        {
            // Arrange
            var instructionReadRepositoryMock = new Mock<IInstructionReadRepository>();
            instructionReadRepositoryMock.Setup(x => x.ListAsync(It.IsAny<Expression<Func<Instruction, bool>>>(), It.IsAny<int?>(), It.IsAny<int?>())).Throws<SomeDatabaseSpecificException>();
            var instructionReadRepository = instructionReadRepositoryMock.Object;

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(x => x.Map<IEnumerable<InstructionModel>>(It.IsAny<object>())).Returns(Enumerable.Empty<InstructionModel>());
            var mapper = mapperMock.Object;

            var query = new ListInstructionQuery(null, null, null);

            var handler = new ListInstructionQueryHandler(mapper, instructionReadRepository);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Failures.Should().OnlyContain(x => x.Message == CustomFailures.ListInstructionFailure);
        }

        public class SomeDatabaseSpecificException : Exception { }
    }
}
