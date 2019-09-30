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
    public class ListSiteQueryHandlerTests
    {
        [TestMethod]
        public void ConstructorShouldSucceed()
        {
            // Arrange
            var mapper = new Mock<IMapper>().Object;
            var siteReadRepository = new Mock<ISiteReadRepository>().Object;
            var siteOdataProvider = new Mock<ISiteOdataProvider>().Object;

            // Act
            Action ctor = () =>
            {
                new ListSiteQueryHandler(mapper, siteReadRepository, siteOdataProvider);
            };

            // Assert
            ctor.Should().NotThrow();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenMapperIsNull()
        {
            // Arrange
            IMapper mapper = null;
            var siteReadRepository = new Mock<ISiteReadRepository>().Object;
            var siteOdataProvider = new Mock<ISiteOdataProvider>().Object;

            // Act
            Action ctor = () =>
            {
                new ListSiteQueryHandler(mapper, siteReadRepository, siteOdataProvider);
            };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenSiteReadRepositoryIsNull()
        {
            // Arrange
            var mapper = new Mock<IMapper>().Object;
            ISiteReadRepository siteReadRepository = null;
            var siteOdataProvider = new Mock<ISiteOdataProvider>().Object;

            // Act
            Action ctor = () =>
            {
                new ListSiteQueryHandler(mapper, siteReadRepository, siteOdataProvider);
            };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenSiteOdataProviderIsNull()
        {
            // Arrange
            var mapper = new Mock<IMapper>().Object;
            var siteReadRepository = new Mock<ISiteReadRepository>().Object;
            ISiteOdataProvider siteOdataProvider = null;

            // Act
            Action ctor = () =>
            {
                new ListSiteQueryHandler(mapper, siteReadRepository, siteOdataProvider);
            };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public async Task HandleShouldReturnOk()
        {
            // Arrange
            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(x => x.Map<IEnumerable<SiteModel>>(It.IsAny<object>())).Returns(Enumerable.Empty<SiteModel>());
            var mapper = mapperMock.Object;

            var siteReadRepositoryMock = new Mock<ISiteReadRepository>();
            siteReadRepositoryMock.Setup(x => x.ListAsync(It.IsAny<Expression<Func<Site, bool>>>(), It.IsAny<int?>(), It.IsAny<int?>())).Returns(Task.FromResult(Enumerable.Empty<Site>()));
            var siteReadRepository = siteReadRepositoryMock.Object;

            var siteOdataProviderMock = new Mock<ISiteOdataProvider>();
            siteOdataProviderMock.Setup(x => x.GetFilterPredicate(It.IsAny<string>())).Returns((Expression<Func<Site, bool>>)null);
            var SiteOdataProvider = siteOdataProviderMock.Object;

            var query = new ListSiteQuery(null, null, null);

            var handler = new ListSiteQueryHandler(mapper, siteReadRepository, SiteOdataProvider);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsFailure.Should().BeFalse();
            result.Should().BeOfType(typeof(Result<SitesModel>));
        }

        [TestMethod]
        public async Task HandleShouldReturnFailWhenDatabaseSpecificErrorOccurs()
        {
            // Arrange
            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(x => x.Map<IEnumerable<SiteModel>>(It.IsAny<object>())).Returns(Enumerable.Empty<SiteModel>());
            var mapper = mapperMock.Object;

            var siteReadRepositoryMock = new Mock<ISiteReadRepository>();
            siteReadRepositoryMock.Setup(x => x.ListAsync(It.IsAny<Expression<Func<Site, bool>>>(), It.IsAny<int?>(), It.IsAny<int?>())).Throws<SomeDatabaseSpecificException>();
            var siteReadRepository = siteReadRepositoryMock.Object;

            var siteOdataProviderMock = new Mock<ISiteOdataProvider>();
            siteOdataProviderMock.Setup(x => x.GetFilterPredicate(It.IsAny<string>())).Returns((Expression<Func<Site, bool>>)null);
            var siteOdataProvider = siteOdataProviderMock.Object;

            var query = new ListSiteQuery(null, null, null);

            var handler = new ListSiteQueryHandler(mapper, siteReadRepository, siteOdataProvider);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Failures.Should().OnlyContain(x => x.Message == CustomFailures.ListSiteFailure);
        }

        public class SomeDatabaseSpecificException : Exception { }
    }
}
