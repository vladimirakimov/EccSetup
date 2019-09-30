using AutoMapper;
using FluentAssertions;
using ITG.Brix.EccSetup.Application.Bases;
using ITG.Brix.EccSetup.Application.Cqs.Queries.Definitions;
using ITG.Brix.EccSetup.Application.Cqs.Queries.Handlers;
using ITG.Brix.EccSetup.Application.Cqs.Queries.Models.BuildingBlocks;
using ITG.Brix.EccSetup.Application.Resources;
using ITG.Brix.EccSetup.Domain;
using ITG.Brix.EccSetup.Domain.Repositories;
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
    public class ListInformationQueryHandlerTests
    {
        [TestMethod]
        public void ConstructorShouldSucceed()
        {
            // Arrange
            var mapper = new Mock<IMapper>().Object;
            var informationReadRepository = new Mock<IInformationReadRepository>().Object;

            // Act
            Action ctor = () =>
            {
                new ListInformationQueryHandler(mapper, informationReadRepository);
            };

            // Assert
            ctor.Should().NotThrow();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenMapperIsNull()
        {
            // Arrange
            IMapper mapper = null;
            var informationReadRepository = new Mock<IInformationReadRepository>().Object;

            // Act
            Action ctor = () =>
            {
                new ListInformationQueryHandler(mapper, informationReadRepository);
            };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenInformationReadRepositoryIsNull()
        {
            // Arrange
            var mapper = new Mock<IMapper>().Object;
            IInformationReadRepository informationReadRepository = null;

            // Act
            Action ctor = () =>
            {
                new ListInformationQueryHandler(mapper, informationReadRepository);
            };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public async Task HandleShouldReturnOk()
        {
            // Arrange
            var informationReadRepositoryMock = new Mock<IInformationReadRepository>();
            informationReadRepositoryMock.Setup(x => x.ListAsync(It.IsAny<Expression<Func<Information, bool>>>(), It.IsAny<int?>(), It.IsAny<int?>())).Returns(Task.FromResult(Enumerable.Empty<Information>()));
            var informationReadRepository = informationReadRepositoryMock.Object;

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(x => x.Map<IEnumerable<InformationModel>>(It.IsAny<object>())).Returns(Enumerable.Empty<InformationModel>());
            var mapper = mapperMock.Object;

            var query = new ListInformationQuery(null, null, null);

            var handler = new ListInformationQueryHandler(mapper, informationReadRepository);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsFailure.Should().BeFalse();
            result.Should().BeOfType(typeof(Result<InformationsModel>));
        }

        [TestMethod]
        public async Task HandleShouldReturnFailWhenDatabaseSpecificErrorOccurs()
        {
            // Arrange
            var informationReadRepositoryMock = new Mock<IInformationReadRepository>();
            informationReadRepositoryMock.Setup(x => x.ListAsync(It.IsAny<Expression<Func<Information, bool>>>(), It.IsAny<int?>(), It.IsAny<int?>())).Throws<SomeDatabaseSpecificException>();
            var informationReadRepository = informationReadRepositoryMock.Object;

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(x => x.Map<IEnumerable<InformationModel>>(It.IsAny<object>())).Returns(Enumerable.Empty<InformationModel>());
            var mapper = mapperMock.Object;

            var query = new ListInformationQuery(null, null, null);

            var handler = new ListInformationQueryHandler(mapper, informationReadRepository);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Failures.Should().OnlyContain(x => x.Message == CustomFailures.ListInformationFailure);
        }

        public class SomeDatabaseSpecificException : Exception { }
    }
}
