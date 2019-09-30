using FluentAssertions;
using ITG.Brix.EccSetup.Application.Cqs.Commands.Handlers;
using ITG.Brix.EccSetup.Domain.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace ITG.Brix.EccSetup.UnitTests.Application.Cqs.Commands.Handlers
{
    [TestClass]
    public class DeleteSourceCommandHandlerTests
    {
        [TestMethod]
        public void ConstructorShouldSucceed()
        {
            // Arrange
            var sourceWriteRepository = new Mock<ISourceWriteRepository>().Object;

            // Act
            var ctor = new DeleteSourceCommandHandler(sourceWriteRepository);

            // Assert
            ctor.Should().NotBeNull();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenSourceWriteRepositoryIsNull()
        {
            // Arrange
            ISourceWriteRepository sourceWriteRepository = null;

            // Act
            Action ctor = () => { new DeleteSourceCommandHandler(sourceWriteRepository); };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }
    }
}
