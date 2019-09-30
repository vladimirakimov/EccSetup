using FluentAssertions;
using ITG.Brix.EccSetup.Application.Cqs.Commands.Handlers;
using ITG.Brix.EccSetup.Domain.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace ITG.Brix.EccSetup.UnitTests.Application.Cqs.Commands.Handlers
{
    [TestClass]
    public class DeleteOperationCommandHandlerTests
    {
        [TestMethod]
        public void ConstructorShouldSucceed()
        {
            // Arrange
            var operationWriteRepository = new Mock<IOperationWriteRepository>().Object;

            // Act
            Action ctor = () => { new DeleteOperationCommandHandler(operationWriteRepository); };

            // Assert
            ctor.Should().NotThrow();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenOperationWriteRepositoryIsNull()
        {
            // Arrange
            IOperationWriteRepository operationWriteRepository = null;

            // Act
            Action ctor = () => { new DeleteOperationCommandHandler(operationWriteRepository); };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }
    }
}
