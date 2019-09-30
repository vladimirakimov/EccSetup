using FluentAssertions;
using ITG.Brix.EccSetup.Domain;
using ITG.Brix.EccSetup.Domain.Model.Flows;
using ITG.Brix.EccSetup.Domain.Repositories;
using ITG.Brix.EccSetup.Infrastructure.ClassMaps;
using ITG.Brix.EccSetup.Infrastructure.Configurations.Impl;
using ITG.Brix.EccSetup.Infrastructure.Constants;
using ITG.Brix.EccSetup.Infrastructure.Exceptions;
using ITG.Brix.EccSetup.Infrastructure.Repositories;
using ITG.Brix.EccSetup.IntegrationTests.Infrastructure.Bases;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ITG.Brix.EccSetup.IntegrationTests.Infrastructure.Repositories
{
    [TestClass]
    [TestCategory("Integration")]
    public class FlowWriteRepositoryTests
    {
        private IFlowWriteRepository _repository;

        [ClassInitialize()]
        public static void ClassInitialize(TestContext testContext)
        {
            ClassMapsRegistrator.RegisterMaps();
        }

        [TestInitialize]
        public void TestInitialize()
        {
            RepositoryTestsHelper.Init(Consts.Collections.FlowCollectionName);
            _repository = new FlowWriteRepository(new PersistenceContext(new PersistenceConfiguration(RepositoryTestsHelper.ConnectionString)));
        }

        [TestMethod]
        public async Task CreateShouldSuccess()
        {
            // Arrange
            var id = Guid.NewGuid();
            var name = "name";
            var description = "description";
            var image = "image";
            var diagram = "diagram";
            var filterContent = "{\"sources\":[{\"id\":\"dcfe1db6-2484-42c2-9d9e-a77a28a5078d\",\"name\":\"BKAL33+KBT T\",\"description\":\"Plato Chemicals Test\",\"sourceBusinessUnits\":[]}],\"operations\":[{\"id\":\"d2760435-9d0b-4b69-adce-09017f2840c6\",\"name\":\"Unload into warehouse\",\"description\":\"Unloading goods into the warehouse\",\"icon\":null,\"tags\":[\"string\"]}]}";

            var flow = new Flow(id, name);
            flow.SetDescription(description);
            flow.SetImage(image);
            flow.SetDiagram(diagram);
            flow.SetFilterContent(filterContent);

            // Act
            await _repository.CreateAsync(flow);

            // Assert
            var data = RepositoryHelper.ForFlow.GetFlows();
            data.Should().HaveCount(1);
            var result = data.First();

            result.Name.Should().Be(name);
            result.Id.Should().Be(id);
            result.Description.Should().Be(description);
            result.Image.Should().Be(image);
            result.Diagram.Should().Be(diagram);
            result.FilterContent.Should().Be(filterContent);
        }

        [TestMethod]
        public void CreateWithAlreadyExistingNameShouldFail()
        {
            // Arrange
            var id = Guid.NewGuid();
            var name = "name";

            RepositoryHelper.ForFlow.CreateFlow(id, name);

            var otherId = Guid.NewGuid();
            var otherName = name;

            var flow = new Flow(otherId, otherName);

            // Act
            Action act = () => { _repository.CreateAsync(flow).GetAwaiter().GetResult(); };

            // Assert
            act.Should().Throw<UniqueKeyException>();
        }

        [TestMethod]
        public async Task UpdateShouldSucceed()
        {
            // Arrange
            var id = Guid.NewGuid();
            var name = "name";
            var filterContent = "{\"sources\":[{\"id\":\"dcfe1db6-2484-42c2-9d9e-a77a28a5078d\",\"name\":\"BKAL33+KBT T\",\"description\":\"Plato Chemicals Test\",\"sourceBusinessUnits\":[]}],\"operations\":[{\"id\":\"d2760435-9d0b-4b69-adce-09017f2840c6\",\"name\":\"Unload into warehouse\",\"description\":\"Unloading goods into the warehouse\",\"icon\":null,\"tags\":[\"string\"]}]}";
            var filter = JsonConvert.DeserializeObject<FlowFilter>(filterContent);

            var flow = RepositoryHelper.ForFlow.CreateFlow(id, name);
            var diagram = "diagram";
            flow.SetDiagram(diagram);
            flow.SetFilterContent(filterContent);
            flow.SetFilter(filter);


            // Act
            await _repository.UpdateAsync(flow);

            // Assert
            var data = RepositoryHelper.ForFlow.GetFlows();
            data.Should().HaveCount(1);
            var result = data.First();
            result.Should().NotBeNull();
            result.Diagram.Should().Be(diagram);
            result.Filter.Sources.Should().BeEquivalentTo(filter.Sources);
            result.Filter.Operations.Should().BeEquivalentTo(filter.Operations);
            result.Filter.Sites.Should().BeEquivalentTo(filter.Sites);
            result.Filter.OperationalDepartments.Should().BeEquivalentTo(filter.OperationalDepartments);
            result.Filter.TypePlannings.Should().BeEquivalentTo(filter.TypePlannings);
            result.Filter.Customers.Should().BeEquivalentTo(filter.Customers);
            result.Filter.ProductionSites.Should().BeEquivalentTo(filter.ProductionSites);
            result.Filter.TransportTypes.Should().BeEquivalentTo(filter.TransportTypes);
        }

        [TestMethod]
        public void UpdateWithAlreadyExistingNameShouldFail()
        {
            // Arrange
            var id = Guid.NewGuid();
            var name = "nameOne";

            RepositoryHelper.ForFlow.CreateFlow(id, name);


            var otherId = Guid.NewGuid();
            var otherName = "nameTwo";

            var other = RepositoryHelper.ForFlow.CreateFlow(otherId, otherName);

            other.ChangeName(name);

            // Act
            Action act = () => { _repository.UpdateAsync(other).GetAwaiter().GetResult(); };

            // Assert
            act.Should().Throw<UniqueKeyException>();
        }

        [TestMethod]
        public async Task DeleteShouldSucceed()
        {
            // Arrange
            var id = Guid.NewGuid();
            var name = "nameOne";

            RepositoryHelper.ForFlow.CreateFlow(id, name);

            // Act
            await _repository.DeleteAsync(id, 0);

            // Assert
            var data = RepositoryHelper.ForFlow.GetFlows();
            data.Should().HaveCount(0);
        }
    }
}
