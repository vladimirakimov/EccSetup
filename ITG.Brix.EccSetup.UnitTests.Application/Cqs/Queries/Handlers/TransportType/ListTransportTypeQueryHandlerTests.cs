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
    public class ListTransportTypeQueryHandlerTests
    {
        [TestMethod]
        public void ConstructorShouldSucceed()
        {
            // Arrange
            var mapper = new Mock<IMapper>().Object;
            var transportTypeReadRepository = new Mock<ITransportTypeReadRepository>().Object;
            var transportTypeOdataProvider = new Mock<ITransportTypeOdataProvider>().Object;

            // Act
            Action ctor = () =>
            {
                new ListTransportTypeQueryHandler(mapper, transportTypeReadRepository, transportTypeOdataProvider);
            };

            // Assert
            ctor.Should().NotThrow();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenMapperIsNull()
        {
            // Arrange
            IMapper mapper = null;
            var transportTypeReadRepository = new Mock<ITransportTypeReadRepository>().Object;
            var transportTypeOdataProvider = new Mock<ITransportTypeOdataProvider>().Object;

            // Act
            Action ctor = () =>
            {
                new ListTransportTypeQueryHandler(mapper, transportTypeReadRepository, transportTypeOdataProvider);
            };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenTransportTypeReadRepositoryIsNull()
        {
            // Arrange
            var mapper = new Mock<IMapper>().Object;
            ITransportTypeReadRepository TransportTypeReadRepository = null;
            var transportTypeOdataProvider = new Mock<ITransportTypeOdataProvider>().Object;

            // Act
            Action ctor = () =>
            {
                new ListTransportTypeQueryHandler(mapper, TransportTypeReadRepository, transportTypeOdataProvider);
            };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenTransportTypeOdataProviderIsNull()
        {
            // Arrange
            var mapper = new Mock<IMapper>().Object;
            var transportTypeReadRepository = new Mock<ITransportTypeReadRepository>().Object;
            ITransportTypeOdataProvider transportTypeOdataProvider = null;

            // Act
            Action ctor = () =>
            {
                new ListTransportTypeQueryHandler(mapper, transportTypeReadRepository, transportTypeOdataProvider);
            };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public async Task HandleShouldReturnOk()
        {
            // Arrange
            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(x => x.Map<IEnumerable<TransportTypeModel>>(It.IsAny<object>())).Returns(Enumerable.Empty<TransportTypeModel>());
            var mapper = mapperMock.Object;

            var transportTypeReadRepositoryMock = new Mock<ITransportTypeReadRepository>();
            transportTypeReadRepositoryMock.Setup(x => x.ListAsync(It.IsAny<Expression<Func<TransportType, bool>>>(), It.IsAny<int?>(), It.IsAny<int?>())).Returns(Task.FromResult(Enumerable.Empty<TransportType>()));
            var transportTypeReadRepository = transportTypeReadRepositoryMock.Object;

            var transportTypeOdataProviderMock = new Mock<ITransportTypeOdataProvider>();
            transportTypeOdataProviderMock.Setup(x => x.GetFilterPredicate(It.IsAny<string>())).Returns((Expression<Func<TransportType, bool>>)null);
            var transportTypeOdataProvider = transportTypeOdataProviderMock.Object;

            var query = new ListTransportTypeQuery(null, null, null);

            var handler = new ListTransportTypeQueryHandler(mapper, transportTypeReadRepository, transportTypeOdataProvider);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsFailure.Should().BeFalse();
            result.Should().BeOfType(typeof(Result<TransportTypesModel>));
        }

        [TestMethod]
        public async Task HandleShouldReturnFailWhenDatabaseSpecificErrorOccurs()
        {
            // Arrange
            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(x => x.Map<IEnumerable<TransportTypeModel>>(It.IsAny<object>())).Returns(Enumerable.Empty<TransportTypeModel>());
            var mapper = mapperMock.Object;

            var transportTypeReadRepositoryMock = new Mock<ITransportTypeReadRepository>();
            transportTypeReadRepositoryMock.Setup(x => x.ListAsync(It.IsAny<Expression<Func<TransportType, bool>>>(), It.IsAny<int?>(), It.IsAny<int?>())).Throws<SomeDatabaseSpecificException>();
            var TransportTypeReadRepository = transportTypeReadRepositoryMock.Object;

            var transportTypeOdataProviderMock = new Mock<ITransportTypeOdataProvider>();
            transportTypeOdataProviderMock.Setup(x => x.GetFilterPredicate(It.IsAny<string>())).Returns((Expression<Func<TransportType, bool>>)null);
            var transportTypeOdataProvider = transportTypeOdataProviderMock.Object;

            var query = new ListTransportTypeQuery(null, null, null);

            var handler = new ListTransportTypeQueryHandler(mapper, TransportTypeReadRepository, transportTypeOdataProvider);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Failures.Should().OnlyContain(x => x.Message == CustomFailures.ListTransportTypeFailure);
        }

        public class SomeDatabaseSpecificException : Exception { }
    }
}
