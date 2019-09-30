using FluentAssertions;
using ITG.Brix.EccSetup.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ITG.Brix.EccSetup.UnitTests.Domain
{
    [TestClass]
    public class RemarkIconTests
    {
        [TestMethod]
        public void CreateRemarkIconShouldSucceed()
        {
            // Arrange
            var iconId = Guid.NewGuid();

            // Act
            var icon = new RemarkIcon(iconId);

            // Assert
            icon.Should().NotBeNull();
        }

        [TestMethod]
        public void CreateIconShouldFailWhenIconIdIsDefault()
        {
            // Arrange
            var iconId = Guid.Empty;

            // Act
            Action ctor = () => { new RemarkIcon(iconId); };

            // Assert
            ctor.Should().Throw<ArgumentException>();
        }
    }
}
