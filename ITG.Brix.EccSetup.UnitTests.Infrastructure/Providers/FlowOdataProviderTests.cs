using FluentAssertions;
using ITG.Brix.EccSetup.Infrastructure.Providers.Bases.Impl.Dtos;
using ITG.Brix.EccSetup.Infrastructure.Providers.Impl;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ITG.Brix.EccSetup.UnitTests.Infrastructure.Providers
{
    [TestClass]
    public class FlowOdataProviderTests
    {
        [TestMethod]
        public void GetFilterPropertiesShouldSucceed()
        {
            //Arrange
            var provider = new FlowOdataProvider();
            var matches = new[] { "source eq 'BKAL33+KBT T'" };

            //Act
            var properties = provider.GetFilterProperties(matches);

            //Assert
            properties.Should().ContainEquivalentOf(new PropertyValue { Name = "source", Value = "BKAL33+KBT T" });

        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void GetFilterPropertiesShouldFailWhenFilterHasWrongFormat()
        {
            var provider = new FlowOdataProvider();
            var matches = new[] { "wrongformatfilter" };

            //Act
            var properties = provider.GetFilterProperties(matches);
        }
    }
}
