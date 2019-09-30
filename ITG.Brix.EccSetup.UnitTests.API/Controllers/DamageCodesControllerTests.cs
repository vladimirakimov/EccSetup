using FluentAssertions;
using ITG.Brix.EccSetup.API.Context.Services;
using ITG.Brix.EccSetup.API.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace ITG.Brix.EccSetup.UnitTests.API.Controllers
{
    [TestClass]
    public class DamageCodesControllerTests
    {
        [TestMethod]
        public void ConstructorShouldRegisterAllDependencies()
        {
            // Arrange
            var apiResult = new Mock<IApiResult>().Object;

            // Act
            var controller = new DamageCodesController(apiResult);

            // Assert
            controller.Should().NotBeNull();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenMediatorIsNull()
        {
            // Arrange
            IApiResult apiResult = null;

            // Act
            Action ctor = () => { new DamageCodesController(apiResult); };

            // Assert
            ctor.Should().Throw<ArgumentNullException>().WithMessage($"*{nameof(apiResult)}*");
        }
    }
}
