using AutoMapper;
using FluentAssertions;
using ITG.Brix.EccSetup.Domain;
using ITG.Brix.EccSetup.Domain.Repositories;
using ITG.Brix.EccSetup.Infrastructure.ClassMaps;
using ITG.Brix.EccSetup.Infrastructure.Configurations.Impl;
using ITG.Brix.EccSetup.Infrastructure.Constants;
using ITG.Brix.EccSetup.Infrastructure.Exceptions;
using ITG.Brix.EccSetup.Infrastructure.MappingProfiles;
using ITG.Brix.EccSetup.Infrastructure.Repositories.Configurations.External;
using ITG.Brix.EccSetup.IntegrationTests.Infrastructure.Bases;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ITG.Brix.EccSetup.IntegrationTests.Infrastructure.Repositories
{
    [TestClass]
    [TestCategory("Integration")]
    public class LocationWriteRepositoryTests
    {
        private ILocationWriteRepository _repository;

        [ClassInitialize()]
        public static void ClassInitialize(TestContext testContext)
        {
            ClassMapsRegistrator.RegisterMaps();
        }

        [TestInitialize]
        public void TestInitialize()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<DomainToClassProfile>();
                cfg.AddProfile<ClassToDomainProfile>();
            });
            var mapper = new Mapper(config);
            RepositoryTestsHelper.Init(Consts.Collections.LocationCollectionName);
            _repository = new LocationWriteRepository(new PersistenceContext(new PersistenceConfiguration(RepositoryTestsHelper.ConnectionString)), mapper);
        }

        [TestMethod]
        public async Task CreateShouldSucceed()
        {
            //Arrange
            var id = Guid.NewGuid();
            var source = "TestSource";
            var site = "TestSite";
            var warehouse = "TestWarehouse";
            var gate = "TestGate";
            var row = "TestRow";
            var position = "TestPosition";
            var type = "TestType";
            var isRack = true;

            var location = new Location(id, source, site, warehouse, gate, row, position, type, isRack);

            //Act
            await _repository.CreateAsync(location);
            //Asssert
            var data = RepositoryHelper.ForLocation.GetLocations();
            data.Should().HaveCount(1);
            var result = data.First();
            result.Gate.Should().Be(gate);
            result.Id.Should().Be(id);
            result.Source.Should().Be(source);
            result.IsRack.Should().Be(isRack);
            result.Position.Should().Be(position);
            result.Row.Should().Be(row);
            result.Site.Should().Be(site);
            result.Warehouse.Should().Be(warehouse);
        }

        [TestMethod]
        public async Task CreateShouldFailWhenParametersAreSame()
        {
            //Arrange
            var id = Guid.NewGuid();
            var source = "TestSource";
            var site = "TestSite";
            var warehouse = "TestWarehouse";
            var gate = "TestGate";
            var row = "TestRow";
            var position = "TestPosition";
            var type = "TestType";
            var isRack = true;

            var location = new Location(id, source, site, warehouse, gate, row, position, type, isRack);
            await _repository.CreateAsync(location);
            var location2 = new Location(id, source, site, warehouse, gate, row, position, type, isRack);
            //Act
            Action act = () => { _repository.CreateAsync(location2).GetAwaiter().GetResult(); };
            //Assert
            act.Should().Throw<UniqueKeyException>();
        }
    }
}
