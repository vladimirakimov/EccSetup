using FluentAssertions;
using ITG.Brix.EccSetup.Application.Cqs.Commands.Handlers;
using ITG.Brix.EccSetup.Domain.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace ITG.Brix.EccSetup.UnitTests.Application.Cqs.Commands.Handlers
{
    [TestClass]
    public class DeleteIconCommandHandlerTests
    {
        [TestMethod]
        public void ConstructorShouldSucceed()
        {
            // Arrange
            var iconWriteRepository = new Mock<IIconWriteRepository>().Object;

            // Act
            Action ctor = () => { new DeleteIconCommandHandler(iconWriteRepository); };

            // Assert
            ctor.Should().NotThrow();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenIconWriteRepositoryIsNull()
        {
            // Arrange
            IIconWriteRepository iconWriteRepository = null;

            // Act
            Action ctor = () => { new DeleteIconCommandHandler(iconWriteRepository); };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }
    }
}
