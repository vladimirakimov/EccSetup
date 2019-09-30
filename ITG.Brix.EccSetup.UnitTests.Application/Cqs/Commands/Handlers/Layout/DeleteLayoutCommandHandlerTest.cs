using FluentAssertions;
using ITG.Brix.EccSetup.Application.Cqs.Commands.Handlers;
using ITG.Brix.EccSetup.Domain.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace ITG.Brix.EccSetup.UnitTests.Application.Cqs.Commands.Handlers
{
    [TestClass]
    public class DeleteLayoutCommandHandlerTest
    {
        [TestMethod]
        public void ConstructorShouldSucceed()
        {
            // Arrange
            var layoutWriteRepository = new Mock<ILayoutWriteRepository>().Object;

            // Act
            Action ctor = () => { new DeleteLayoutCommandHandler(layoutWriteRepository); };

            // Assert
            ctor.Should().NotThrow();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenWriteRepositoryIsNull()
        {
            // Arrange
            ILayoutWriteRepository layoutWriteRepository = null;

            // Act
            Action ctor = () => { new DeleteLayoutCommandHandler(layoutWriteRepository); };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }
    }
}
