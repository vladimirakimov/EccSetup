using FluentAssertions;
using ITG.Brix.EccSetup.API.Context.Services.Responses.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ITG.Brix.EccSetup.UnitTests.API.Context.Services.Responses.Results
{
    [TestClass]
    public class CustomOkResultTests
    {
        [TestMethod]
        public void ConstructorShouldSucceed()
        {
            // Arrange
            var location = string.Format("/sources/{0}", Guid.NewGuid());
            var eTagValue = "234234324325";

            // Act
            var obj = new CustomOkResult(location, eTagValue);

            // Assert
            obj.Should().NotBeNull();
        }

        [TestMethod]
        public void ConstructorShouldSetETagValue()
        {
            // Arrange
            var location = string.Format("/sources/{0}", Guid.NewGuid());
            var eTagValue = "234234324325";

            // Act
            var obj = new CustomOkResult(location, eTagValue);

            // Assert
            obj.ETagValue.Should().Be(eTagValue);
        }
    }
}
