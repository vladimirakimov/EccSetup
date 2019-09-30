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
    public class UpdateInformationCommandHandlerTests
    {
        [TestMethod]
        public void ConstructorShouldSucceed()
        {
            // Arrange
            var informationWriteRepository = new Mock<IInformationWriteRepository>().Object;
            var informationReadRepository = new Mock<IInformationReadRepository>().Object;
            var versionProvider = new Mock<IVersionProvider>().Object;

            // Act
            Action ctor = () =>
            {
                new UpdateInformationCommandHandler(informationWriteRepository,
                                                    informationReadRepository,
                                                    versionProvider);
            };

            // Assert
            ctor.Should().NotThrow();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenInformationWriteRepositoryIsNull()
        {
            // Arrange
            IInformationWriteRepository informationWriteRepository = null;
            var informationReadRepository = new Mock<IInformationReadRepository>().Object;
            var versionProvider = new Mock<IVersionProvider>().Object;

            // Act
            Action ctor = () =>
            {
                new UpdateInformationCommandHandler(informationWriteRepository,
                                                    informationReadRepository,
                                                    versionProvider);
            };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenInformationReadRepositoryIsNull()
        {
            // Arrange
            var informationWriteRepository = new Mock<IInformationWriteRepository>().Object;
            IInformationReadRepository informationReadRepository = null;
            var versionProvider = new Mock<IVersionProvider>().Object;

            // Act
            Action ctor = () =>
            {
                new UpdateInformationCommandHandler(informationWriteRepository,
                                                    informationReadRepository,
                                                    versionProvider);
            };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenVersionProviderIsNull()
        {
            // Arrange
            var informationWriteRepository = new Mock<IInformationWriteRepository>().Object;
            var informationReadRepository = new Mock<IInformationReadRepository>().Object;
            IVersionProvider versionProvider = null;

            // Act
            Action ctor = () =>
            {
                new UpdateInformationCommandHandler(informationWriteRepository,
                                                    informationReadRepository,
                                                    versionProvider);
            };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }
    }
}
