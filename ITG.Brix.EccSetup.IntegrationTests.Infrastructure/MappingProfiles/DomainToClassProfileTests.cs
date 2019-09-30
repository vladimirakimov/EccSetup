using AutoMapper;
using FluentAssertions;
using ITG.Brix.EccSetup.Domain;
using ITG.Brix.EccSetup.Infrastructure.DataAccess.ClassModels;
using ITG.Brix.EccSetup.Infrastructure.MappingProfiles;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ITG.Brix.EccSetup.IntegrationTests.Infrastructure.MappingProfiles
{
    [TestClass]
    public class DomainToClassProfileTests
    {
        [TestMethod]
        public void LocationToLocationClassShouldMapCorrectly()
        {
            // Arrange
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<DomainToClassProfile>();
            });
            IMapper mapper = new Mapper(config);
            var id = Guid.NewGuid();
            var location = new Location(id, "source", "site", "warehouse", "gate", "row", "position", "type", true);

            // Act
            var model = mapper.Map<LocationClass>(location);

            // Assert
            model.Should().NotBeNull();
            model.Id.Should().Be(id);
            model.G.Should().Be(location.Gate);
            model.P.Should().Be(location.Position);
            model.R.Should().Be(location.Row);
            model.Ra.Should().Be(location.IsRack);
            model.Sc.Should().Be(location.Source);
            model.St.Should().Be(location.Site);
            model.T.Should().Be(location.Type);
            model.W.Should().Be(location.Warehouse);
        }
    }
}