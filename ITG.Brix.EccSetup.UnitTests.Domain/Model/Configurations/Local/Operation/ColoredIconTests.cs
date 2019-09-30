using FluentAssertions;
using ITG.Brix.EccSetup.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ITG.Brix.EccSetup.UnitTests.Domain
{
    [TestClass]
    public class ColoredIconTests
    {
        [TestMethod]
        public void CreateColoredIconShouldSucceed()
        {
            // Arrange
            var iconId = new Guid("16c924d0-73da-417a-a110-cd328595243a");
            var fillColor = "#000000";

            // Act
            var icon = new ColoredIcon(iconId, fillColor);

            // Assert
            icon.Should().NotBeNull();
        }

        [TestMethod]
        public void CreateColoredIconShouldFailWhenIconIdIsDefault()
        {
            // Arrange
            var iconId = Guid.Empty;
            var fillColor = "#000000";

            // Act
            Action ctor = () => { new ColoredIcon(iconId, fillColor); };

            // Assert
            ctor.Should().Throw<ArgumentException>();
        }

        [TestMethod]
        public void CreateColoredIconShouldFailWhenFillColorIsEmpty()
        {
            // Arrange
            var iconId = new Guid("16c924d0-73da-417a-a110-cd328595243a");
            var fillColor = string.Empty;

            // Act
            Action ctor = () => { new ColoredIcon(iconId, fillColor); };

            // Assert
            ctor.Should().Throw<ArgumentException>();
        }

        [TestMethod]
        public void CreateColoredIconShouldFailWhenFillColorIsNull()
        {
            // Arrange
            var iconId = new Guid("16c924d0-73da-417a-a110-cd328595243a");
            string fillColor = null;

            // Act
            Action ctor = () => { new ColoredIcon(iconId, fillColor); };

            // Assert
            ctor.Should().Throw<ArgumentException>();
        }
    }
}
