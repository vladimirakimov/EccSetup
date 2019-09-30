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
    public class ClassToDomainProfileTests
    {
        [TestMethod]
        public void LocationClassToLocationShouldMapCorrectly()
        {
            // Arrange
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ClassToDomainProfile>();
            });
            IMapper mapper = new Mapper(config);
            var id = Guid.NewGuid();

            var locationclass = new LocationClass();
            locationclass.Id = id;
            locationclass.G = "Gate";
            locationclass.P = "Position";
            locationclass.R = "Row";
            locationclass.Ra = true;
            locationclass.Sc = "Source";
            locationclass.St = "Site";
            locationclass.T = "Type";
            locationclass.W = "warehouse";
            // Act
            var model = mapper.Map<Location>(locationclass);

            // Assert
            model.Should().NotBeNull();
            model.Id.Should().Be(id);
            model.Gate.Should().Be(locationclass.G);
            model.Position.Should().Be(locationclass.P);
            model.Row.Should().Be(locationclass.R);
            model.Site.Should().Be(locationclass.St);
            model.Source.Should().Be(locationclass.Sc);
            model.Type.Should().Be(locationclass.T);
            model.Warehouse.Should().Be(locationclass.W);
            model.IsRack.Should().Be(locationclass.Ra);
        }
    }
}
