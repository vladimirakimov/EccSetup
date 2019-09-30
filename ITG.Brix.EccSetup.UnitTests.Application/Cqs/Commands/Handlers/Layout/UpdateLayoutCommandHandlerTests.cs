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
    public class UpdateLayoutCommandHandlerTests
    {
        [TestMethod]
        public void ConstructorShouldSucceed()
        {
            // Arrange
            var layoutWriteRepository = new Mock<ILayoutWriteRepository>().Object;
            var layoutReadRepository = new Mock<ILayoutReadRepository>().Object;
            var versionProvider = new Mock<IVersionProvider>().Object;

            // Act
            Action ctor = () => { new UpdateLayoutCommandHandler(layoutWriteRepository, layoutReadRepository, versionProvider); };

            // Assert
            ctor.Should().NotThrow();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenLayoutWriteRepositoryIsNull()
        {
            // Arrange
            ILayoutWriteRepository layoutWriteRepository = null;
            var layoutReadRepository = new Mock<ILayoutReadRepository>().Object;
            var versionProvider = new Mock<IVersionProvider>().Object;

            // Act
            Action ctor = () => { new UpdateLayoutCommandHandler(layoutWriteRepository, layoutReadRepository, versionProvider); };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenLayoutReadRepositoryIsNull()
        {
            // Arrange
            var layoutWriteRepository = new Mock<ILayoutWriteRepository>().Object;
            ILayoutReadRepository layoutReadRepository = null;
            var versionProvider = new Mock<IVersionProvider>().Object;

            // Act
            Action ctor = () => { new UpdateLayoutCommandHandler(layoutWriteRepository, layoutReadRepository, versionProvider); };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenVersionProviderIsNull()
        {
            // Arrange
            var layoutWriteRepository = new Mock<ILayoutWriteRepository>().Object;
            var layoutReadRepository = new Mock<ILayoutReadRepository>().Object;
            IVersionProvider versionProvider = null;

            // Act
            Action ctor = () => { new UpdateLayoutCommandHandler(layoutWriteRepository, layoutReadRepository, versionProvider); };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }
    }
}
