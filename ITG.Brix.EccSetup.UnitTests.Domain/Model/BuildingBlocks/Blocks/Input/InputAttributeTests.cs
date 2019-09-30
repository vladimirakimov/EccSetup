using FluentAssertions;
using ITG.Brix.EccSetup.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ITG.Brix.EccSetup.UnitTests.Domain
{
    [TestClass]
    public class InputAttributeTests
    {
        string name = "any name";
        bool notMandatory = true;
        bool scanningOnly = true;
        int minLenght = new Random().Next();
        int maxLenght = new Random().Next();
        int checkLastXCharacters = new Random().Next();
        string prefix = "any prefix";

        [TestMethod]
        public void CreateShouldSuccess()
        {
            // Arrange

            // Act
            var result = new InputAttribute(name, notMandatory, scanningOnly, minLenght, maxLenght, prefix, checkLastXCharacters);
            // Assert
            result.Name.Should().Be(name);
            result.ScanningOnly.Should().Be(scanningOnly);
            result.MinimumLenght.Should().Be(minLenght);
            result.MaximumLenght.Should().Be(maxLenght);
            result.Prefix.Should().Be(prefix);
            result.CheckLastXCharacters.Should().Be(checkLastXCharacters);
        }
    }
}
