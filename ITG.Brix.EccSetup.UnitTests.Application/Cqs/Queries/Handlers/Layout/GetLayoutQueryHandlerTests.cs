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
    public class GetLayoutQueryHandlerTests
    {
        [TestMethod]
        public void ConstructorShouldSucceed()
        {
            // Arrange
            var mapper = new Mock<IMapper>().Object;
            var layoutReadRepository = new Mock<ILayoutReadRepository>().Object;

            // Act
            Action ctor = () =>
            {
                new GetLayoutQueryHandler(mapper, layoutReadRepository);
            };

            // Assert
            ctor.Should().NotThrow();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenMapperIsNull()
        {
            // Arrange
            IMapper mapper = null;
            var layoutReadRepository = new Mock<ILayoutReadRepository>().Object;

            // Act
            Action ctor = () =>
            {
                new GetLayoutQueryHandler(mapper, layoutReadRepository);
            };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenLayoutReadRepositoryIsNull()
        {
            // Arrange
            var mapper = new Mock<IMapper>().Object;
            ILayoutReadRepository layoutReadRepository = null;

            // Act
            Action ctor = () =>
            {
                new GetLayoutQueryHandler(mapper, layoutReadRepository);
            };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public async Task HandleShouldReturnOk()
        {
            // Arrange
            var id = Guid.NewGuid();

            var layoutReadRepositoryMock = new Mock<ILayoutReadRepository>();
            layoutReadRepositoryMock.Setup(x => x.GetAsync(id)).Returns(Task.FromResult(new Layout(id, "name")));
            var layoutReadRepository = layoutReadRepositoryMock.Object;

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(x => x.Map<LayoutModel>(It.IsAny<object>())).Returns(new LayoutModel());
            var mapper = mapperMock.Object;

            var query = new GetLayoutQuery(id);

            var handler = new GetLayoutQueryHandler(mapper, layoutReadRepository);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsFailure.Should().BeFalse();
            result.Should().BeOfType(typeof(Result<LayoutModel>));
        }

        [TestMethod]
        public async Task HandleShouldReturnFailWhenNotFound()
        {
            // Arrange
            var id = Guid.NewGuid();

            var layoutReadRepositoryMock = new Mock<ILayoutReadRepository>();
            layoutReadRepositoryMock.Setup(x => x.GetAsync(id)).Throws<EntityNotFoundDbException>();
            var layoutReadRepository = layoutReadRepositoryMock.Object;

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(x => x.Map<LayoutModel>(It.IsAny<object>())).Returns(new LayoutModel());
            var mapper = mapperMock.Object;

            var query = new GetLayoutQuery(id);

            var handler = new GetLayoutQueryHandler(mapper, layoutReadRepository);

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

            var layoutReadRepositoryMock = new Mock<ILayoutReadRepository>();
            layoutReadRepositoryMock.Setup(x => x.GetAsync(id)).Throws<SomeDatabaseSpecificException>();
            var layoutReadRepository = layoutReadRepositoryMock.Object;

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(x => x.Map<LayoutModel>(It.IsAny<object>())).Returns(new LayoutModel());
            var mapper = mapperMock.Object;

            var query = new GetLayoutQuery(id);

            var handler = new GetLayoutQueryHandler(mapper, layoutReadRepository);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Failures.Should().OnlyContain(x => x.Message == CustomFailures.GetLayoutFailure);
        }

        public class SomeDatabaseSpecificException : Exception { }
    }
}
