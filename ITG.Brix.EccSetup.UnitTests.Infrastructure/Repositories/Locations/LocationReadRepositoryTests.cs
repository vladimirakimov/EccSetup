using AutoMapper;
using FluentAssertions;
using ITG.Brix.EccSetup.Infrastructure.Configurations.Impl;
using ITG.Brix.EccSetup.Infrastructure.Providers.Impl;
using ITG.Brix.EccSetup.Infrastructure.Repositories;
using ITG.Brix.EccSetup.Infrastructure.Repositories.Configurations.External;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace ITG.Brix.EccSetup.UnitTests.Infrastructure.Repositories.Locations
{
    [TestClass]
    public class LocationReadRepositoryTests
    {
        [TestMethod]
        public void CtorShouldSucceed()
        {
            //Arrange
            var persistenceConfiguration = new PersistenceConfiguration("mongodb://localhost:C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==@localhost:10255/admin?ssl=true");
            var dataContext = new Mock<DataContext>(persistenceConfiguration).Object;
            var odataProvider = new Mock<LocationOdataProvider>().Object;
            var mapper = new Mock<IMapper>().Object;

            // Act
            Action ctor = () => { new LocationReadRepository(dataContext, odataProvider, mapper); };

            // Assert
            ctor.Should().NotThrow();
        }

        [TestMethod]
        public void CtorShouldFailWhenDataContextIsNull()
        {
            // Arrange
            DataContext dataContext = null;
            var odataProvider = new Mock<LocationOdataProvider>().Object;
            var mapper = new Mock<IMapper>().Object;

            // Act
            Action ctor = () => { new LocationReadRepository(dataContext, odataProvider, mapper); };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }
    }
}
