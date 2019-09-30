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
    public class GetBusinessUnitQueryHandlerTests
    {
        [TestMethod]
        public void ConstructorShouldSucceed()
        {
            // Arrange
            var mapper = new Mock<IMapper>().Object;
            var businessUnitReadRepository = new Mock<IBusinessUnitReadRepository>().Object;

            // Act
            Action ctor = () =>
            {
                new GetBusinessUnitQueryHandler(mapper, businessUnitReadRepository);
            };

            // Assert
            ctor.Should().NotThrow();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenMapperIsNull()
        {
            // Arrange
            IMapper mapper = null;
            var businessUnitReadRepository = new Mock<IBusinessUnitReadRepository>().Object;

            // Act
            Action ctor = () =>
            {
                new GetBusinessUnitQueryHandler(mapper, businessUnitReadRepository);
            };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenBusinessUnitReadRepositoryIsNull()
        {
            // Arrange
            var mapper = new Mock<IMapper>().Object;
            IBusinessUnitReadRepository businessUnitReadRepository = null;

            // Act
            Action ctor = () =>
            {
                new GetBusinessUnitQueryHandler(mapper, businessUnitReadRepository);
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

            var businessUnitReadRepositoryMock = new Mock<IBusinessUnitReadRepository>();
            businessUnitReadRepositoryMock.Setup(x => x.GetAsync(id)).Returns(Task.FromResult(new BusinessUnit(id, name)));
            var businessUnitReadRepository = businessUnitReadRepositoryMock.Object;

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(x => x.Map<BusinessUnitModel>(It.IsAny<object>())).Returns(new BusinessUnitModel());
            var mapper = mapperMock.Object;

            var query = new GetBusinessUnitQuery(id);

            var handler = new GetBusinessUnitQueryHandler(mapper, businessUnitReadRepository);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsFailure.Should().BeFalse();
            result.Should().BeOfType(typeof(Result<BusinessUnitModel>));
        }

        [TestMethod]
        public async Task HandleShouldReturnFailWhenNotFound()
        {
            // Arrange
            var id = Guid.NewGuid();

            var businessUnitReadRepositoryMock = new Mock<IBusinessUnitReadRepository>();
            businessUnitReadRepositoryMock.Setup(x => x.GetAsync(id)).Throws<EntityNotFoundDbException>();
            var businessUnitReadRepository = businessUnitReadRepositoryMock.Object;

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(x => x.Map<BusinessUnitModel>(It.IsAny<object>())).Returns(new BusinessUnitModel());
            var mapper = mapperMock.Object;

            var query = new GetBusinessUnitQuery(id);

            var handler = new GetBusinessUnitQueryHandler(mapper, businessUnitReadRepository);

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

            var businessUnitReadRepositoryMock = new Mock<IBusinessUnitReadRepository>();
            businessUnitReadRepositoryMock.Setup(x => x.GetAsync(id)).Throws<SomeDatabaseSpecificException>();
            var businessUnitReadRepository = businessUnitReadRepositoryMock.Object;

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(x => x.Map<BusinessUnitModel>(It.IsAny<object>())).Returns(new BusinessUnitModel());
            var mapper = mapperMock.Object;

            var query = new GetBusinessUnitQuery(id);

            var handler = new GetBusinessUnitQueryHandler(mapper, businessUnitReadRepository);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Failures.Should().OnlyContain(x => x.Message == CustomFailures.GetBusinessUnitFailure);
        }

        public class SomeDatabaseSpecificException : Exception { }
    }
}
