using FluentAssertions;
using ITG.Brix.EccSetup.Application.Cqs.Commands.Handlers;
using ITG.Brix.EccSetup.Domain.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace ITG.Brix.EccSetup.UnitTests.Application.Cqs.Commands.Handlers
{
    [TestClass]
    public class DeleteValidationCommandHandlerTests
    {
        [TestMethod]
        public void ConstructorShouldSucceed()
        {
            // Arrange
            var validationWriteRepository = new Mock<IValidationWriteRepository>().Object;

            // Act
            var ctor = new DeleteValidationCommandHandler(validationWriteRepository);

            // Assert
            ctor.Should().NotBeNull();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenValidationWriteRepositoryIsNull()
        {
            // Arrange
            IValidationWriteRepository validationWriteRepository = null;

            // Act
            Action ctor = () => { new DeleteValidationCommandHandler(validationWriteRepository); };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }
    }
}
