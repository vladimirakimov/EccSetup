using FluentAssertions;
using ITG.Brix.EccSetup.Application.Bases;
using ITG.Brix.EccSetup.Application.Cqs.Commands.Definitions;
using ITG.Brix.EccSetup.Application.Cqs.Commands.Handlers;
using ITG.Brix.EccSetup.Application.DataTypes;
using ITG.Brix.EccSetup.Application.Services.Json;
using ITG.Brix.EccSetup.Domain;
using ITG.Brix.EccSetup.Domain.Model.Flows;
using ITG.Brix.EccSetup.Domain.Repositories;
using ITG.Brix.EccSetup.Infrastructure.Providers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ITG.Brix.EccSetup.UnitTests.Application.Cqs.Commands.Handlers
{
    [TestClass]
    public class UpdateFlowCommandHandlerTests
    {
        [TestMethod]
        public void ConstructorShouldSucceed()
        {
            // Arrange
            var flowWriteRepository = new Mock<IFlowWriteRepository>().Object;
            var flowReadRepository = new Mock<IFlowReadRepository>().Object;
            var versionProvider = new Mock<IVersionProvider>().Object;
            var jsonService = new Mock<IJsonService<object>>().Object;

            // Act
            Action ctor = () =>
            {
                new UpdateFlowCommandHandler(flowWriteRepository,
                                             flowReadRepository,
                                             versionProvider,
                                             jsonService);
            };

            // Assert
            ctor.Should().NotThrow();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenFlowWriteRepositoryIsNull()
        {
            // Arrange
            IFlowWriteRepository flowWriteRepository = null;
            var flowReadRepository = new Mock<IFlowReadRepository>().Object;
            var versionProvider = new Mock<IVersionProvider>().Object;
            var jsonService = new Mock<IJsonService<object>>().Object;

            // Act
            Action ctor = () =>
            {
                new UpdateFlowCommandHandler(flowWriteRepository,
                                             flowReadRepository,
                                             versionProvider,
                                             jsonService);
            };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenFlowReadRepositoryIsNull()
        {
            // Arrange
            var flowWriteRepository = new Mock<IFlowWriteRepository>().Object;
            IFlowReadRepository flowReadRepository = null;
            var versionProvider = new Mock<IVersionProvider>().Object;
            var jsonService = new Mock<IJsonService<object>>().Object;

            // Act
            Action ctor = () =>
            {
                new UpdateFlowCommandHandler(flowWriteRepository,
                                             flowReadRepository,
                                             versionProvider,
                                             jsonService);
            };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenVersionProviderIsNull()
        {
            // Arrange
            var flowWriteRepository = new Mock<IFlowWriteRepository>().Object;
            var flowReadRepository = new Mock<IFlowReadRepository>().Object;
            IVersionProvider versionProvider = null;
            var jsonService = new Mock<IJsonService<object>>().Object;

            // Act
            Action ctor = () =>
            {
                new UpdateFlowCommandHandler(flowWriteRepository,
                                             flowReadRepository,
                                             versionProvider,
                                             jsonService);
            };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public async Task HandleShouldReturnOk()
        {
            //Arrange
            var id = Guid.NewGuid();
            Optional<string> name = new Optional<string>("FlowUpdate");
            Optional<string> diagram = new Optional<string>("diagram");
            Optional<string> filterContent = new Optional<string>("{\"sources\":[{\"id\":\"dcfe1db6-2484-42c2-9d9e-a77a28a5078d\",\"name\":\"BKAL33+KBT T\",\"description\":\"Plato Chemicals Test\",\"sourceBusinessUnits\":[]}],\"operations\":[{\"id\":\"d2760435-9d0b-4b69-adce-09017f2840c6\",\"name\":\"Unload into warehouse\",\"description\":\"Unloading goods into the warehouse\",\"icon\":null,\"tags\":[\"string\"]}]}");
            Optional<string> description = new Optional<string>("description");
            Optional<string> image = new Optional<string>("image");
            var flowFilter = JsonConvert.DeserializeObject<FlowFilter>(filterContent.Value);
            var flow = new Flow(id, name.Value) { Version = 1 };
            var version = 1;

            var flowWriteRepositoryMock = new Mock<IFlowWriteRepository>();
            flowWriteRepositoryMock.Setup(x=>x.UpdateAsync(It.IsAny<Flow>())).Returns(Task.CompletedTask);
            var flowWriteRepo = flowWriteRepositoryMock.Object;

            var flowReadRepositoryMock = new Mock<IFlowReadRepository>();
            flowReadRepositoryMock.Setup(x=>x.GetAsync(id)).Returns(Task.FromResult(flow));
            var flowReadRepo = flowReadRepositoryMock.Object;

            var versionProviderMock = new Mock<IVersionProvider>();
            versionProviderMock.Setup(x=>x.Generate()).Returns(version);
            var versionProvider = versionProviderMock.Object;

            var jsonServiceMock = new Mock<IJsonService<object>>();
            jsonServiceMock.Setup(x=>x.Deserialize<FlowFilter>(filterContent.Value)).Returns(flowFilter);
            var jsonService = jsonServiceMock.Object;           
           

            var command = new UpdateFlowCommand(id,
                                                name,
                                                description,
                                                image,
                                                diagram,
                                                filterContent,
                                                version
                                                );

            var handler = new UpdateFlowCommandHandler(flowWriteRepo,
                                                       flowReadRepo,
                                                       versionProvider,
                                                       jsonService);

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            result.IsFailure.Should().BeFalse();
            result.Should().BeOfType(typeof(Result));
        }
    }
}
