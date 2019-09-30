using FluentAssertions;
using ITG.Brix.EccSetup.Application.Cqs.Commands.Handlers;
using ITG.Brix.EccSetup.Domain.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace ITG.Brix.EccSetup.UnitTests.Application.Cqs.Commands.Handlers
{
    [TestClass]
    public class DeleteRemarkCommandHandlerTests
    {
        [TestMethod]
        public void ConstructorShouldSucceed()
        {
            // Arrange
            var remarkWriteRepository = new Mock<IRemarkWriteRepository>().Object;

            // Act
            var ctor = new DeleteRemarkCommandHandler(remarkWriteRepository);

            // Assert
            ctor.Should().NotBeNull();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenRemarkWriteRepositoryIsNull()
        {
            // Arrange
            IRemarkWriteRepository remarkWriteRepository = null;

            // Act
            Action ctor = () => { new DeleteRemarkCommandHandler(remarkWriteRepository); };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }
    }
}
