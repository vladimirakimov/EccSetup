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
    public class UpdateValidationCommandHandlerTests
    {
        [TestMethod]
        public void ConstructorShouldSucceed()
        {
            // Arrange
            var validationWriteRepository = new Mock<IValidationWriteRepository>().Object;
            var validationReadRepository = new Mock<IValidationReadRepository>().Object;
            var versionProvider = new Mock<IVersionProvider>().Object;

            // Act
            Action ctor = () =>
            {
                new UpdateValidationCommandHandler(validationWriteRepository,
                                               validationReadRepository,
                                               versionProvider);
            };

            // Assert
            ctor.Should().NotThrow();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenValidationWriteRepositoryIsNull()
        {
            // Arrange
            IValidationWriteRepository validationWriteRepository = null;
            var validationReadRepository = new Mock<IValidationReadRepository>().Object;
            var versionProvider = new Mock<IVersionProvider>().Object;

            // Act
            Action ctor = () =>
            {
                new UpdateValidationCommandHandler(validationWriteRepository,
                                               validationReadRepository,
                                               versionProvider);
            };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenValidationReadRepositoryIsNull()
        {
            // Arrange
            var validationWriteRepository = new Mock<IValidationWriteRepository>().Object;
            IValidationReadRepository validationReadRepository = null;
            var versionProvider = new Mock<IVersionProvider>().Object;

            // Act
            Action ctor = () =>
            {
                new UpdateValidationCommandHandler(validationWriteRepository,
                                               validationReadRepository,
                                               versionProvider);
            };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenVersionProviderIsNull()
        {
            // Arrange
            var validationWriteRepository = new Mock<IValidationWriteRepository>().Object;
            var validationReadRepository = new Mock<IValidationReadRepository>().Object;
            IVersionProvider versionProvider = null;

            // Act
            Action ctor = () =>
            {
                new UpdateValidationCommandHandler(validationWriteRepository,
                                               validationReadRepository,
                                               versionProvider);
            };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }
    }
}
