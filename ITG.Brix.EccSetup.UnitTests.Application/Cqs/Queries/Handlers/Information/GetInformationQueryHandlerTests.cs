using AutoMapper;
using FluentAssertions;
using ITG.Brix.EccSetup.Application.Bases;
using ITG.Brix.EccSetup.Application.Cqs.Queries.Definitions;
using ITG.Brix.EccSetup.Application.Cqs.Queries.Handlers;
using ITG.Brix.EccSetup.Application.Cqs.Queries.Models.BuildingBlocks;
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
    public class GetInformationQueryHandlerTests
    {
        [TestMethod]
        public void ConstructorShouldSucceed()
        {
            // Arrange
            var mapper = new Mock<IMapper>().Object;
            var informationReadRepository = new Mock<IInformationReadRepository>().Object;

            // Act
            Action ctor = () =>
            {
                new GetInformationQueryHandler(mapper, informationReadRepository);
            };

            // Assert
            ctor.Should().NotThrow();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenMapperIsNull()
        {
            // Arrange
            IMapper mapper = null;
            var informationReadRepository = new Mock<IInformationReadRepository>().Object;

            // Act
            Action ctor = () =>
            {
                new GetInformationQueryHandler(mapper, informationReadRepository);
            };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenInformationReadRepositoryIsNull()
        {
            // Arrange
            var mapper = new Mock<IMapper>().Object;
            IInformationReadRepository informationReadRepository = null;

            // Act
            Action ctor = () =>
            {
                new GetInformationQueryHandler(mapper, informationReadRepository);
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
            string nameOnApplication = "nameOnApplication";
            var description = "description";
            var icon = Guid.NewGuid();

            var informationReadRepositoryMock = new Mock<IInformationReadRepository>();
            informationReadRepositoryMock.Setup(x => x.GetAsync(id)).Returns(Task.FromResult(new Information(id, name, nameOnApplication, description, icon)));
            var informationReadRepository = informationReadRepositoryMock.Object;

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(x => x.Map<InformationModel>(It.IsAny<object>())).Returns(new InformationModel());
            var mapper = mapperMock.Object;

            var query = new GetInformationQuery(id);

            var handler = new GetInformationQueryHandler(mapper, informationReadRepository);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsFailure.Should().BeFalse();
            result.Should().BeOfType(typeof(Result<InformationModel>));
        }

        [TestMethod]
        public async Task HandleShouldReturnFailWhenNotFound()
        {
            // Arrange
            var id = Guid.NewGuid();

            var informationReadRepositoryMock = new Mock<IInformationReadRepository>();
            informationReadRepositoryMock.Setup(x => x.GetAsync(id)).Throws<EntityNotFoundDbException>();
            var informationReadRepository = informationReadRepositoryMock.Object;

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(x => x.Map<InformationModel>(It.IsAny<object>())).Returns(new InformationModel());
            var mapper = mapperMock.Object;

            var query = new GetInformationQuery(id);

            var handler = new GetInformationQueryHandler(mapper, informationReadRepository);

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

            var informationReadRepositoryMock = new Mock<IInformationReadRepository>();
            informationReadRepositoryMock.Setup(x => x.GetAsync(id)).Throws<SomeDatabaseSpecificException>();
            var informationReadRepository = informationReadRepositoryMock.Object;

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(x => x.Map<InformationModel>(It.IsAny<object>())).Returns(new InformationModel());
            var mapper = mapperMock.Object;

            var query = new GetInformationQuery(id);

            var handler = new GetInformationQueryHandler(mapper, informationReadRepository);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Failures.Should().OnlyContain(x => x.Message == CustomFailures.GetInformationFailure);
        }

        public class SomeDatabaseSpecificException : Exception { }
    }
}
