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
    public class UpdateBusinessUnitCommandHandlerTests
    {
        [TestMethod]
        public void ConstructorShouldSucceed()
        {
            // Arrange
            var businessUnitWriteRepository = new Mock<IBusinessUnitWriteRepository>().Object;
            var businessUnitReadRepository = new Mock<IBusinessUnitReadRepository>().Object;
            var versionProvider = new Mock<IVersionProvider>().Object;

            // Act
            Action ctor = () =>
            {
                new UpdateBusinessUnitCommandHandler(businessUnitWriteRepository,
                                                     businessUnitReadRepository,
                                                     versionProvider);
            };

            // Assert
            ctor.Should().NotThrow();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenBusinessUnitWriteRepositoryIsNull()
        {
            // Arrange
            IBusinessUnitWriteRepository businessUnitWriteRepository = null;
            var businessUnitReadRepository = new Mock<IBusinessUnitReadRepository>().Object;
            var versionProvider = new Mock<IVersionProvider>().Object;

            // Act
            Action ctor = () =>
            {
                new UpdateBusinessUnitCommandHandler(businessUnitWriteRepository,
                                                     businessUnitReadRepository,
                                                     versionProvider);
            };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenBusinessUnitReadRepositoryIsNull()
        {
            // Arrange
            var businessUnitWriteRepository = new Mock<IBusinessUnitWriteRepository>().Object;
            IBusinessUnitReadRepository businessUnitReadRepository = null;
            var versionProvider = new Mock<IVersionProvider>().Object;

            // Act
            Action ctor = () =>
            {
                new UpdateBusinessUnitCommandHandler(businessUnitWriteRepository,
                                                     businessUnitReadRepository,
                                                     versionProvider);
            };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenVersionProviderIsNull()
        {
            // Arrange
            var businessUnitWriteRepository = new Mock<IBusinessUnitWriteRepository>().Object;
            var businessUnitReadRepository = new Mock<IBusinessUnitReadRepository>().Object;
            IVersionProvider versionProvider = null;

            // Act
            Action ctor = () =>
            {
                new UpdateBusinessUnitCommandHandler(businessUnitWriteRepository,
                                                     businessUnitReadRepository,
                                                     versionProvider);
            };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }
    }
}
