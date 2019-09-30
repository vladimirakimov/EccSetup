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
    public class UpdateInputCommandHandlerTests
    {
        [TestMethod]
        public void ConstructorShouldSucceed()
        {
            // Arrange
            var inputWriteRepository = new Mock<IInputWriteRepository>().Object;
            var inputReadRepository = new Mock<IInputReadRepository>().Object;
            var versionProvider = new Mock<IVersionProvider>().Object;

            // Act
            Action ctor = () =>
            {
                new UpdateInputCommandHandler(inputWriteRepository,
                                              inputReadRepository,
                                              versionProvider);
            };

            // Assert
            ctor.Should().NotThrow();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenInputWriteRepositoryIsNull()
        {
            // Arrange
            IInputWriteRepository inputWriteRepository = null;
            var inputReadRepository = new Mock<IInputReadRepository>().Object;
            var versionProvider = new Mock<IVersionProvider>().Object;

            // Act
            Action ctor = () =>
            {
                new UpdateInputCommandHandler(inputWriteRepository,
                                              inputReadRepository,
                                              versionProvider);
            };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenInputReadRepositoryIsNull()
        {
            // Arrange
            var inputWriteRepository = new Mock<IInputWriteRepository>().Object;
            IInputReadRepository inputReadRepository = null;
            var versionProvider = new Mock<IVersionProvider>().Object;

            // Act
            Action ctor = () =>
            {
                new UpdateInputCommandHandler(inputWriteRepository,
                                              inputReadRepository,
                                              versionProvider);
            };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenVersionProviderIsNull()
        {
            // Arrange
            var inputWriteRepository = new Mock<IInputWriteRepository>().Object;
            var inputReadRepository = new Mock<IInputReadRepository>().Object;
            IVersionProvider versionProvider = null;

            // Act
            Action ctor = () =>
            {
                new UpdateInputCommandHandler(inputWriteRepository,
                                              inputReadRepository,
                                              versionProvider);
            };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }
    }
}
