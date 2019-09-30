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
    public class UpdateSourceCommandHandlerTests
    {
        [TestMethod]
        public void ConstructorShouldSucceed()
        {
            // Arrange
            var sourceWriteRepository = new Mock<ISourceWriteRepository>().Object;
            var sourceReadRepository = new Mock<ISourceReadRepository>().Object;
            var versionProvider = new Mock<IVersionProvider>().Object;

            // Act
            Action ctor = () =>
            {
                new UpdateSourceCommandHandler(sourceWriteRepository,
                                               sourceReadRepository,
                                               versionProvider);
            };

            // Assert
            ctor.Should().NotThrow();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenSourceWriteRepositoryIsNull()
        {
            // Arrange
            ISourceWriteRepository sourceWriteRepository = null;
            var sourceReadRepository = new Mock<ISourceReadRepository>().Object;
            var versionProvider = new Mock<IVersionProvider>().Object;

            // Act
            Action ctor = () =>
            {
                new UpdateSourceCommandHandler(sourceWriteRepository,
                                               sourceReadRepository,
                                               versionProvider);
            };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenSourceReadRepositoryIsNull()
        {
            // Arrange
            var sourceWriteRepository = new Mock<ISourceWriteRepository>().Object;
            ISourceReadRepository sourceReadRepository = null;
            var versionProvider = new Mock<IVersionProvider>().Object;

            // Act
            Action ctor = () =>
            {
                new UpdateSourceCommandHandler(sourceWriteRepository,
                                               sourceReadRepository,
                                               versionProvider);
            };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenVersionProviderIsNull()
        {
            // Arrange
            var sourceWriteRepository = new Mock<ISourceWriteRepository>().Object;
            var sourceReadRepository = new Mock<ISourceReadRepository>().Object;
            IVersionProvider versionProvider = null;

            // Act
            Action ctor = () =>
            {
                new UpdateSourceCommandHandler(sourceWriteRepository,
                                               sourceReadRepository,
                                               versionProvider);
            };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }
    }
}
