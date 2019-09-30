using FluentAssertions;
using ITG.Brix.EccSetup.Application.Cqs.Commands.Handlers;
using ITG.Brix.EccSetup.Domain.Repositories;
using ITG.Brix.EccSetup.Infrastructure.Providers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace ITG.Brix.EccSetup.UnitTests.Application.Cqs.Commands.Handlers.OperationalDepartment
{
    [TestClass]
    public class CreateOperationalDepartmentCommandHandlerTests
    {
        [TestMethod]
        public void ConstructorShouldSucceed()
        {
            // Arrange
            var operationalDepartmentWriteRepository = new Mock<IOperationalDepartmentWriteRepository>().Object;
            var identifierProvider = new Mock<IIdentifierProvider>().Object;
            var versionProvider = new Mock<IVersionProvider>().Object;

            // Act
            Action ctor = () =>
            {
                new CreateOperationalDepartmentCommandHandler(operationalDepartmentWriteRepository,
                                                     identifierProvider,
                                                     versionProvider);
            };

            // Assert
            ctor.Should().NotThrow();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenBusinessUnitWriteRepositoryIsNull()
        {
            // Arrange
            IOperationalDepartmentWriteRepository operationalDepartmentWriteRepository = null;
            var identifierProvider = new Mock<IIdentifierProvider>().Object;
            var versionProvider = new Mock<IVersionProvider>().Object;

            // Act
            Action ctor = () =>
            {
                new CreateOperationalDepartmentCommandHandler(operationalDepartmentWriteRepository,
                                                     identifierProvider,
                                                     versionProvider);
            };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenIdentifierProviderIsNull()
        {
            // Arrange
            var operationalDepartmentWriteRepository = new Mock<IOperationalDepartmentWriteRepository>().Object;
            IIdentifierProvider identifierProvider = null;
            var versionProvider = new Mock<IVersionProvider>().Object;

            // Act
            Action ctor = () =>
            {
                new CreateOperationalDepartmentCommandHandler(operationalDepartmentWriteRepository,
                                             identifierProvider,
                                             versionProvider);
            };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenVersionProviderIsNull()
        {
            // Arrange
            var operationalDepartmentWriteRepository = new Mock<IOperationalDepartmentWriteRepository>().Object;
            var identifierProvider = new Mock<IIdentifierProvider>().Object;
            IVersionProvider versionProvider = null;

            // Act
            Action ctor = () =>
            {
                new CreateOperationalDepartmentCommandHandler(operationalDepartmentWriteRepository,
                                             identifierProvider,
                                             versionProvider);
            };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }
    }
}
