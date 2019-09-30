using AutoMapper;
using FluentAssertions;
using ITG.Brix.EccSetup.Application.Cqs.Queries.Models;
using ITG.Brix.EccSetup.Application.MappingProfiles;
using ITG.Brix.EccSetup.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ITG.Brix.EccSetup.IntegrationTests.Application.MappingProfiles
{
    [TestClass]
    public class DomainProfileTests
    {
        [AssemblyInitialize()]
        public static void ClassInit(TestContext context)
        {
            Mapper.Initialize(m => m.AddProfile<DomainProfile>());
        }

        [TestMethod]
        public void AutoMapperConfigurationShouldBeValid()
        {
            Mapper.AssertConfigurationIsValid();
        }

        [TestMethod]
        public void LayoutToLayoutModelShouldMapCorrectly()
        {
            // Arrange
            var id = Guid.NewGuid();
            var name = "name";
            var description = "description";
            var image = "image";
            var diagram = "diagram";
            var layout = new Layout(id, name);
            layout.SetDescription(description);
            layout.SetImage(image);
            layout.SetDiagram(diagram);

            // Act
            var layoutModel = Mapper.Map<LayoutModel>(layout);

            // Assert
            layoutModel.Should().NotBeNull();
            layoutModel.Id.Should().Be(id);
            layoutModel.Name.Should().Be(name);
            layoutModel.Description.Should().Be(description);
            layoutModel.Image.Should().Be(image);
            layoutModel.Diagram.Should().Be(diagram);
        }
    }
}
