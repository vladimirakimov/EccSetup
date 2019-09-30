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
    public class ListBusinessUnitQueryHandlerTests
    {
        [TestMethod]
        public void ConstructorShouldSucceed()
        {
            // Arrange
            var mapper = new Mock<IMapper>().Object;
            var businessUnitReadRepository = new Mock<IBusinessUnitReadRepository>().Object;
            var businessUnitOdataProvider = new Mock<IBusinessUnitOdataProvider>().Object;

            // Act
            Action ctor = () =>
            {
                new ListBusinessUnitQueryHandler(mapper, businessUnitReadRepository, businessUnitOdataProvider);
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
            var businessUnitOdataProvider = new Mock<IBusinessUnitOdataProvider>().Object;

            // Act
            Action ctor = () =>
            {
                new ListBusinessUnitQueryHandler(mapper, businessUnitReadRepository, businessUnitOdataProvider);
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
            var businessUnitOdataProvider = new Mock<IBusinessUnitOdataProvider>().Object;

            // Act
            Action ctor = () =>
            {
                new ListBusinessUnitQueryHandler(mapper, businessUnitReadRepository, businessUnitOdataProvider);
            };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenBusinessUnitOdataProviderIsNull()
        {
            // Arrange
            var mapper = new Mock<IMapper>().Object;
            var businessUnitReadRepository = new Mock<IBusinessUnitReadRepository>().Object;
            IBusinessUnitOdataProvider businessUnitOdataProvider = null;

            // Act
            Action ctor = () =>
            {
                new ListBusinessUnitQueryHandler(mapper, businessUnitReadRepository, businessUnitOdataProvider);
            };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public async Task HandleShouldReturnOk()
        {
            // Arrange
            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(x => x.Map<IEnumerable<BusinessUnitModel>>(It.IsAny<object>())).Returns(Enumerable.Empty<BusinessUnitModel>());
            var mapper = mapperMock.Object;

            var businessUnitReadRepositoryMock = new Mock<IBusinessUnitReadRepository>();
            businessUnitReadRepositoryMock.Setup(x => x.ListAsync(It.IsAny<Expression<Func<BusinessUnit, bool>>>(), It.IsAny<int?>(), It.IsAny<int?>())).Returns(Task.FromResult(Enumerable.Empty<BusinessUnit>()));
            var businessUnitReadRepository = businessUnitReadRepositoryMock.Object;

            var businessUnitOdataProviderMock = new Mock<IBusinessUnitOdataProvider>();
            businessUnitOdataProviderMock.Setup(x => x.GetFilterPredicate(It.IsAny<string>())).Returns((Expression<Func<BusinessUnit, bool>>)null);
            var businessUnitOdataProvider = businessUnitOdataProviderMock.Object;

            var query = new ListBusinessUnitQuery(null, null, null);

            var handler = new ListBusinessUnitQueryHandler(mapper, businessUnitReadRepository, businessUnitOdataProvider);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsFailure.Should().BeFalse();
            result.Should().BeOfType(typeof(Result<BusinessUnitsModel>));
        }

        [TestMethod]
        public async Task HandleShouldReturnFailWhenDatabaseSpecificErrorOccurs()
        {
            // Arrange
            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(x => x.Map<IEnumerable<BusinessUnitModel>>(It.IsAny<object>())).Returns(Enumerable.Empty<BusinessUnitModel>());
            var mapper = mapperMock.Object;

            var businessUnitReadRepositoryMock = new Mock<IBusinessUnitReadRepository>();
            businessUnitReadRepositoryMock.Setup(x => x.ListAsync(It.IsAny<Expression<Func<BusinessUnit, bool>>>(), It.IsAny<int?>(), It.IsAny<int?>())).Throws<SomeDatabaseSpecificException>();
            var businessUnitReadRepository = businessUnitReadRepositoryMock.Object;

            var businessUnitOdataProviderMock = new Mock<IBusinessUnitOdataProvider>();
            businessUnitOdataProviderMock.Setup(x => x.GetFilterPredicate(It.IsAny<string>())).Returns((Expression<Func<BusinessUnit, bool>>)null);
            var businessUnitOdataProvider = businessUnitOdataProviderMock.Object;

            var query = new ListBusinessUnitQuery(null, null, null);

            var handler = new ListBusinessUnitQueryHandler(mapper, businessUnitReadRepository, businessUnitOdataProvider);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Failures.Should().OnlyContain(x => x.Message == CustomFailures.ListBusinessUnitFailure);
        }

        public class SomeDatabaseSpecificException : Exception { }
    }
}
