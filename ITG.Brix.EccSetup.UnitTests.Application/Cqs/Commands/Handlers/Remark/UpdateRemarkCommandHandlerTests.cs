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
    public class UpdateRemarkCommandHandlerTests
    {
        [TestMethod]
        public void ConstructorShouldSucceed()
        {
            // Arrange
            var remarkWriteRepository = new Mock<IRemarkWriteRepository>().Object;
            var remarkReadRepository = new Mock<IRemarkReadRepository>().Object;
            var versionProvider = new Mock<IVersionProvider>().Object;

            // Act
            Action ctor = () =>
            {
                new UpdateRemarkCommandHandler(remarkWriteRepository,
                                               remarkReadRepository,
                                               versionProvider);
            };

            // Assert
            ctor.Should().NotThrow();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenRemarkWriteRepositoryIsNull()
        {
            // Arrange
            IRemarkWriteRepository remarkWriteRepository = null;
            var remarkReadRepository = new Mock<IRemarkReadRepository>().Object;
            var versionProvider = new Mock<IVersionProvider>().Object;

            // Act
            Action ctor = () =>
            {
                new UpdateRemarkCommandHandler(remarkWriteRepository,
                                               remarkReadRepository,
                                               versionProvider);
            };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenRemarkReadRepositoryIsNull()
        {
            // Arrange
            var remarkWriteRepository = new Mock<IRemarkWriteRepository>().Object;
            IRemarkReadRepository remarkReadRepository = null;
            var versionProvider = new Mock<IVersionProvider>().Object;

            // Act
            Action ctor = () =>
            {
                new UpdateRemarkCommandHandler(remarkWriteRepository,
                                               remarkReadRepository,
                                               versionProvider);
            };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenVersionProviderIsNull()
        {
            // Arrange
            var remarkWriteRepository = new Mock<IRemarkWriteRepository>().Object;
            var remarkReadRepository = new Mock<IRemarkReadRepository>().Object;
            IVersionProvider versionProvider = null;

            // Act
            Action ctor = () =>
            {
                new UpdateRemarkCommandHandler(remarkWriteRepository,
                                               remarkReadRepository,
                                               versionProvider);
            };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }
    }
}
