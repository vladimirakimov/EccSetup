using FluentAssertions;
using ITG.Brix.EccSetup.Application.Bases;
using ITG.Brix.EccSetup.Application.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ITG.Brix.EccSetup.UnitTests.Application.Bases
{
    [TestClass]
    public class CustomFaultTests
    {
        [TestMethod]
        public void ShouldHaveCustomErrorType()
        {
            // Arrange
            var fault = new CustomFault();

            // Act
            var type = fault.Type;

            // Assert
            type.Should().Be(ErrorType.CustomError);
        }
    }
}
