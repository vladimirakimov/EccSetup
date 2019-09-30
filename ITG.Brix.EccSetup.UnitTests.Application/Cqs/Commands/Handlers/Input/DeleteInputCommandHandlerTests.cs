using FluentAssertions;
using ITG.Brix.EccSetup.Application.Cqs.Commands.Handlers;
using ITG.Brix.EccSetup.Domain.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace ITG.Brix.EccSetup.UnitTests.Application.Cqs.Commands.Handlers
{
    [TestClass]
    public class DeleteInputCommandHandlerTests
    {
        [TestMethod]
        public void ConstructorShouldSucceed()
        {
            // Arrange
            var inputWriteRepository = new Mock<IInputWriteRepository>().Object;

            // Act
            Action ctor = () => { new DeleteInputCommandHandler(inputWriteRepository); };

            // Assert
            ctor.Should().NotThrow();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenInputWriteRepositoryIsNull()
        {
            // Arrange
            IInputWriteRepository inputWriteRepository = null;

            // Act
            Action ctor = () => { new DeleteInputCommandHandler(inputWriteRepository); };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }
    }
}
