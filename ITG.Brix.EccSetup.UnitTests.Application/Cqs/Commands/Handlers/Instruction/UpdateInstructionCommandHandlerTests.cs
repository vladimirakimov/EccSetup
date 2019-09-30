using FluentAssertions;
using ITG.Brix.EccSetup.Application.Cqs.Commands.Handlers;
using ITG.Brix.EccSetup.Domain.Repositories;
using ITG.Brix.EccSetup.Infrastructure.Providers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace ITG.Brix.EccSetup.UnitTests.Application.Cqs.Commands.Handlers
{
    [TestClass]
    public class UpdateInstructionCommandHandlerTests
    {

        [TestMethod]
        public void ConstructorShouldSucceed()
        {
            // Arrange
            var instructionWriteRepository = new Mock<IInstructionWriteRepository>().Object;
            var instructionReadRepository = new Mock<IInstructionReadRepository>().Object;
            var versionProvider = new Mock<IVersionProvider>().Object;

            // Act
            Action ctor = () =>
            {
                new UpdateInstructionCommandHandler(instructionWriteRepository,
                                                    instructionReadRepository,
                                                    versionProvider);
            };

            // Assert
            ctor.Should().NotThrow();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenInstructionWriteRepositoryIsNull()
        {
            // Arrange
            IInstructionWriteRepository instructionWriteRepository = null;
            var instructionReadRepository = new Mock<IInstructionReadRepository>().Object;
            var versionProvider = new Mock<IVersionProvider>().Object;

            // Act
            Action ctor = () =>
            {
                new UpdateInstructionCommandHandler(instructionWriteRepository,
                                                    instructionReadRepository,
                                                    versionProvider);
            };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenInstructionReadRepositoryIsNull()
        {
            // Arrange
            var instructionWriteRepository = new Mock<IInstructionWriteRepository>().Object;
            IInstructionReadRepository instructionReadRepository = null;
            var versionProvider = new Mock<IVersionProvider>().Object;

            // Act
            Action ctor = () =>
            {
                new UpdateInstructionCommandHandler(instructionWriteRepository,
                                                    instructionReadRepository,
                                                    versionProvider);
            };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenVersionProviderIsNull()
        {
            // Arrange
            var instructionWriteRepository = new Mock<IInstructionWriteRepository>().Object;
            var instructionReadRepository = new Mock<IInstructionReadRepository>().Object;
            IVersionProvider versionProvider = null;

            // Act
            Action ctor = () =>
            {
                new UpdateInstructionCommandHandler(instructionWriteRepository,
                                                    instructionReadRepository,
                                                    versionProvider);
            };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }
    }
}
