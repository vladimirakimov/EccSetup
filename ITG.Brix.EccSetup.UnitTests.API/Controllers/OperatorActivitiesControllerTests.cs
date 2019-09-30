using FluentAssertions;
using ITG.Brix.EccSetup.API.Context.Services;
using ITG.Brix.EccSetup.API.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace ITG.Brix.EccSetup.UnitTests.API.Controllers
{
    [TestClass]
    public class OperatorActivitiesControllerTests
    {
        [TestMethod]
        public void ConstructorShouldSucceed()
        {
            //Arrange
            var apiResult = new Mock<IApiResult>().Object;

            //Act
            var controller = new OperatorActivitiesController(apiResult);

            //Assert
            controller.Should().NotBeNull();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConstructorShouldFailWhenApiResultIsNull()
        {
            //Arrange
            IApiResult apiResult = null;

            //Act
            var controller = new OperatorActivitiesController(apiResult);
        }
    }
}
