using FluentAssertions;
using ITG.Brix.EccSetup.Application.Cqs.Commands.Handlers;
using ITG.Brix.EccSetup.Domain.Repositories;
using ITG.Brix.EccSetup.Infrastructure.Providers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace ITG.Brix.EccSetup.UnitTests.Application.Cqs.Commands.Handlers.DamageCode
{
    [TestClass]
    public class CreateDamageCodeCommandHandlerTests
    {
        [TestMethod]
        public void ConstructorShouldSucceed()
        {
            // Arrange
            var damageCodeWriteRepository = new Mock<IDamageCodeWriteRepository>().Object;
            var identifierProvider = new Mock<IIdentifierProvider>().Object;
            var versionProvider = new Mock<IVersionProvider>().Object;

            // Act
            Action ctor = () =>
            {
                new CreateDamageCodeCommandHandler(damageCodeWriteRepository,
                                                     identifierProvider,
                                                     versionProvider);
            };

            // Assert
            ctor.Should().NotThrow();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenWriteRepositoryIsNull()
        {
            // Arrange
            IDamageCodeWriteRepository damageCodeWriteRepository = null;
            var identifierProvider = new Mock<IIdentifierProvider>().Object;
            var versionProvider = new Mock<IVersionProvider>().Object;

            // Act
            Action ctor = () =>
            {
                new CreateDamageCodeCommandHandler(damageCodeWriteRepository,
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
            var damageCodeWriteRepository = new Mock<IDamageCodeWriteRepository>().Object;
            IIdentifierProvider identifierProvider = null;
            var versionProvider = new Mock<IVersionProvider>().Object;

            // Act
            Action ctor = () =>
            {
                new CreateDamageCodeCommandHandler(damageCodeWriteRepository,
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
            var damageCodeWriteRepository = new Mock<IDamageCodeWriteRepository>().Object;
            var identifierProvider = new Mock<IIdentifierProvider>().Object;
            IVersionProvider versionProvider = null;

            // Act
            Action ctor = () =>
            {
                new CreateDamageCodeCommandHandler(damageCodeWriteRepository,
                                                     identifierProvider,
                                                     versionProvider);
            };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }
    }
}
