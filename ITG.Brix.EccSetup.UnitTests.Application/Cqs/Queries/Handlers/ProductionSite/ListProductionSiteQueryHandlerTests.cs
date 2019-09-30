using AutoMapper;
using FluentAssertions;
using ITG.Brix.EccSetup.Application.Bases;
using ITG.Brix.EccSetup.Application.Cqs.Queries.Definitions;
using ITG.Brix.EccSetup.Application.Cqs.Queries.Handlers;
using ITG.Brix.EccSetup.Application.Cqs.Queries.Models;
using ITG.Brix.EccSetup.Application.Resources;
using ITG.Brix.EccSetup.Domain;
using ITG.Brix.EccSetup.Domain.Repositories;
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
    public class ListProductionSiteQueryHandlerTests
    {
        [TestMethod]
        public void ConstructorShouldSucceed()
        {
            // Arrange
            var mapper = new Mock<IMapper>().Object;
            var productionSiteReadRepository = new Mock<IProductionSiteReadRepository>().Object;
            var productionSiteOdataProvider = new Mock<IProductionSiteOdataProvider>().Object;

            // Act
            Action ctor = () =>
            {
                new ListProductionSiteQueryHandler(mapper, productionSiteReadRepository, productionSiteOdataProvider);
            };

            // Assert
            ctor.Should().NotThrow();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenMapperIsNull()
        {
            // Arrange
            IMapper mapper = null;
            var productionSiteReadRepository = new Mock<IProductionSiteReadRepository>().Object;
            var productionSiteOdataProvider = new Mock<IProductionSiteOdataProvider>().Object;

            // Act
            Action ctor = () =>
            {
                new ListProductionSiteQueryHandler(mapper, productionSiteReadRepository, productionSiteOdataProvider);
            };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenProductionSiteReadRepositoryIsNull()
        {
            // Arrange
            var mapper = new Mock<IMapper>().Object;
            IProductionSiteReadRepository productionSiteReadRepository = null;
            var productionSiteOdataProvider = new Mock<IProductionSiteOdataProvider>().Object;

            // Act
            Action ctor = () =>
            {
                new ListProductionSiteQueryHandler(mapper, productionSiteReadRepository, productionSiteOdataProvider);
            };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenProductionSiteOdataProviderIsNull()
        {
            // Arrange
            var mapper = new Mock<IMapper>().Object;
            var productionSiteReadRepository = new Mock<IProductionSiteReadRepository>().Object;
            IProductionSiteOdataProvider productionSiteOdataProvider = null;

            // Act
            Action ctor = () =>
            {
                new ListProductionSiteQueryHandler(mapper, productionSiteReadRepository, productionSiteOdataProvider);
            };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public async Task HandleShouldReturnOk()
        {
            // Arrange
            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(x => x.Map<IEnumerable<ProductionSiteModel>>(It.IsAny<object>())).Returns(Enumerable.Empty<ProductionSiteModel>());
            var mapper = mapperMock.Object;

            var productionSiteReadRepositoryMock = new Mock<IProductionSiteReadRepository>();
            productionSiteReadRepositoryMock.Setup(x => x.ListAsync(It.IsAny<Expression<Func<ProductionSite, bool>>>(), It.IsAny<int?>(), It.IsAny<int?>())).Returns(Task.FromResult(Enumerable.Empty<ProductionSite>()));
            var productionSiteReadRepository = productionSiteReadRepositoryMock.Object;

            var productionSiteOdataProviderMock = new Mock<IProductionSiteOdataProvider>();
            productionSiteOdataProviderMock.Setup(x => x.GetFilterPredicate(It.IsAny<string>())).Returns((Expression<Func<ProductionSite, bool>>)null);
            var productionSiteOdataProvider = productionSiteOdataProviderMock.Object;

            var query = new ListProductionSiteQuery(null, null, null);

            var handler = new ListProductionSiteQueryHandler(mapper, productionSiteReadRepository, productionSiteOdataProvider);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsFailure.Should().BeFalse();
            result.Should().BeOfType(typeof(Result<ProductionSitesModel>));
        }

        [TestMethod]
        public async Task HandleShouldReturnFailWhenDatabaseSpecificErrorOccurs()
        {
            // Arrange
            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(x => x.Map<IEnumerable<ProductionSiteModel>>(It.IsAny<object>())).Returns(Enumerable.Empty<ProductionSiteModel>());
            var mapper = mapperMock.Object;

            var productionSiteReadRepositoryMock = new Mock<IProductionSiteReadRepository>();
            productionSiteReadRepositoryMock.Setup(x => x.ListAsync(It.IsAny<Expression<Func<ProductionSite, bool>>>(), It.IsAny<int?>(), It.IsAny<int?>())).Throws<SomeDatabaseSpecificException>();
            var productionSiteReadRepository = productionSiteReadRepositoryMock.Object;

            var productionSiteOdataProviderMock = new Mock<IProductionSiteOdataProvider>();
            productionSiteOdataProviderMock.Setup(x => x.GetFilterPredicate(It.IsAny<string>())).Returns((Expression<Func<ProductionSite, bool>>)null);
            var productionSiteOdataProvider = productionSiteOdataProviderMock.Object;

            var query = new ListProductionSiteQuery(null, null, null);

            var handler = new ListProductionSiteQueryHandler(mapper, productionSiteReadRepository, productionSiteOdataProvider);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Failures.Should().OnlyContain(x => x.Message == CustomFailures.ListProductionSiteFailure);
        }

        public class SomeDatabaseSpecificException : Exception { }
    }
}
