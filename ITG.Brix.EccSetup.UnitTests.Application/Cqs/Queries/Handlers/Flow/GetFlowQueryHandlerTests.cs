using AutoMapper;
using FluentAssertions;
using ITG.Brix.EccSetup.Application.Bases;
using ITG.Brix.EccSetup.Application.Cqs.Queries.Definitions;
using ITG.Brix.EccSetup.Application.Cqs.Queries.Handlers;
using ITG.Brix.EccSetup.Application.Cqs.Queries.Models;
using ITG.Brix.EccSetup.Application.Resources;
using ITG.Brix.EccSetup.Domain;
using ITG.Brix.EccSetup.Domain.Repositories;
using ITG.Brix.EccSetup.Infrastructure.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ITG.Brix.EccSetup.UnitTests.Application.Cqs.Queries.Handlers
{
    [TestClass]
    public class GetFlowQueryHandlerTests
    {
        [TestMethod]
        public void ConstructorShouldSucceed()
        {
            // Arrange
            var mapper = new Mock<IMapper>().Object;
            var flowReadRepository = new Mock<IFlowReadRepository>().Object;

            // Act
            Action ctor = () =>
            {
                new GetFlowQueryHandler(mapper, flowReadRepository);
            };

            // Assert
            ctor.Should().NotThrow();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenMapperIsNull()
        {
            // Arrange
            IMapper mapper = null;
            var flowReadRepository = new Mock<IFlowReadRepository>().Object;

            // Act
            Action ctor = () =>
            {
                new GetFlowQueryHandler(mapper, flowReadRepository);
            };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenFlowReadRepositoryIsNull()
        {
            // Arrange
            var mapper = new Mock<IMapper>().Object;
            IFlowReadRepository flowReadRepository = null;

            // Act
            Action ctor = () =>
            {
                new GetFlowQueryHandler(mapper, flowReadRepository);
            };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public async Task HandleShouldReturnOk()
        {
            // Arrange
            var id = Guid.NewGuid();
            string name = "name";

            var flowReadRepositoryMock = new Mock<IFlowReadRepository>();
            flowReadRepositoryMock.Setup(x => x.GetAsync(id)).Returns(Task.FromResult(new Flow(id, name)));
            var flowReadRepository = flowReadRepositoryMock.Object;

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(x => x.Map<FlowModel>(It.IsAny<object>())).Returns(new FlowModel());
            var mapper = mapperMock.Object;

            var query = new GetFlowQuery(id);

            var handler = new GetFlowQueryHandler(mapper, flowReadRepository);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsFailure.Should().BeFalse();
            result.Should().BeOfType(typeof(Result<FlowModel>));
        }

        [TestMethod]
        public async Task HandleShouldReturnFailWhenNotFound()
        {
            // Arrange
            var id = Guid.NewGuid();

            var flowReadRepositoryMock = new Mock<IFlowReadRepository>();
            flowReadRepositoryMock.Setup(x => x.GetAsync(id)).Throws<EntityNotFoundDbException>();
            var flowReadRepository = flowReadRepositoryMock.Object;

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(x => x.Map<FlowModel>(It.IsAny<object>())).Returns(new FlowModel());
            var mapper = mapperMock.Object;

            var query = new GetFlowQuery(id);

            var handler = new GetFlowQueryHandler(mapper, flowReadRepository);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Failures.Should().OnlyContain(x => x.Code == HandlerFaultCode.NotFound.Name);
        }

        [TestMethod]
        public async Task HandleShouldReturnFailWhenDatabaseSpecificErrorOccurs()
        {
            // Arrange
            var id = Guid.NewGuid();

            var flowReadRepositoryMock = new Mock<IFlowReadRepository>();
            flowReadRepositoryMock.Setup(x => x.GetAsync(id)).Throws<SomeDatabaseSpecificException>();
            var flowReadRepository = flowReadRepositoryMock.Object;

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(x => x.Map<FlowModel>(It.IsAny<object>())).Returns(new FlowModel());
            var mapper = mapperMock.Object;

            var query = new GetFlowQuery(id);

            var handler = new GetFlowQueryHandler(mapper, flowReadRepository);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Failures.Should().OnlyContain(x => x.Message == CustomFailures.GetFlowFailure);
        }

        public class SomeDatabaseSpecificException : Exception { }
    }
}
