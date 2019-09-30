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
    public class ListIconQueryHandlerTests
    {
        [TestMethod]
        public void ConstructorShouldSucceed()
        {
            // Arrange
            var mapper = new Mock<IMapper>().Object;
            var iconReadRepository = new Mock<IIconReadRepository>().Object;
            var iconOdataProvider = new Mock<IIconOdataProvider>().Object;

            // Act
            Action ctor = () =>
            {
                new ListIconQueryHandler(mapper, iconReadRepository, iconOdataProvider);
            };

            // Assert
            ctor.Should().NotThrow();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenMapperIsNull()
        {
            // Arrange
            IMapper mapper = null;
            var iconReadRepository = new Mock<IIconReadRepository>().Object;
            var iconOdataProvider = new Mock<IIconOdataProvider>().Object;

            // Act
            Action ctor = () =>
            {
                new ListIconQueryHandler(mapper, iconReadRepository, iconOdataProvider);
            };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenIconReadRepositoryIsNull()
        {
            // Arrange
            var mapper = new Mock<IMapper>().Object;
            IIconReadRepository iconReadRepository = null;
            var iconOdataProvider = new Mock<IIconOdataProvider>().Object;

            // Act
            Action ctor = () =>
            {
                new ListIconQueryHandler(mapper, iconReadRepository, iconOdataProvider);
            };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenIconOdataProviderIsNull()
        {
            // Arrange
            var mapper = new Mock<IMapper>().Object;
            var iconReadRepository = new Mock<IIconReadRepository>().Object;
            IIconOdataProvider iconOdataProvider = null;

            // Act
            Action ctor = () =>
            {
                new ListIconQueryHandler(mapper, iconReadRepository, iconOdataProvider);
            };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public async Task HandleShouldReturnOk()
        {
            // Arrange
            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(x => x.Map<IEnumerable<IconModel>>(It.IsAny<object>())).Returns(Enumerable.Empty<IconModel>());
            var mapper = mapperMock.Object;

            var iconReadRepositoryMock = new Mock<IIconReadRepository>();
            iconReadRepositoryMock.Setup(x => x.ListAsync(It.IsAny<Expression<Func<Icon, bool>>>(), It.IsAny<int?>(), It.IsAny<int?>())).Returns(Task.FromResult(Enumerable.Empty<Icon>()));
            var iconReadRepository = iconReadRepositoryMock.Object;

            var iconOdataProviderMock = new Mock<IIconOdataProvider>();
            iconOdataProviderMock.Setup(x => x.GetFilterPredicate(It.IsAny<string>())).Returns((Expression<Func<Icon, bool>>)null);
            var iconOdataProvider = iconOdataProviderMock.Object;

            var query = new ListIconQuery(null, null, null);

            var handler = new ListIconQueryHandler(mapper, iconReadRepository, iconOdataProvider);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsFailure.Should().BeFalse();
            result.Should().BeOfType(typeof(Result<IconsModel>));
        }

        [TestMethod]
        public async Task HandleShouldReturnFailWhenFilterOdataErrorOccurs()
        {
            // Arrange
            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(x => x.Map<IEnumerable<FlowModel>>(It.IsAny<object>())).Returns(Enumerable.Empty<FlowModel>());
            var mapper = mapperMock.Object;

            var iconReadRepositoryMock = new Mock<IIconReadRepository>();
            iconReadRepositoryMock.Setup(x => x.ListAsync(It.IsAny<Expression<Func<Icon, bool>>>(), It.IsAny<int?>(), It.IsAny<int?>())).Returns(Task.FromResult(Enumerable.Empty<Icon>()));
            var iconReadRepository = iconReadRepositoryMock.Object;

            var iconOdataProviderMock = new Mock<IIconOdataProvider>();
            iconOdataProviderMock.Setup(x => x.GetFilterPredicate(It.IsAny<string>())).Throws<FilterODataException>();
            var iconOdataProvider = iconOdataProviderMock.Object;

            var query = new ListIconQuery(null, null, null);

            var handler = new ListIconQueryHandler(mapper, iconReadRepository, iconOdataProvider);

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
            mapperMock.Setup(x => x.Map<IEnumerable<IconModel>>(It.IsAny<object>())).Returns(Enumerable.Empty<IconModel>());
            var mapper = mapperMock.Object;

            var iconReadRepositoryMock = new Mock<IIconReadRepository>();
            iconReadRepositoryMock.Setup(x => x.ListAsync(It.IsAny<Expression<Func<Icon, bool>>>(), It.IsAny<int?>(), It.IsAny<int?>())).Throws<SomeDatabaseSpecificException>();
            var iconReadRepository = iconReadRepositoryMock.Object;

            var iconOdataProviderMock = new Mock<IIconOdataProvider>();
            iconOdataProviderMock.Setup(x => x.GetFilterPredicate(It.IsAny<string>())).Returns((Expression<Func<Icon, bool>>)null);
            var iconOdataProvider = iconOdataProviderMock.Object;

            var query = new ListIconQuery(null, null, null);

            var handler = new ListIconQueryHandler(mapper, iconReadRepository, iconOdataProvider);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Failures.Should().OnlyContain(x => x.Message == CustomFailures.ListIconFailure);
        }

        public class SomeDatabaseSpecificException : Exception { }
    }
}
