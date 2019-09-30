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
    public class GetSourceQueryHandlerTests
    {
        [TestMethod]
        public void ConstructorShouldSucceed()
        {
            // Arrange
            var mapper = new Mock<IMapper>().Object;
            var sourceReadRepository = new Mock<ISourceReadRepository>().Object;

            // Act
            Action ctor = () =>
            {
                new GetSourceQueryHandler(mapper, sourceReadRepository);
            };

            // Assert
            ctor.Should().NotThrow();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenMapperIsNull()
        {
            // Arrange
            IMapper mapper = null;
            var sourceReadRepository = new Mock<ISourceReadRepository>().Object;

            // Act
            Action ctor = () =>
            {
                new GetSourceQueryHandler(mapper, sourceReadRepository);
            };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenSourceReadRepositoryIsNull()
        {
            // Arrange
            var mapper = new Mock<IMapper>().Object;
            ISourceReadRepository sourceReadRepository = null;

            // Act
            Action ctor = () =>
            {
                new GetSourceQueryHandler(mapper, sourceReadRepository);
            };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public async Task HandleShouldReturnOk()
        {
            // Arrange
            var id = Guid.NewGuid();

            var sourceReadRepositoryMock = new Mock<ISourceReadRepository>();
            sourceReadRepositoryMock.Setup(x => x.GetAsync(id)).Returns(Task.FromResult(new Source(id, "name", "description")));
            var sourceReadRepository = sourceReadRepositoryMock.Object;

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(x => x.Map<SourceModel>(It.IsAny<object>())).Returns(new SourceModel());
            var mapper = mapperMock.Object;

            var query = new GetSourceQuery(id);

            var handler = new GetSourceQueryHandler(mapper, sourceReadRepository);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsFailure.Should().BeFalse();
            result.Should().BeOfType(typeof(Result<SourceModel>));
        }

        [TestMethod]
        public async Task HandleShouldReturnFailWhenNotFound()
        {
            // Arrange
            var id = Guid.NewGuid();

            var sourceReadRepositoryMock = new Mock<ISourceReadRepository>();
            sourceReadRepositoryMock.Setup(x => x.GetAsync(id)).Throws<EntityNotFoundDbException>();
            var sourceReadRepository = sourceReadRepositoryMock.Object;

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(x => x.Map<SourceModel>(It.IsAny<object>())).Returns(new SourceModel());
            var mapper = mapperMock.Object;

            var query = new GetSourceQuery(id);

            var handler = new GetSourceQueryHandler(mapper, sourceReadRepository);

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

            var sourceReadRepositoryMock = new Mock<ISourceReadRepository>();
            sourceReadRepositoryMock.Setup(x => x.GetAsync(id)).Throws<SomeDatabaseSpecificException>();
            var sourceReadRepository = sourceReadRepositoryMock.Object;

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(x => x.Map<SourceModel>(It.IsAny<object>())).Returns(new SourceModel());
            var mapper = mapperMock.Object;

            var query = new GetSourceQuery(id);

            var handler = new GetSourceQueryHandler(mapper, sourceReadRepository);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Failures.Should().OnlyContain(x => x.Message == CustomFailures.GetSourceFailure);
        }

        public class SomeDatabaseSpecificException : Exception { }
    }
}
