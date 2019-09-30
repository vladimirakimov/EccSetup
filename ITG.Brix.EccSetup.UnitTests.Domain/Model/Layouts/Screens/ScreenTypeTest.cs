using FluentAssertions;
using ITG.Brix.EccSetup.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ITG.Brix.EccSetup.UnitTests.Domain
{
    [TestClass]
    public class ScreenTypeTest
    {
        [TestMethod]
        public void CreateScreenTypeShouldSucceed()
        {
            //Act
            var result = new ScreenType();
            //Assert
            result.Should().NotBeNull();
        }
    }
}
