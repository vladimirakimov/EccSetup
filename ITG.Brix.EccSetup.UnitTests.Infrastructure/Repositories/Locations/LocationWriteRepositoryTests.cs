using AutoMapper;
using FluentAssertions;
using ITG.Brix.EccSetup.Infrastructure.Configurations.Impl;
using ITG.Brix.EccSetup.Infrastructure.MappingProfiles;
using ITG.Brix.EccSetup.Infrastructure.Repositories;
using ITG.Brix.EccSetup.Infrastructure.Repositories.Configurations.External;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace ITG.Brix.EccSetup.UnitTests.Infrastructure.Repositories.Locations
{
    [TestClass]
    public class LocationWriteRepositoryTests
    {
     

        [TestMethod]
        public void CtorShouldSucceed()
        {
            //Arrange
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<DomainToClassProfile>();
                cfg.AddProfile<ClassToDomainProfile>();
            });
            var mapper = new Mapper(config);
            var persistenceConfiguration = new PersistenceConfiguration("mongodb://localhost:C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==@localhost:10255/admin?ssl=true");
            var dataContext = new Mock<DataContext>(persistenceConfiguration).Object;

            // Act
            Action ctor = () => { new LocationWriteRepository(dataContext, mapper); };

            // Assert
            ctor.Should().NotThrow();
        }

        [TestMethod]
        public void CtorShouldFailWhenDataContextIsNull()
        {
            // Arrange
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<DomainToClassProfile>();
                cfg.AddProfile<ClassToDomainProfile>();
            });
            var mapper = new Mapper(config);
            DataContext dataContext = null;

            // Act
            Action ctor = () => { new LocationWriteRepository(dataContext, mapper); };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }
    }
}
