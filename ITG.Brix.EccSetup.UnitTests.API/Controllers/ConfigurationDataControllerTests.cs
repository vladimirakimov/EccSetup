using FluentAssertions;
using ITG.Brix.EccSetup.API.Controllers;
using MediatR;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace ITG.Brix.EccSetup.UnitTests.API.Controllers
{
    [TestClass]
    public class ConfigurationDataControllerTests
    {
        [TestMethod]
        public void ConstructorShouldSucceed()
        {
            //Arrange
            var mediator = new Mock<IMediator>().Object;

            //Act
            var controller = new ConfigurationDataController(mediator);

            //Assert
            controller.Should().NotBeNull();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConstructorShouldFailWhenMediatorIsNull()
        {
            //Act
            new ConfigurationDataController(null);
        }
    }
}
