using AutoMapper;
using FluentAssertions;
using ITG.Brix.EccSetup.Application.Bases;
using ITG.Brix.EccSetup.Application.Cqs.Queries.Definitions;
using ITG.Brix.EccSetup.Application.Cqs.Queries.Handlers;
using ITG.Brix.EccSetup.Application.Cqs.Queries.Models;
using ITG.Brix.EccSetup.Application.Resources;
using ITG.Brix.EccSetup.Domain;
using ITG.Brix.EccSetup.Domain.Repositories;
using ITG.Brix.EccSetup.Infrastructure.DataAccess.ClassModels;
using ITG.Brix.EccSetup.Infrastructure.Providers.Impl;
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
    public class ListLocationQueryHandlerTests
    {
        [TestMethod]
        public void ConstructorShouldSucceed()
        {
            // Arrange
            var mapper = new Mock<IMapper>().Object;
            var locationReadRepository = new Mock<ILocationReadRepository>().Object;
            var locationOdataProvider = new Mock<ILocationOdataProvider>().Object;

            // Act
            Action ctor = () =>
            {
                new ListLocationQueryHandler(mapper, locationReadRepository, locationOdataProvider);
            };

            // Assert
            ctor.Should().NotThrow();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenMapperIsNull()
        {
            // Arrange
            IMapper mapper = null;
            var locationReadRepository = new Mock<ILocationReadRepository>().Object;
            var locationOdataProvider = new Mock<ILocationOdataProvider>().Object;

            // Act
            Action ctor = () =>
            {
                new ListLocationQueryHandler(mapper, locationReadRepository, locationOdataProvider);
            };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenLocationReadRepositoryIsNull()
        {
            // Arrange
            var mapper = new Mock<IMapper>().Object;
            ILocationReadRepository locationReadRepository = null;
            var locationOdataProvider = new Mock<ILocationOdataProvider>().Object;

            // Act
            Action ctor = () =>
            {
                new ListLocationQueryHandler(mapper, locationReadRepository, locationOdataProvider);
            };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenLocationOdataProviderIsNull()
        {
            // Arrange
            var mapper = new Mock<IMapper>().Object;
            var locationReadRepository = new Mock<ILocationReadRepository>().Object;
            ILocationOdataProvider locationOdataProvider = null;

            // Act
            Action ctor = () =>
            {
                new ListLocationQueryHandler(mapper, locationReadRepository, locationOdataProvider);
            };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public async Task HandleShouldReturnOk()
        {
            // Arrange
            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(x => x.Map<IEnumerable<LocationModel>>(It.IsAny<object>())).Returns(Enumerable.Empty<LocationModel>());
            var mapper = mapperMock.Object;

            var locationReadRepositoryMock = new Mock<ILocationReadRepository>();
            locationReadRepositoryMock.Setup(x => x.ListAsync(It.IsAny<string>(), It.IsAny<int?>(), It.IsAny<int?>())).Returns(Task.FromResult(Enumerable.Empty<Location>()));
            var locationReadRepository = locationReadRepositoryMock.Object;

            var locationOdataProviderMock = new Mock<ILocationOdataProvider>();
            locationOdataProviderMock.Setup(x => x.GetFilterPredicate(It.IsAny<string>())).Returns((Expression<Func<LocationClass, bool>>)null);
            var LocationOdataProvider = locationOdataProviderMock.Object;

            var query = new ListLocationQuery(null, null, null);

            var handler = new ListLocationQueryHandler(mapper, locationReadRepository, LocationOdataProvider);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsFailure.Should().BeFalse();
            result.Should().BeOfType(typeof(Result<LocationsModel>));
        }

        [TestMethod]
        public async Task HandleShouldReturnFailWhenDatabaseSpecificErrorOccurs()
        {
            // Arrange
            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(x => x.Map<IEnumerable<LocationModel>>(It.IsAny<object>())).Returns(Enumerable.Empty<LocationModel>());
            var mapper = mapperMock.Object;

            var locationReadRepositoryMock = new Mock<ILocationReadRepository>();
            locationReadRepositoryMock.Setup(x => x.ListAsync(It.IsAny<string>(), It.IsAny<int?>(), It.IsAny<int?>())).Throws<SomeDatabaseSpecificException>();
            var LocationReadRepository = locationReadRepositoryMock.Object;

            var locationOdataProviderMock = new Mock<ILocationOdataProvider>();
            locationOdataProviderMock.Setup(x => x.GetFilterPredicate(It.IsAny<string>())).Returns((Expression<Func<LocationClass, bool>>)null);
            var LocationOdataProvider = locationOdataProviderMock.Object;

            var query = new ListLocationQuery(null, null, null);

            var handler = new ListLocationQueryHandler(mapper, LocationReadRepository, LocationOdataProvider);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Failures.Should().OnlyContain(x => x.Message == CustomFailures.ListLocationFailure);
        }

        public class SomeDatabaseSpecificException : Exception { }
    }
}
