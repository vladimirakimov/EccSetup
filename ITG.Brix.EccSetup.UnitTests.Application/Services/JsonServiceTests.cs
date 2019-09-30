using FluentAssertions;
using ITG.Brix.EccSetup.Application.Services.Json.Impl;
using ITG.Brix.EccSetup.Domain;
using ITG.Brix.EccSetup.Domain.Model.Flows;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace ITG.Brix.EccSetup.UnitTests.Application.Services
{
    [TestClass]
    public class JsonServiceTests
    {
        [TestMethod]
        public void DeserializeMethodShouldReturnRightResultObject()
        {
            //Arrange
            var jsonToDeserialize = "{\"sources\":[{\"id\":\"dcfe1db6-2484-42c2-9d9e-a77a28a5078d\",\"name\":\"BKAL33+KBT T\",\"description\":\"Plato Chemicals Test\",\"sourceBusinessUnits\":[]}],\"operations\":[{\"id\":\"d2760435-9d0b-4b69-adce-09017f2840c6\",\"name\":\"Unload into warehouse\",\"description\":\"Unloading goods into the warehouse\",\"icon\":null,\"tags\":[\"string\"]}]}";
            var jsonService = new JsonService();
            var flowFilter = new FlowFilter(new List<FlowSource> {new FlowSource("BKAL33+KBT T")},
                                            new List<FlowOperation> { new FlowOperation("Unload into warehouse") },
                                            new List<FlowSite>(),
                                            new List<FlowOperationalDepartment>(),
                                            new List<FlowTypePlanning>(),
                                            new List<FlowCustomer>(),
                                            new List<FlowProductionSite>(),
                                            new List<FlowTransportType>(),
                                            "No");
            //Act
            var result = jsonService.Deserialize<FlowFilter>(jsonToDeserialize);

            //Assert
            result.Sources.Should().BeEquivalentTo(flowFilter.Sources);
            result.Operations.Should().BeEquivalentTo(flowFilter.Operations);
        }
    }
}
