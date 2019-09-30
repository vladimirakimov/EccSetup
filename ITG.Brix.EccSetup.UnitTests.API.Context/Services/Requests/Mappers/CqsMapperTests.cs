using FluentAssertions;
using ITG.Brix.EccSetup.API.Context.Providers;
using ITG.Brix.EccSetup.API.Context.Services.Requests.Mappers.Impl;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace ITG.Brix.EccSetup.UnitTests.API.Context.Services.Requests.Mappers
{
    [TestClass]
    public class CqsMapperTests
    {
        [TestMethod]
        public void ConstructorShouldSucceed()
        {
            // Arrange

            var jsonProvider = new Mock<IJsonProvider>().Object;

            // Act
            var mapper = new CqsMapper(jsonProvider);

            // Assert
            mapper.Should().NotBeNull();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenJsonProviderIsNull()
        {
            // Arrange
            IJsonProvider jsonProvider = null;

            // Act
            Action ctor = () => { new CqsMapper(jsonProvider); };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }
    }
}
