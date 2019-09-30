using FluentAssertions;
using ITG.Brix.EccSetup.Application.Cqs.Commands.Handlers;
using ITG.Brix.EccSetup.Domain.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace ITG.Brix.EccSetup.UnitTests.Application.Cqs.Commands.Handlers
{
    [TestClass]
    public class DeleteFlowCommandHandlerTests
    {
        [TestMethod]
        public void ConstructorShouldSucceed()
        {
            // Arrange
            var flowWriteRepository = new Mock<IFlowWriteRepository>().Object;

            // Act
            Action ctor = () => { new DeleteFlowCommandHandler(flowWriteRepository); };

            // Assert
            ctor.Should().NotThrow();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenFlowWriteRepositoryIsNull()
        {
            // Arrange
            IFlowWriteRepository flowWriteRepository = null;

            // Act
            Action ctor = () => { new DeleteFlowCommandHandler(flowWriteRepository); };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }
    }
}
