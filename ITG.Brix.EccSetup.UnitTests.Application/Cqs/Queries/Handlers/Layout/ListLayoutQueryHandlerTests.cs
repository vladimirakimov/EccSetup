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
using ITG.Brix.EccSetup.Infrastructure.Providers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace ITG.Brix.EccSetup.UnitTests.Application.Cqs.Queries.Handlers
{
    [TestClass]
    public class ListLayoutQueryHandlerTests
    {
        [TestMethod]
        public void ConstructorShouldSucceed()
        {
            // Arrange
            var mapper = new Mock<IMapper>().Object;
            var layoutReadRepository = new Mock<ILayoutReadRepository>().Object;
            var layoutOdataProvider = new Mock<ILayoutOdataProvider>().Object;

            // Act
            Action ctor = () =>
            {
                new ListLayoutQueryHandler(mapper, layoutReadRepository, layoutOdataProvider);
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
            var layoutOdataProvider = new Mock<ILayoutOdataProvider>().Object;

            // Act
            Action ctor = () =>
            {
                new ListLayoutQueryHandler(mapper, layoutReadRepository, layoutOdataProvider);
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
            var layoutOdataProvider = new Mock<ILayoutOdataProvider>().Object;

            // Act
            Action ctor = () =>
            {
                new ListLayoutQueryHandler(mapper, layoutReadRepository, layoutOdataProvider);
            };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenLayoutOdataProviderIsNull()
        {
            // Arrange
            var mapper = new Mock<IMapper>().Object;
            var layoutReadRepository = new Mock<ILayoutReadRepository>().Object;
            ILayoutOdataProvider layoutOdataProvider = null;

            // Act
            Action ctor = () =>
            {
                new ListLayoutQueryHandler(mapper, layoutReadRepository, layoutOdataProvider);
            };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public async Task HandleShouldReturnOk()
        {
            // Arrange
            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(x => x.Map<IEnumerable<LayoutModel>>(It.IsAny<object>())).Returns(Enumerable.Empty<LayoutModel>());
            var mapper = mapperMock.Object;

            var layoutReadRepositoryMock = new Mock<ILayoutReadRepository>();
            layoutReadRepositoryMock.Setup(x => x.ListAsync(It.IsAny<Expression<Func<Layout, bool>>>(), It.IsAny<int?>(), It.IsAny<int?>())).Returns(Task.FromResult(Enumerable.Empty<Layout>()));
            var layoutReadRepository = layoutReadRepositoryMock.Object;

            var layoutOdataProviderMock = new Mock<ILayoutOdataProvider>();
            layoutOdataProviderMock.Setup(x => x.GetFilterPredicate(It.IsAny<string>())).Returns((Expression<Func<Layout, bool>>)null);
            var layoutOdataProvider = layoutOdataProviderMock.Object;

            var query = new ListLayoutQuery(null, null, null);

            var handler = new ListLayoutQueryHandler(mapper, layoutReadRepository, layoutOdataProvider);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsFailure.Should().BeFalse();
            result.Should().BeOfType(typeof(Result<LayoutsModel>));
        }

        [TestMethod]
        public async Task HandleShouldReturnFailWhenFilterOdataErrorOccurs()
        {
            // Arrange
            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(x => x.Map<IEnumerable<FlowModel>>(It.IsAny<object>())).Returns(Enumerable.Empty<FlowModel>());
            var mapper = mapperMock.Object;

            var layoutReadRepositoryMock = new Mock<ILayoutReadRepository>();
            layoutReadRepositoryMock.Setup(x => x.ListAsync(It.IsAny<Expression<Func<Layout, bool>>>(), It.IsAny<int?>(), It.IsAny<int?>())).Returns(Task.FromResult(Enumerable.Empty<Layout>()));
            var layoutReadRepository = layoutReadRepositoryMock.Object;

            var layoutOdataProviderMock = new Mock<ILayoutOdataProvider>();
            layoutOdataProviderMock.Setup(x => x.GetFilterPredicate(It.IsAny<string>())).Throws<FilterODataException>();
            var layoutOdataProvider = layoutOdataProviderMock.Object;

            var query = new ListLayoutQuery(null, null, null);

            var handler = new ListLayoutQueryHandler(mapper, layoutReadRepository, layoutOdataProvider);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Failures.Should().OnlyContain(x => x.Code == HandlerFaultCode.InvalidQueryFilter.Name &&
                                                      x.Message == HandlerFailures.InvalidQueryFilter &&
                                                      x.Target == "$filter");
        }

        [TestMethod]
        public async Task HandleShouldReturnFailWhenDatabaseSpecificErrorOccurs()
        {
            // Arrange
            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(x => x.Map<IEnumerable<LayoutModel>>(It.IsAny<object>())).Returns(Enumerable.Empty<LayoutModel>());
            var mapper = mapperMock.Object;

            var layoutReadRepositoryMock = new Mock<ILayoutReadRepository>();
            layoutReadRepositoryMock.Setup(x => x.ListAsync(It.IsAny<Expression<Func<Layout, bool>>>(), It.IsAny<int?>(), It.IsAny<int?>())).Throws<SomeDatabaseSpecificException>();
            var layoutReadRepository = layoutReadRepositoryMock.Object;

            var layoutOdataProviderMock = new Mock<ILayoutOdataProvider>();
            layoutOdataProviderMock.Setup(x => x.GetFilterPredicate(It.IsAny<string>())).Returns((Expression<Func<Layout, bool>>)null);
            var layoutOdataProvider = layoutOdataProviderMock.Object;

            var query = new ListLayoutQuery(null, null, null);

            var handler = new ListLayoutQueryHandler(mapper, layoutReadRepository, layoutOdataProvider);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Failures.Should().OnlyContain(x => x.Message == CustomFailures.ListLayoutFailure);
        }

        public class SomeDatabaseSpecificException : Exception { }
    }
}
