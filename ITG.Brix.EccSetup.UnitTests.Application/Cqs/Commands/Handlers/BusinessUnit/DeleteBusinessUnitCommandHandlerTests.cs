using FluentAssertions;
using ITG.Brix.EccSetup.Application.Cqs.Commands.Handlers;
using ITG.Brix.EccSetup.Domain.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace ITG.Brix.EccSetup.UnitTests.Application.Cqs.Commands.Handlers
{
    [TestClass]
    public class DeleteBusinessUnitCommandHandlerTests
    {
        [TestMethod]
        public void ConstructorShouldSucceed()
        {
            // Arrange
            var businessUnitWriteRepository = new Mock<IBusinessUnitWriteRepository>().Object;

            // Act
            Action ctor = () => { new DeleteBusinessUnitCommandHandler(businessUnitWriteRepository); };

            // Assert
            ctor.Should().NotThrow();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenBusinessUnitWriteRepositoryIsNull()
        {
            // Arrange
            IBusinessUnitWriteRepository businessUnitWriteRepository = null;

            // Act
            Action ctor = () => { new DeleteBusinessUnitCommandHandler(businessUnitWriteRepository); };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }
    }
}
