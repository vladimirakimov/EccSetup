using FluentAssertions;
using ITG.Brix.EccSetup.Application.Cqs.Commands.Handlers;
using ITG.Brix.EccSetup.Domain.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace ITG.Brix.EccSetup.UnitTests.Application.Cqs.Commands.Handlers
{
    [TestClass]
    public class DeleteInformationCommandHandlerTests
    {
        [TestMethod]
        public void ConstructorShouldSucceed()
        {
            // Arrange
            var informationWriteRepository = new Mock<IInformationWriteRepository>().Object;

            // Act
            Action ctor = () => { new DeleteInformationCommandHandler(informationWriteRepository); };

            // Assert
            ctor.Should().NotThrow();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenInformationWriteRepositoryIsNull()
        {
            // Arrange
            IInformationWriteRepository informationWriteRepository = null;

            // Act
            Action ctor = () => { new DeleteInformationCommandHandler(informationWriteRepository); };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }
    }
}
