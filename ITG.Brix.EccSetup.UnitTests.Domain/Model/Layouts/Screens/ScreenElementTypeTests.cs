using FluentAssertions;
using ITG.Brix.EccSetup.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ITG.Brix.EccSetup.UnitTests.Domain
{
    [TestClass]
    public class ScreenElementTypeTests
    {
        [TestMethod]
        public void CreatScreenElementTypeShouldSucceed()
        {
            //Act
            var result = new ScreenElementType();
            //Assert
            result.Should().NotBeNull();
        }
    }
}
