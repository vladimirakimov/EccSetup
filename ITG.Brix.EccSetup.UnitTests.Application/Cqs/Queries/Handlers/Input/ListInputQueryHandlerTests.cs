using AutoMapper;
using FluentAssertions;
using ITG.Brix.EccSetup.Application.Bases;
using ITG.Brix.EccSetup.Application.Cqs.Queries.Definitions;
using ITG.Brix.EccSetup.Application.Cqs.Queries.Handlers;
using ITG.Brix.EccSetup.Application.Cqs.Queries.Models;
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
    public class ListInputQueryHandlerTests
    {
        [TestMethod]
        public void ConstructorShouldSucceed()
        {
            // Arrange
            var mapper = new Mock<IMapper>().Object;
            var inputReadRepository = new Mock<IInputReadRepository>().Object;

            // Act
            Action ctor = () =>
            {
                new ListInputQueryHandler(mapper, inputReadRepository);
            };

            // Assert
            ctor.Should().NotThrow();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenMapperIsNull()
        {
            // Arrange
            IMapper mapper = null;
            var inputReadRepository = new Mock<IInputReadRepository>().Object;

            // Act
            Action ctor = () =>
            {
                new ListInputQueryHandler(mapper, inputReadRepository);
            };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenInputReadRepositoryIsNull()
        {
            // Arrange
            var mapper = new Mock<IMapper>().Object;
            IInputReadRepository inputReadRepository = null;

            // Act
            Action ctor = () =>
            {
                new ListInputQueryHandler(mapper, inputReadRepository);
            };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public async Task HandleShouldReturnOk()
        {
            // Arrange
            var inputReadRepositoryMock = new Mock<IInputReadRepository>();
            inputReadRepositoryMock.Setup(x => x.ListAsync(It.IsAny<Expression<Func<Input, bool>>>(), It.IsAny<int?>(), It.IsAny<int?>())).Returns(Task.FromResult(Enumerable.Empty<Input>()));
            var inputReadRepository = inputReadRepositoryMock.Object;

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(x => x.Map<IEnumerable<InputModel>>(It.IsAny<object>())).Returns(Enumerable.Empty<InputModel>());
            var mapper = mapperMock.Object;

            var query = new ListInputQuery(null, null, null);

            var handler = new ListInputQueryHandler(mapper, inputReadRepository);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsFailure.Should().BeFalse();
            result.Should().BeOfType(typeof(Result<InputsModel>));
        }

        [TestMethod]
        public async Task HandleShouldReturnFailWhenDatabaseSpecificErrorOccurs()
        {
            // Arrange
            var inputReadRepositoryMock = new Mock<IInputReadRepository>();
            inputReadRepositoryMock.Setup(x => x.ListAsync(It.IsAny<Expression<Func<Input, bool>>>(), It.IsAny<int?>(), It.IsAny<int?>())).Throws<SomeDatabaseSpecificException>();
            var inputReadRepository = inputReadRepositoryMock.Object;

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(x => x.Map<IEnumerable<InputModel>>(It.IsAny<object>())).Returns(Enumerable.Empty<InputModel>());
            var mapper = mapperMock.Object;

            var query = new ListInputQuery(null, null, null);

            var handler = new ListInputQueryHandler(mapper, inputReadRepository);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Failures.Should().OnlyContain(x => x.Message == CustomFailures.ListInputFailure);
        }

        public class SomeDatabaseSpecificException : Exception { }
    }
}
