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
    public class UpdateOperationCommandHandlerTests
    {
        [TestMethod]
        public void ConstructorShouldSucceed()
        {
            // Arrange
            var operationWriteRepository = new Mock<IOperationWriteRepository>().Object;
            var operationReadRepository = new Mock<IOperationReadRepository>().Object;
            var versionProvider = new Mock<IVersionProvider>().Object;

            // Act
            Action ctor = () =>
            {
                new UpdateOperationCommandHandler(operationWriteRepository,
                                                  operationReadRepository,
                                                  versionProvider);
            };

            // Assert
            ctor.Should().NotThrow();
        }


        [TestMethod]
        public void ConstructorShouldFailWhenOperationWriteRepositoryIsNull()
        {
            // Arrange
            IOperationWriteRepository operationWriteRepository = null;
            var operationReadRepository = new Mock<IOperationReadRepository>().Object;
            var versionProvider = new Mock<IVersionProvider>().Object;

            // Act
            Action ctor = () =>
            {
                new UpdateOperationCommandHandler(operationWriteRepository,
                                                  operationReadRepository,
                                                  versionProvider);
            };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenOperationReadRepositoryIsNull()
        {
            // Arrange
            var operationWriteRepository = new Mock<IOperationWriteRepository>().Object;
            IOperationReadRepository operationReadRepository = null;
            var versionProvider = new Mock<IVersionProvider>().Object;

            // Act
            Action ctor = () =>
            {
                new UpdateOperationCommandHandler(operationWriteRepository,
                                                  operationReadRepository,
                                                  versionProvider);
            };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenVersionProviderIsNull()
        {
            // Arrange
            var operationWriteRepository = new Mock<IOperationWriteRepository>().Object;
            var operationReadRepository = new Mock<IOperationReadRepository>().Object;
            IVersionProvider versionProvider = null;

            // Act
            Action ctor = () =>
            {
                new UpdateOperationCommandHandler(operationWriteRepository,
                                                  operationReadRepository,
                                                  versionProvider);
            };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }
    }
}
