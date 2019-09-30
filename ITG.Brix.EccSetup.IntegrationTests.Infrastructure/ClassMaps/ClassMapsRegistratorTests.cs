using FluentAssertions;
using ITG.Brix.EccSetup.Infrastructure.ClassMaps;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ITG.Brix.EccSetup.IntegrationTests.Infrastructure.ClassMaps
{
    [TestClass]
    public class ClassMapsRegistratorTests
    {
        [TestMethod]
        public void RegisterMapsShouldSucceed()
        {
            Exception exception = null;
            try
            {
                ClassMapsRegistrator.RegisterMaps();
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            exception.Should().BeNull();
        }
    }
}

