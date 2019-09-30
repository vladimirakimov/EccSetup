using FluentAssertions;
using ITG.Brix.EccSetup.API.Context.Providers;
using ITG.Brix.EccSetup.API.Context.Providers.Impl;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ITG.Brix.EccSetup.UnitTests.API.Context.Providers
{
    [TestClass]
    public class JsonProviderTests
    {
        [TestMethod]
        public void GenerateShouldBeInRange()
        {
            // Arrange
            IJsonProvider jsonProvider = new JsonProvider();
            var json = @"{
                                    ""uno"" : ""Uno"",
                                    ""due"" : ""Due""
                                 }";

            // Act
            var result = jsonProvider.ToDictionary(json);

            // Assert
            result.Should().NotBeNull();
            result.Keys.Count.Should().Be(2);
            result["uno"].Should().Be("Uno");
            result["due"].Should().Be("Due");
        }
    }
}
