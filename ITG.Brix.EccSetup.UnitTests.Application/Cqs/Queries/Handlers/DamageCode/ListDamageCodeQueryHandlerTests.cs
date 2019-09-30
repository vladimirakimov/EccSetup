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
    public class ListDamageCodeQueryHandlerTests
    {
        [TestMethod]
        public void ConstructorShouldSucceed()
        {
            // Arrange
            var mapper = new Mock<IMapper>().Object;
            var damageCodeReadRepository = new Mock<IDamageCodeReadRepository>().Object;
            var damageCodeOdataProvider = new Mock<IDamageCodeOdataProvider>().Object;

            // Act
            Action ctor = () =>
            {
                new ListDamageCodeQueryHandler(mapper, damageCodeReadRepository, damageCodeOdataProvider);
            };

            // Assert
            ctor.Should().NotThrow();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenMapperIsNull()
        {
            // Arrange
            IMapper mapper = null;
            var damageCodeReadRepository = new Mock<IDamageCodeReadRepository>().Object;
            var damageCodeOdataProvider = new Mock<IDamageCodeOdataProvider>().Object;

            // Act
            Action ctor = () =>
            {
                new ListDamageCodeQueryHandler(mapper, damageCodeReadRepository, damageCodeOdataProvider);
            };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenDamageCodeReadRepositoryIsNull()
        {
            // Arrange
            var mapper = new Mock<IMapper>().Object;
            IDamageCodeReadRepository damageCodeReadRepository = null;
            var damageCodeOdataProvider = new Mock<IDamageCodeOdataProvider>().Object;

            // Act
            Action ctor = () =>
            {
                new ListDamageCodeQueryHandler(mapper, damageCodeReadRepository, damageCodeOdataProvider);
            };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenDamageCodeOdataProviderIsNull()
        {
            // Arrange
            var mapper = new Mock<IMapper>().Object;
            var damageCodeReadRepository = new Mock<IDamageCodeReadRepository>().Object;
            IDamageCodeOdataProvider damageCodeOdataProvider = null;

            // Act
            Action ctor = () =>
            {
                new ListDamageCodeQueryHandler(mapper, damageCodeReadRepository, damageCodeOdataProvider);
            };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public async Task HandleShouldReturnOk()
        {
            // Arrange
            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(x => x.Map<IEnumerable<DamageCodeModel>>(It.IsAny<object>())).Returns(Enumerable.Empty<DamageCodeModel>());
            var mapper = mapperMock.Object;

            var damageCodeReadRepositoryMock = new Mock<IDamageCodeReadRepository>();
            damageCodeReadRepositoryMock.Setup(x => x.ListAsync(It.IsAny<Expression<Func<DamageCode, bool>>>(), It.IsAny<int?>(), It.IsAny<int?>())).Returns(Task.FromResult(Enumerable.Empty<DamageCode>()));
            var damageCodeReadRepository = damageCodeReadRepositoryMock.Object;

            var damageCodeOdataProviderMock = new Mock<IDamageCodeOdataProvider>();
            damageCodeOdataProviderMock.Setup(x => x.GetFilterPredicate(It.IsAny<string>())).Returns((Expression<Func<DamageCode, bool>>)null);
            var damageCodeOdataProvider = damageCodeOdataProviderMock.Object;

            var query = new ListDamageCodeQuery(null, null, null);

            var handler = new ListDamageCodeQueryHandler(mapper, damageCodeReadRepository, damageCodeOdataProvider);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsFailure.Should().BeFalse();
            result.Should().BeOfType(typeof(Result<DamageCodesModel>));
        }

        [TestMethod]
        public async Task HandleShouldReturnFailWhenDatabaseSpecificErrorOccurs()
        {
            // Arrange
            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(x => x.Map<IEnumerable<DamageCodeModel>>(It.IsAny<object>())).Returns(Enumerable.Empty<DamageCodeModel>());
            var mapper = mapperMock.Object;

            var damageCodeReadRepositoryMock = new Mock<IDamageCodeReadRepository>();
            damageCodeReadRepositoryMock.Setup(x => x.ListAsync(It.IsAny<Expression<Func<DamageCode, bool>>>(), It.IsAny<int?>(), It.IsAny<int?>())).Throws<SomeDatabaseSpecificException>();
            var damageCodeReadRepository = damageCodeReadRepositoryMock.Object;

            var damageCodeOdataProviderMock = new Mock<IDamageCodeOdataProvider>();
            damageCodeOdataProviderMock.Setup(x => x.GetFilterPredicate(It.IsAny<string>())).Returns((Expression<Func<DamageCode, bool>>)null);
            var damageCodeOdataProvider = damageCodeOdataProviderMock.Object;

            var query = new ListDamageCodeQuery(null, null, null);

            var handler = new ListDamageCodeQueryHandler(mapper, damageCodeReadRepository, damageCodeOdataProvider);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Failures.Should().OnlyContain(x => x.Message == CustomFailures.ListDamageCodeFailure);
        }

        public class SomeDatabaseSpecificException : Exception { }
    }
}
