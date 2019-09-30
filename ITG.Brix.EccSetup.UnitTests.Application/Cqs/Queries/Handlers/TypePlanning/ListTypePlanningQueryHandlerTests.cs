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
    public class ListTypePlanningQueryHandlerTests
    {
        [TestMethod]
        public void ConstructorShouldSucceed()
        {
            // Arrange
            var mapper = new Mock<IMapper>().Object;
            var typePlanningReadRepository = new Mock<ITypePlanningReadRepository>().Object;
            var typePlanningOdataProvider = new Mock<ITypePlanningOdataProvider>().Object;

            // Act
            Action ctor = () =>
            {
                new ListTypePlanningQueryHandler(mapper, typePlanningReadRepository, typePlanningOdataProvider);
            };

            // Assert
            ctor.Should().NotThrow();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenMapperIsNull()
        {
            // Arrange
            IMapper mapper = null;
            var typePlanningReadRepository = new Mock<ITypePlanningReadRepository>().Object;
            var typePlanningOdataProvider = new Mock<ITypePlanningOdataProvider>().Object;

            // Act
            Action ctor = () =>
            {
                new ListTypePlanningQueryHandler(mapper, typePlanningReadRepository, typePlanningOdataProvider);
            };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenTypePlanningReadRepositoryIsNull()
        {
            // Arrange
            var mapper = new Mock<IMapper>().Object;
            ITypePlanningReadRepository typePlanningReadRepository = null;
            var typePlanningOdataProvider = new Mock<ITypePlanningOdataProvider>().Object;

            // Act
            Action ctor = () =>
            {
                new ListTypePlanningQueryHandler(mapper, typePlanningReadRepository, typePlanningOdataProvider);
            };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenTypePlanningOdataProviderIsNull()
        {
            // Arrange
            var mapper = new Mock<IMapper>().Object;
            var typePlanningReadRepository = new Mock<ITypePlanningReadRepository>().Object;
            ITypePlanningOdataProvider typePlanningOdataProvider = null;

            // Act
            Action ctor = () =>
            {
                new ListTypePlanningQueryHandler(mapper, typePlanningReadRepository, typePlanningOdataProvider);
            };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public async Task HandleShouldReturnOk()
        {
            // Arrange
            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(x => x.Map<IEnumerable<TypePlanningModel>>(It.IsAny<object>())).Returns(Enumerable.Empty<TypePlanningModel>());
            var mapper = mapperMock.Object;

            var typePlanningReadRepositoryMock = new Mock<ITypePlanningReadRepository>();
            typePlanningReadRepositoryMock.Setup(x => x.ListAsync(It.IsAny<Expression<Func<TypePlanning, bool>>>(), It.IsAny<int?>(), It.IsAny<int?>())).Returns(Task.FromResult(Enumerable.Empty<TypePlanning>()));
            var typePlanningReadRepository = typePlanningReadRepositoryMock.Object;

            var typePlanningOdataProviderMock = new Mock<ITypePlanningOdataProvider>();
            typePlanningOdataProviderMock.Setup(x => x.GetFilterPredicate(It.IsAny<string>())).Returns((Expression<Func<TypePlanning, bool>>)null);
            var typePlanningOdataProvider = typePlanningOdataProviderMock.Object;

            var query = new ListTypePlanningQuery(null, null, null);

            var handler = new ListTypePlanningQueryHandler(mapper, typePlanningReadRepository, typePlanningOdataProvider);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsFailure.Should().BeFalse();
            result.Should().BeOfType(typeof(Result<TypePlanningsModel>));
        }

        [TestMethod]
        public async Task HandleShouldReturnFailWhenDatabaseSpecificErrorOccurs()
        {
            // Arrange
            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(x => x.Map<IEnumerable<TypePlanningModel>>(It.IsAny<object>())).Returns(Enumerable.Empty<TypePlanningModel>());
            var mapper = mapperMock.Object;

            var typePlanningReadRepositoryMock = new Mock<ITypePlanningReadRepository>();
            typePlanningReadRepositoryMock.Setup(x => x.ListAsync(It.IsAny<Expression<Func<TypePlanning, bool>>>(), It.IsAny<int?>(), It.IsAny<int?>())).Throws<SomeDatabaseSpecificException>();
            var typePlanningReadRepository = typePlanningReadRepositoryMock.Object;

            var typePlanningOdataProviderMock = new Mock<ITypePlanningOdataProvider>();
            typePlanningOdataProviderMock.Setup(x => x.GetFilterPredicate(It.IsAny<string>())).Returns((Expression<Func<TypePlanning, bool>>)null);
            var typePlanningOdataProvider = typePlanningOdataProviderMock.Object;

            var query = new ListTypePlanningQuery(null, null, null);

            var handler = new ListTypePlanningQueryHandler(mapper, typePlanningReadRepository, typePlanningOdataProvider);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Failures.Should().OnlyContain(x => x.Message == CustomFailures.ListTypePlanningFailure);
        }

        public class SomeDatabaseSpecificException : Exception { }
    }
}
