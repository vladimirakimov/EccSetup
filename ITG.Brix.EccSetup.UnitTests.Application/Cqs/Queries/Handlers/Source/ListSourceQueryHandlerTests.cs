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
    public class ListSourceQueryHandlerTests
    {
        [TestMethod]
        public void ConstructorShouldSucceed()
        {
            // Arrange
            var mapper = new Mock<IMapper>().Object;
            var sourceReadRepository = new Mock<ISourceReadRepository>().Object;
            var sourceOdataProvider = new Mock<ISourceOdataProvider>().Object;

            // Act
            Action ctor = () =>
            {
                new ListSourceQueryHandler(mapper, sourceReadRepository, sourceOdataProvider);
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
            var sourceOdataProvider = new Mock<ISourceOdataProvider>().Object;

            // Act
            Action ctor = () =>
            {
                new ListSourceQueryHandler(mapper, sourceReadRepository, sourceOdataProvider);
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
            var sourceOdataProvider = new Mock<ISourceOdataProvider>().Object;

            // Act
            Action ctor = () =>
            {
                new ListSourceQueryHandler(mapper, sourceReadRepository, sourceOdataProvider);
            };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenSourceOdataProviderIsNull()
        {
            // Arrange
            var mapper = new Mock<IMapper>().Object;
            var sourceReadRepository = new Mock<ISourceReadRepository>().Object;
            ISourceOdataProvider sourceOdataProvider = null;

            // Act
            Action ctor = () =>
            {
                new ListSourceQueryHandler(mapper, sourceReadRepository, sourceOdataProvider);
            };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public async Task HandleShouldReturnOk()
        {
            // Arrange
            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(x => x.Map<IEnumerable<SourceModel>>(It.IsAny<object>())).Returns(Enumerable.Empty<SourceModel>());
            var mapper = mapperMock.Object;

            var sourceReadRepositoryMock = new Mock<ISourceReadRepository>();
            sourceReadRepositoryMock.Setup(x => x.ListAsync(It.IsAny<Expression<Func<Source, bool>>>(), It.IsAny<int?>(), It.IsAny<int?>())).Returns(Task.FromResult(Enumerable.Empty<Source>()));
            var sourceReadRepository = sourceReadRepositoryMock.Object;

            var sourceOdataProviderMock = new Mock<ISourceOdataProvider>();
            sourceOdataProviderMock.Setup(x => x.GetFilterPredicate(It.IsAny<string>())).Returns((Expression<Func<Source, bool>>)null);
            var sourceOdataProvider = sourceOdataProviderMock.Object;

            var query = new ListSourceQuery(null, null, null);

            var handler = new ListSourceQueryHandler(mapper, sourceReadRepository, sourceOdataProvider);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsFailure.Should().BeFalse();
            result.Should().BeOfType(typeof(Result<SourcesModel>));
        }

        [TestMethod]
        public async Task HandleShouldReturnFailWhenFilterOdataErrorOccurs()
        {
            // Arrange
            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(x => x.Map<IEnumerable<FlowModel>>(It.IsAny<object>())).Returns(Enumerable.Empty<FlowModel>());
            var mapper = mapperMock.Object;

            var sourceReadRepositoryMock = new Mock<ISourceReadRepository>();
            sourceReadRepositoryMock.Setup(x => x.ListAsync(It.IsAny<Expression<Func<Source, bool>>>(), It.IsAny<int?>(), It.IsAny<int?>())).Returns(Task.FromResult(Enumerable.Empty<Source>()));
            var sourceReadRepository = sourceReadRepositoryMock.Object;

            var sourceOdataProviderMock = new Mock<ISourceOdataProvider>();
            sourceOdataProviderMock.Setup(x => x.GetFilterPredicate(It.IsAny<string>())).Throws<FilterODataException>();
            var sourceOdataProvider = sourceOdataProviderMock.Object;

            var query = new ListSourceQuery(null, null, null);

            var handler = new ListSourceQueryHandler(mapper, sourceReadRepository, sourceOdataProvider);

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
            mapperMock.Setup(x => x.Map<IEnumerable<SourceModel>>(It.IsAny<object>())).Returns(Enumerable.Empty<SourceModel>());
            var mapper = mapperMock.Object;

            var sourceReadRepositoryMock = new Mock<ISourceReadRepository>();
            sourceReadRepositoryMock.Setup(x => x.ListAsync(It.IsAny<Expression<Func<Source, bool>>>(), It.IsAny<int?>(), It.IsAny<int?>())).Throws<SomeDatabaseSpecificException>();
            var sourceReadRepository = sourceReadRepositoryMock.Object;

            var sourceOdataProviderMock = new Mock<ISourceOdataProvider>();
            sourceOdataProviderMock.Setup(x => x.GetFilterPredicate(It.IsAny<string>())).Returns((Expression<Func<Source, bool>>)null);
            var sourceOdataProvider = sourceOdataProviderMock.Object;


            var query = new ListSourceQuery(null, null, null);

            var handler = new ListSourceQueryHandler(mapper, sourceReadRepository, sourceOdataProvider);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Failures.Should().OnlyContain(x => x.Message == CustomFailures.ListSourceFailure);
        }

        public class SomeDatabaseSpecificException : Exception { }
    }
}
