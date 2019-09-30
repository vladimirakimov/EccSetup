using AutoMapper;
using FluentAssertions;
using ITG.Brix.EccSetup.Application.Cqs.Commands.Definitions.OperatorActivity;
using ITG.Brix.EccSetup.Application.Cqs.Commands.Dtos;
using ITG.Brix.EccSetup.Application.Cqs.Commands.Handlers;
using ITG.Brix.EccSetup.Application.Resources;
using ITG.Brix.EccSetup.Application.Services.Json;
using ITG.Brix.EccSetup.Domain.Model.OperatorActivities;
using ITG.Brix.EccSetup.Domain.Repositories.OperatorActivities;
using ITG.Brix.EccSetup.Infrastructure.Providers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ITG.Brix.EccSetup.UnitTests.Application.Cqs.Commands.Handlers
{
    [TestClass]
    public class CreateOperatorActivityCommandHandlerTests
    {
        [TestMethod]
        public void ConstructorShouldSucceed()
        {
            //Arrange
            var operatorActivityWriteRepository = new Mock<IOperatorActivityWriteRepository>().Object;
            var versionProvider = new Mock<IVersionProvider>().Object;
            var identifierProvider = new Mock<IIdentifierProvider>().Object;
            var jsonService = new Mock<IJsonService<object>>().Object;
            var mapper = new Mock<IMapper>().Object;

            //Act
            var handler = new CreateOperatorActivityCommandHandler(operatorActivityWriteRepository, versionProvider, identifierProvider, jsonService, mapper);

            //Assert
            handler.Should().NotBeNull();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConstructorShouldFailWhenVersionProviderIsNull()
        {
            //Arrange
            var operatorActivityWriteRepository = new Mock<IOperatorActivityWriteRepository>().Object;
            IVersionProvider versionProvider = null;
            var identifierProvider = new Mock<IIdentifierProvider>().Object;
            var jsonService = new Mock<IJsonService<object>>().Object;
            var mapper = new Mock<IMapper>().Object;

            //Act
            var handler = new CreateOperatorActivityCommandHandler(operatorActivityWriteRepository, versionProvider, identifierProvider, jsonService, mapper);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConstructorShouldFailWhenIdentifierProviderIsNull()
        {
            //Arrange
            var operatorActivityWriteRepository = new Mock<IOperatorActivityWriteRepository>().Object;
            var versionProvider = new Mock<IVersionProvider>().Object;
            IIdentifierProvider identifierProvider = null;
            var jsonService = new Mock<IJsonService<object>>().Object;
            var mapper = new Mock<IMapper>().Object;

            //Act
            var handler = new CreateOperatorActivityCommandHandler(operatorActivityWriteRepository, versionProvider, identifierProvider, jsonService, mapper);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConstructorShouldFailWhenJsonServiceIsNull()
        {
            //Arrange
            var operatorActivityWriteRepository = new Mock<IOperatorActivityWriteRepository>().Object;
            var versionProvider = new Mock<IVersionProvider>().Object;
            var identifierProvider = new Mock<IIdentifierProvider>().Object;
            IJsonService<object> jsonService = null;
            var mapper = new Mock<IMapper>().Object;

            //Act
            var handler = new CreateOperatorActivityCommandHandler(operatorActivityWriteRepository, versionProvider, identifierProvider, jsonService, mapper);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConstructorShouldFailWhenOperatorActivityWriteRepositoryIsNull()
        {
            //Arrange
            IOperatorActivityWriteRepository operatorActivityWriteRepository = null;
            var versionProvider = new Mock<IVersionProvider>().Object;
            var identifierProvider = new Mock<IIdentifierProvider>().Object;
            var jsonService = new Mock<IJsonService<object>>().Object;
            var mapper = new Mock<IMapper>().Object;

            //Act
            var handler = new CreateOperatorActivityCommandHandler(operatorActivityWriteRepository, versionProvider, identifierProvider, jsonService, mapper);
        }

        [TestMethod]
        public async Task HandleShouldReturnOk()
        {
            //Arrange
            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(x => x.Map<OperatorActivity>(It.IsAny<object>())).Returns(new OperatorActivity());
            var mapper = mapperMock.Object;

            var jsonServiceMock = new Mock<IJsonService<object>>();
            jsonServiceMock.Setup(x => x.ToObject<List<OperatorActivityDto>>(It.IsAny<JToken>())).Returns(new List<OperatorActivityDto>());
            var jsonService = jsonServiceMock.Object;

            var versionProviderMock = new Mock<IVersionProvider>();
            versionProviderMock.Setup(x => x.Generate()).Returns(1);
            var versionProvider = versionProviderMock.Object;

            var identifierProviderMock = new Mock<IIdentifierProvider>();
            identifierProviderMock.Setup(x => x.Generate()).Returns(Guid.NewGuid());
            var identifierProvider = identifierProviderMock.Object;

            var writeRepositoryMock = new Mock<IOperatorActivityWriteRepository>();
            writeRepositoryMock.Setup(x => x.CreateAsync(It.IsAny<OperatorActivity>())).Returns(Task.FromResult(default(object)));
            var operatorActivityWriteRepository = writeRepositoryMock.Object;

            var activities = "{\r\n\t\"Activities\": [\r\n\t\t{\r\n\t\t\t\"Type\" : \"instruction\",\r\n\t\t\t\"Name\": \"Instruction\"\r\n\t\t},\r\n\t\t{\r\n\t\t\t\"Type\" : \"validation\",\r\n\t\t\t\"Name\": \"Validation\"\r\n\t\t}\r\n\t]\r\n}";

            var command = new CreateOperatorActivityCommand(JObject.Parse(activities));
            var handler = new CreateOperatorActivityCommandHandler(operatorActivityWriteRepository, versionProvider, identifierProvider, jsonService, mapper);

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            result.IsFailure.Should().BeFalse();
        }

        [TestMethod]
        public async Task HandleShouldFailWhenExceptionThrown()
        {
            //Arrange
            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(x => x.Map<OperatorActivity>(It.IsAny<object>())).Returns(new OperatorActivity());
            var mapper = mapperMock.Object;

            var jsonServiceMock = new Mock<IJsonService<object>>();
            jsonServiceMock.Setup(x => x.ToObject<object>(It.IsAny<JToken>())).Returns(new List<OperatorActivityDto>());
            var jsonService = jsonServiceMock.Object;

            var versionProviderMock = new Mock<IVersionProvider>();
            versionProviderMock.Setup(x => x.Generate()).Returns(1);
            var versionProvider = versionProviderMock.Object;

            var identifierProviderMock = new Mock<IIdentifierProvider>();
            identifierProviderMock.Setup(x => x.Generate()).Returns(Guid.NewGuid());
            var identifierProvider = identifierProviderMock.Object;

            var writeRepositoryMock = new Mock<IOperatorActivityWriteRepository>();
            writeRepositoryMock.Setup(x => x.CreateAsync(It.IsAny<OperatorActivity>())).Returns(Task.FromResult(default(object)));
            var operatorActivityWriteRepository = writeRepositoryMock.Object;

            var activities = "{\r\n\t\"Activities\": [\r\n\t\t{\r\n\t\t\t\"Type\" : \"instruction\",\r\n\t\t\t\"Name\": \"Instruction\"\r\n\t\t},\r\n\t\t{\r\n\t\t\t\"Type\" : \"validation\",\r\n\t\t\t\"Name\": \"Validation\"\r\n\t\t}\r\n\t]\r\n}";

            var command = new CreateOperatorActivityCommand(JObject.Parse(activities));
            var handler = new CreateOperatorActivityCommandHandler(operatorActivityWriteRepository, versionProvider, identifierProvider, jsonService, mapper);

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            result.IsFailure.Should().BeTrue();
            result.Failures.Should().Contain(x => x.Message == CustomFailures.CreateOperatorActivityFailure);
        }
    }
}
