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
    public class ListOperationQueryHandlerTests
    {

        [TestMethod]
        public void ConstructorShouldSucceed()
        {
            // Arrange
            var mapper = new Mock<IMapper>().Object;
            var operationReadRepository = new Mock<IOperationReadRepository>().Object;
            var iconReadRepository = new Mock<IIconReadRepository>().Object;
            var operationOdataProvider = new Mock<IOperationOdataProvider>().Object;

            // Act
            Action ctor = () =>
            {
                new ListOperationQueryHandler(mapper, operationReadRepository, iconReadRepository, operationOdataProvider);
            };

            // Assert
            ctor.Should().NotThrow();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenMapperIsNull()
        {
            // Arrange
            IMapper mapper = null;
            var operationReadRepository = new Mock<IOperationReadRepository>().Object;
            var iconReadRepository = new Mock<IIconReadRepository>().Object;
            var operationOdataProvider = new Mock<IOperationOdataProvider>().Object;

            // Act
            Action ctor = () =>
            {
                new ListOperationQueryHandler(mapper, operationReadRepository, iconReadRepository, operationOdataProvider);
            };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenOperationReadRepositoryIsNull()
        {
            // Arrange
            var mapper = new Mock<IMapper>().Object;
            IOperationReadRepository operationReadRepository = null;
            var iconReadRepository = new Mock<IIconReadRepository>().Object;
            var operationOdataProvider = new Mock<IOperationOdataProvider>().Object;

            // Act
            Action ctor = () =>
            {
                new ListOperationQueryHandler(mapper, operationReadRepository, iconReadRepository, operationOdataProvider);
            };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenIconReadRepositoryIsNull()
        {
            // Arrange
            var mapper = new Mock<IMapper>().Object;
            var operationReadRepository = new Mock<IOperationReadRepository>().Object;
            IIconReadRepository iconReadRepository = null;
            var operationOdataProvider = new Mock<IOperationOdataProvider>().Object;

            // Act
            Action ctor = () =>
            {
                new ListOperationQueryHandler(mapper, operationReadRepository, iconReadRepository, operationOdataProvider);
            };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenOperationOdataProviderIsNull()
        {
            // Arrange
            var mapper = new Mock<IMapper>().Object;
            var operationReadRepository = new Mock<IOperationReadRepository>().Object;
            var iconReadRepository = new Mock<IIconReadRepository>().Object;
            IOperationOdataProvider operationOdataProvider = null;

            // Act
            Action ctor = () =>
            {
                new ListOperationQueryHandler(mapper, operationReadRepository, iconReadRepository, operationOdataProvider);
            };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public async Task HandleShouldReturnOk()
        {
            // Arrange
            var operationReadRepositoryMock = new Mock<IOperationReadRepository>();
            operationReadRepositoryMock.Setup(x => x.ListAsync(It.IsAny<Expression<Func<Operation, bool>>>(), It.IsAny<int?>(), It.IsAny<int?>())).Returns(Task.FromResult(Enumerable.Empty<Operation>()));
            var operationReadRepository = operationReadRepositoryMock.Object;

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(x => x.Map<IEnumerable<OperationModel>>(It.IsAny<object>())).Returns(Enumerable.Empty<OperationModel>());
            var mapper = mapperMock.Object;

            var iconReadRepositoryMock = new Mock<IIconReadRepository>();
            iconReadRepositoryMock.Setup(x => x.GetAsync(It.IsAny<Guid>())).Returns(Task.FromResult<Icon>(null));
            var iconReadRepository = iconReadRepositoryMock.Object;

            var operationOdataProviderMock = new Mock<IOperationOdataProvider>();
            operationOdataProviderMock.Setup(x => x.GetFilterPredicate(It.IsAny<string>())).Returns((Expression<Func<Operation, bool>>)null);
            var operationOdataProvider = operationOdataProviderMock.Object;

            var query = new ListOperationQuery(null, null, null);

            var handler = new ListOperationQueryHandler(mapper, operationReadRepository, iconReadRepository, operationOdataProvider);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsFailure.Should().BeFalse();
            result.Should().BeOfType(typeof(Result<OperationsModel>));
        }

        [TestMethod]
        public async Task HandleShouldReturnFailWhenFilterOdataErrorOccurs()
        {
            // Arrange
            var operationReadRepositoryMock = new Mock<IOperationReadRepository>();
            operationReadRepositoryMock.Setup(x => x.ListAsync(It.IsAny<Expression<Func<Operation, bool>>>(), It.IsAny<int?>(), It.IsAny<int?>())).Returns(Task.FromResult(Enumerable.Empty<Operation>()));
            var operationReadRepository = operationReadRepositoryMock.Object;

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(x => x.Map<IEnumerable<OperationModel>>(It.IsAny<object>())).Returns(Enumerable.Empty<OperationModel>());
            var mapper = mapperMock.Object;

            var iconReadRepositoryMock = new Mock<IIconReadRepository>();
            iconReadRepositoryMock.Setup(x => x.GetAsync(It.IsAny<Guid>())).Returns(Task.FromResult<Icon>(null));
            var iconReadRepository = iconReadRepositoryMock.Object;

            var operationOdataProviderMock = new Mock<IOperationOdataProvider>();
            operationOdataProviderMock.Setup(x => x.GetFilterPredicate(It.IsAny<string>())).Throws<FilterODataException>();
            var operationOdataProvider = operationOdataProviderMock.Object;

            var query = new ListOperationQuery(null, null, null);

            var handler = new ListOperationQueryHandler(mapper, operationReadRepository, iconReadRepository, operationOdataProvider);

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
            var operationReadRepositoryMock = new Mock<IOperationReadRepository>();
            operationReadRepositoryMock.Setup(x => x.ListAsync(It.IsAny<Expression<Func<Operation, bool>>>(), It.IsAny<int?>(), It.IsAny<int?>())).Throws<SomeDatabaseSpecificException>();
            var operationReadRepository = operationReadRepositoryMock.Object;

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(x => x.Map<IEnumerable<OperationModel>>(It.IsAny<object>())).Returns(Enumerable.Empty<OperationModel>());
            var mapper = mapperMock.Object;

            var iconReadRepositoryMock = new Mock<IIconReadRepository>();
            iconReadRepositoryMock.Setup(x => x.GetAsync(It.IsAny<Guid>())).Returns(Task.FromResult<Icon>(null));
            var iconReadRepository = iconReadRepositoryMock.Object;

            var operationOdataProviderMock = new Mock<IOperationOdataProvider>();
            operationOdataProviderMock.Setup(x => x.GetFilterPredicate(It.IsAny<string>())).Returns((Expression<Func<Operation, bool>>)null);
            var operationOdataProvider = operationOdataProviderMock.Object;

            var query = new ListOperationQuery(null, null, null);

            var handler = new ListOperationQueryHandler(mapper, operationReadRepository, iconReadRepository, operationOdataProvider);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Failures.Should().OnlyContain(x => x.Message == CustomFailures.ListOperationFailure);
        }

        public class SomeDatabaseSpecificException : Exception { }
    }
}
