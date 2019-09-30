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
    public class UpdateIconCommandHandlerTests
    {
        [TestMethod]
        public void ConstructorShouldSucceed()
        {
            // Arrange
            var iconWriteRepository = new Mock<IIconWriteRepository>().Object;
            var iconReadRepository = new Mock<IIconReadRepository>().Object;
            var versionProvider = new Mock<IVersionProvider>().Object;

            // Act
            Action ctor = () =>
            {
                new UpdateIconCommandHandler(iconWriteRepository,
                                             iconReadRepository,
                                             versionProvider);
            };

            // Assert
            ctor.Should().NotThrow();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenIconWriteRepositoryIsNull()
        {
            // Arrange
            IIconWriteRepository iconWriteRepository = null;
            var iconReadRepository = new Mock<IIconReadRepository>().Object;
            var versionProvider = new Mock<IVersionProvider>().Object;

            // Act
            Action ctor = () =>
            {
                new UpdateIconCommandHandler(iconWriteRepository,
                                             iconReadRepository,
                                             versionProvider);
            };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenIconReadRepositoryIsNull()
        {
            // Arrange
            var iconWriteRepository = new Mock<IIconWriteRepository>().Object;
            IIconReadRepository iconReadRepository = null;
            var versionProvider = new Mock<IVersionProvider>().Object;

            // Act
            Action ctor = () =>
            {
                new UpdateIconCommandHandler(iconWriteRepository,
                                             iconReadRepository,
                                             versionProvider);
            };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenVersionProviderIsNull()
        {
            // Arrange
            var iconWriteRepository = new Mock<IIconWriteRepository>().Object;
            var iconReadRepository = new Mock<IIconReadRepository>().Object;
            IVersionProvider versionProvider = null;

            // Act
            Action ctor = () =>
            {
                new UpdateIconCommandHandler(iconWriteRepository,
                                             iconReadRepository,
                                             versionProvider);
            };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }
    }
}
