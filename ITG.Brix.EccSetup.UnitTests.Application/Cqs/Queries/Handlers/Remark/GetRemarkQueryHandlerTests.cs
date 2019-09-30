using AutoMapper;
using FluentAssertions;
using ITG.Brix.EccSetup.Application.Bases;
using ITG.Brix.EccSetup.Application.Cqs.Queries.Definitions;
using ITG.Brix.EccSetup.Application.Cqs.Queries.Handlers;
using ITG.Brix.EccSetup.Application.Cqs.Queries.Models.BuildingBlocks.Remark;
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
    public class GetRemarkQueryHandlerTests
    {
        [TestMethod]
        public void ConstructorShouldSucceed()
        {
            // Arrange
            var mapper = new Mock<IMapper>().Object;
            var remarkReadRepository = new Mock<IRemarkReadRepository>().Object;

            // Act
            Action ctor = () =>
            {
                new GetRemarkQueryHandler(mapper, remarkReadRepository);
            };

            // Assert
            ctor.Should().NotThrow();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenMapperIsNull()
        {
            // Arrange
            IMapper mapper = null;
            var remarkReadRepository = new Mock<IRemarkReadRepository>().Object;

            // Act
            Action ctor = () =>
            {
                new GetRemarkQueryHandler(mapper, remarkReadRepository);
            };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenRemarkReadRepositoryIsNull()
        {
            // Arrange
            var mapper = new Mock<IMapper>().Object;
            IRemarkReadRepository remarkReadRepository = null;

            // Act
            Action ctor = () =>
            {
                new GetRemarkQueryHandler(mapper, remarkReadRepository);
            };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public async Task HandleShouldReturnOk()
        {
            // Arrange
            var id = Guid.NewGuid();
            var name = "name";
            var nameOnApplication = "nameOnApplication";
            string description = "description";
            var icon = new RemarkIcon(Guid.NewGuid());

            var remarkReadRepositoryMock = new Mock<IRemarkReadRepository>();
            remarkReadRepositoryMock.Setup(x => x.GetAsync(id)).Returns(Task.FromResult(new Remark(id, name, nameOnApplication, description, icon)));
            var remarkReadRepository = remarkReadRepositoryMock.Object;

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(x => x.Map<RemarkModel>(It.IsAny<object>())).Returns(new RemarkModel());
            var mapper = mapperMock.Object;

            var query = new GetRemarkQuery(id);

            var handler = new GetRemarkQueryHandler(mapper, remarkReadRepository);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsFailure.Should().BeFalse();
            result.Should().BeOfType(typeof(Result<RemarkModel>));
        }

        [TestMethod]
        public async Task HandleShouldReturnFailWhenNotFound()
        {
            // Arrange
            var id = Guid.NewGuid();

            var remarkReadRepositoryMock = new Mock<IRemarkReadRepository>();
            remarkReadRepositoryMock.Setup(x => x.GetAsync(id)).Throws<EntityNotFoundDbException>();
            var remarkReadRepository = remarkReadRepositoryMock.Object;

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(x => x.Map<RemarkModel>(It.IsAny<object>())).Returns(new RemarkModel());
            var mapper = mapperMock.Object;

            var query = new GetRemarkQuery(id);

            var handler = new GetRemarkQueryHandler(mapper, remarkReadRepository);

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

            var remarkReadRepositoryMock = new Mock<IRemarkReadRepository>();
            remarkReadRepositoryMock.Setup(x => x.GetAsync(id)).Throws<SomeDatabaseSpecificException>();
            var remarkReadRepository = remarkReadRepositoryMock.Object;

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(x => x.Map<RemarkModel>(It.IsAny<object>())).Returns(new RemarkModel());
            var mapper = mapperMock.Object;

            var query = new GetRemarkQuery(id);

            var handler = new GetRemarkQueryHandler(mapper, remarkReadRepository);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Failures.Should().OnlyContain(x => x.Message == CustomFailures.GetRemarkFailure);
        }

        public class SomeDatabaseSpecificException : Exception { }
    }
}
