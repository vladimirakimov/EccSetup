using FluentAssertions;
using ITG.Brix.EccSetup.Application.Cqs.Commands.Handlers;
using ITG.Brix.EccSetup.Domain.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace ITG.Brix.EccSetup.UnitTests.Application.Cqs.Commands.Handlers
{
    [TestClass]
    public class DeleteInstructionCommandHandlerTests
    {

        [TestMethod]
        public void ConstructorShouldSucceed()
        {
            // Arrange
            var instructionWriteRepository = new Mock<IInstructionWriteRepository>().Object;

            // Act
            Action ctor = () => { new DeleteInstructionCommandHandler(instructionWriteRepository); };

            // Assert
            ctor.Should().NotThrow();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenInstructionWriteRepositoryIsNull()
        {
            // Arrange
            IInstructionWriteRepository instructionWriteRepository = null;

            // Act
            Action ctor = () => { new DeleteInstructionCommandHandler(instructionWriteRepository); };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }
    }
}
