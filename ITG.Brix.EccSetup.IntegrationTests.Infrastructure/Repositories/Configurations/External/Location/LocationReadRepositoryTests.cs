using AutoMapper;
using FluentAssertions;
using ITG.Brix.EccSetup.Domain.Repositories;
using ITG.Brix.EccSetup.Infrastructure.ClassMaps;
using ITG.Brix.EccSetup.Infrastructure.Configurations.Impl;
using ITG.Brix.EccSetup.Infrastructure.Constants;
using ITG.Brix.EccSetup.Infrastructure.MappingProfiles;
using ITG.Brix.EccSetup.Infrastructure.Providers.Impl;
using ITG.Brix.EccSetup.Infrastructure.Repositories.Configurations.External;
using ITG.Brix.EccSetup.IntegrationTests.Infrastructure.Bases;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;

namespace ITG.Brix.EccSetup.IntegrationTests.Infrastructure.Repositories
{
    [TestClass]
    [TestCategory("Integration")]
    public class LocationReadRepositoryTests
    {
        private ILocationReadRepository _repository;

        [ClassInitialize()]
        public static void ClassInitialize(TestContext testContext)
        {
            ClassMapsRegistrator.RegisterMaps();
        }

        [TestInitialize]
        public void TestInitialize()
        {
            var odataProvider = new Mock<ILocationOdataProvider>().Object;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<DomainToClassProfile>();
                cfg.AddProfile<ClassToDomainProfile>();
            });
            var mapper = new Mapper(config);
            RepositoryTestsHelper.Init(Consts.Collections.LocationCollectionName);
            _repository = new LocationReadRepository(new PersistenceContext(new PersistenceConfiguration(RepositoryTestsHelper.ConnectionString)),odataProvider, mapper);
        }

        [TestMethod]
        public async Task ListShouldReturnAllRecords()
        {
            // Arrange
            RepositoryHelper.ForLocation.CreateLocation(Guid.NewGuid(), "source", "site", "warehouse","gate","row","position","type",true);
            RepositoryHelper.ForLocation.CreateLocation(Guid.NewGuid(), "source2", "site", "warehouse", "gate", "row", "position", "type", false);
            RepositoryHelper.ForLocation.CreateLocation(Guid.NewGuid(), "source3", "site", "warehouse", "gate", "row", "position", "type", false);

            // Act
            var result = await _repository.ListAsync(null, null, null);

            // Assert
            result.Should().HaveCount(3);
        }
    }
}
