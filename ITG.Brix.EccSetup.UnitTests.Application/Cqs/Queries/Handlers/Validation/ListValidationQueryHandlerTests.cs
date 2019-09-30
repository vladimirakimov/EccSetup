using AutoMapper;
using FluentAssertions;
using ITG.Brix.EccSetup.Application.Bases;
using ITG.Brix.EccSetup.Application.Cqs.Queries.Definitions;
using ITG.Brix.EccSetup.Application.Cqs.Queries.Handlers;
using ITG.Brix.EccSetup.Application.Cqs.Queries.Models.BuildingBlocks.Validations;
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
    public class ListValidationQueryHandlerTests
    {
        [TestMethod]
        public void ConstructorShouldSucceed()
        {
            // Arrange
            var mapper = new Mock<IMapper>().Object;
            var validationReadRepository = new Mock<IValidationReadRepository>().Object;

            // Act
            Action ctor = () =>
            {
                new ListValidationQueryHandler(mapper, validationReadRepository);
            };

            // Assert
            ctor.Should().NotThrow();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenMapperIsNull()
        {
            // Arrange
            IMapper mapper = null;
            var validationReadRepository = new Mock<IValidationReadRepository>().Object;

            // Act
            Action ctor = () =>
            {
                new ListValidationQueryHandler(mapper, validationReadRepository);
            };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenValidationReadRepositoryIsNull()
        {
            // Arrange
            var mapper = new Mock<IMapper>().Object;
            IValidationReadRepository validationReadRepository = null;

            // Act
            Action ctor = () =>
            {
                new ListValidationQueryHandler(mapper, validationReadRepository);
            };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public async Task HandleShouldReturnOk()
        {
            // Arrange
            var validationReadRepositoryMock = new Mock<IValidationReadRepository>();
            validationReadRepositoryMock.Setup(x => x.ListAsync(It.IsAny<Expression<Func<Validation, bool>>>(), It.IsAny<int?>(), It.IsAny<int?>())).Returns(Task.FromResult(Enumerable.Empty<Validation>()));
            var validationReadRepository = validationReadRepositoryMock.Object;

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(x => x.Map<IEnumerable<ValidationModel>>(It.IsAny<object>())).Returns(Enumerable.Empty<ValidationModel>());
            var mapper = mapperMock.Object;

            var query = new ListValidationQuery(null, null, null);

            var handler = new ListValidationQueryHandler(mapper, validationReadRepository);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsFailure.Should().BeFalse();
            result.Should().BeOfType(typeof(Result<ValidationsModel>));
        }

        [TestMethod]
        public async Task HandleShouldReturnFailWhenDatabaseSpecificErrorOccurs()
        {
            // Arrange
            var validationReadRepositoryMock = new Mock<IValidationReadRepository>();
            validationReadRepositoryMock.Setup(x => x.ListAsync(It.IsAny<Expression<Func<Validation, bool>>>(), It.IsAny<int?>(), It.IsAny<int?>())).Throws<SomeDatabaseSpecificException>();
            var validationReadRepository = validationReadRepositoryMock.Object;

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(x => x.Map<IEnumerable<ValidationModel>>(It.IsAny<object>())).Returns(Enumerable.Empty<ValidationModel>());
            var mapper = mapperMock.Object;

            var query = new ListValidationQuery(null, null, null);

            var handler = new ListValidationQueryHandler(mapper, validationReadRepository);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Failures.Should().OnlyContain(x => x.Message == CustomFailures.ListValidationFailure);
        }

        public class SomeDatabaseSpecificException : Exception { }
    }
}
