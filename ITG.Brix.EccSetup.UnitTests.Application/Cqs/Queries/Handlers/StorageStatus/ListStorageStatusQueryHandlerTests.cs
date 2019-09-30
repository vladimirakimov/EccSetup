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
    public class ListStorageStatusQueryHandlerTests
    {
        [TestMethod]
        public void ConstructorShouldSucceed()
        {
            // Arrange
            var mapper = new Mock<IMapper>().Object;
            var storageStatusReadRepository = new Mock<IStorageStatusReadRepository>().Object;
            var storageStatusOdataProvider = new Mock<IStorageStatusOdataProvider>().Object;

            // Act
            Action ctor = () =>
            {
                new ListStorageStatusQueryHandler(mapper, storageStatusReadRepository, storageStatusOdataProvider);
            };

            // Assert
            ctor.Should().NotThrow();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenMapperIsNull()
        {
            // Arrange
            IMapper mapper = null;
            var storageStatusReadRepository = new Mock<IStorageStatusReadRepository>().Object;
            var storageStatusOdataProvider = new Mock<IStorageStatusOdataProvider>().Object;

            // Act
            Action ctor = () =>
            {
                new ListStorageStatusQueryHandler(mapper, storageStatusReadRepository, storageStatusOdataProvider);
            };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenStorageStatusReadRepositoryIsNull()
        {
            // Arrange
            var mapper = new Mock<IMapper>().Object;
            IStorageStatusReadRepository storageStatusReadRepository = null;
            var storageStatusOdataProvider = new Mock<IStorageStatusOdataProvider>().Object;

            // Act
            Action ctor = () =>
            {
                new ListStorageStatusQueryHandler(mapper, storageStatusReadRepository, storageStatusOdataProvider);
            };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenStorageStatusOdataProviderIsNull()
        {
            // Arrange
            var mapper = new Mock<IMapper>().Object;
            var storageStatusReadRepository = new Mock<IStorageStatusReadRepository>().Object;
            IStorageStatusOdataProvider storageStatusOdataProvider = null;

            // Act
            Action ctor = () =>
            {
                new ListStorageStatusQueryHandler(mapper, storageStatusReadRepository, storageStatusOdataProvider);
            };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public async Task HandleShouldReturnOk()
        {
            // Arrange
            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(x => x.Map<IEnumerable<StorageStatusModel>>(It.IsAny<object>())).Returns(Enumerable.Empty<StorageStatusModel>());
            var mapper = mapperMock.Object;

            var storageStatusReadRepositoryMock = new Mock<IStorageStatusReadRepository>();
            storageStatusReadRepositoryMock.Setup(x => x.ListAsync(It.IsAny<Expression<Func<StorageStatus, bool>>>(), It.IsAny<int?>(), It.IsAny<int?>())).Returns(Task.FromResult(Enumerable.Empty<StorageStatus>()));
            var storageStatusReadRepository = storageStatusReadRepositoryMock.Object;

            var storageStatusOdataProviderMock = new Mock<IStorageStatusOdataProvider>();
            storageStatusOdataProviderMock.Setup(x => x.GetFilterPredicate(It.IsAny<string>())).Returns((Expression<Func<StorageStatus, bool>>)null);
            var storageStatusOdataProvider = storageStatusOdataProviderMock.Object;

            var query = new ListStorageStatusQuery(null, null, null);

            var handler = new ListStorageStatusQueryHandler(mapper, storageStatusReadRepository, storageStatusOdataProvider);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsFailure.Should().BeFalse();
            result.Should().BeOfType(typeof(Result<StorageStatusesModel>));
        }

        [TestMethod]
        public async Task HandleShouldReturnFailWhenDatabaseSpecificErrorOccurs()
        {
            // Arrange
            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(x => x.Map<IEnumerable<StorageStatusModel>>(It.IsAny<object>())).Returns(Enumerable.Empty<StorageStatusModel>());
            var mapper = mapperMock.Object;

            var storageStatusReadRepositoryMock = new Mock<IStorageStatusReadRepository>();
            storageStatusReadRepositoryMock.Setup(x => x.ListAsync(It.IsAny<Expression<Func<StorageStatus, bool>>>(), It.IsAny<int?>(), It.IsAny<int?>())).Throws<SomeDatabaseSpecificException>();
            var storageStatusReadRepository = storageStatusReadRepositoryMock.Object;

            var storageStatusOdataProviderMock = new Mock<IStorageStatusOdataProvider>();
            storageStatusOdataProviderMock.Setup(x => x.GetFilterPredicate(It.IsAny<string>())).Returns((Expression<Func<StorageStatus, bool>>)null);
            var storageStatusOdataProvider = storageStatusOdataProviderMock.Object;

            var query = new ListStorageStatusQuery(null, null, null);

            var handler = new ListStorageStatusQueryHandler(mapper, storageStatusReadRepository, storageStatusOdataProvider);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Failures.Should().OnlyContain(x => x.Message == CustomFailures.ListStorageStatusFailure);
        }

        public class SomeDatabaseSpecificException : Exception { }
    }
}
