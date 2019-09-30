using FluentAssertions;
using ITG.Brix.EccSetup.Application.Cqs.Commands.Handlers;
using ITG.Brix.EccSetup.Domain.Repositories;
using ITG.Brix.EccSetup.Infrastructure.Providers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;

namespace ITG.Brix.EccSetup.UnitTests.Application.Cqs.Commands.Handlers
{
    [TestClass]
    public class CreateFlowCommandHandlerTests
    {
        [TestMethod]
        public void ConstructorShouldSucceed()
        {
            // Arrange
            var flowWriteRepository = new Mock<IFlowWriteRepository>().Object;
            var identifierProvider = new Mock<IIdentifierProvider>().Object;
            var versionProvider = new Mock<IVersionProvider>().Object;

            // Act
            Action ctor = () =>
            {
                new CreateFlowCommandHandler(flowWriteRepository,
                                             identifierProvider,
                                             versionProvider);
            };

            // Assert
            ctor.Should().NotThrow();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenFlowWriteRepositoryIsNull()
        {
            // Arrange
            IFlowWriteRepository flowWriteRepository = null;
            var identifierProvider = new Mock<IIdentifierProvider>().Object;
            var versionProvider = new Mock<IVersionProvider>().Object;

            // Act
            Action ctor = () =>
            {
                new CreateFlowCommandHandler(flowWriteRepository,
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
            var flowWriteRepository = new Mock<IFlowWriteRepository>().Object;
            IIdentifierProvider identifierProvider = null;
            var versionProvider = new Mock<IVersionProvider>().Object;

            // Act
            Action ctor = () =>
            {
                new CreateFlowCommandHandler(flowWriteRepository,
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
            var flowWriteRepository = new Mock<IFlowWriteRepository>().Object;
            var identifierProvider = new Mock<IIdentifierProvider>().Object;
            IVersionProvider versionProvider = null;

            // Act
            Action ctor = () =>
            {
                new CreateFlowCommandHandler(flowWriteRepository,
                                             identifierProvider,
                                             versionProvider);
            };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }        
    }
}
