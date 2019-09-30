﻿using AutoMapper;
using FluentAssertions;
using ITG.Brix.EccSetup.Application.Bases;
using ITG.Brix.EccSetup.Application.Cqs.Queries.Definitions;
using ITG.Brix.EccSetup.Application.Cqs.Queries.Handlers;
using ITG.Brix.EccSetup.Application.Cqs.Queries.Models.BuildingBlocks.Remark;
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
    public class ListRemarkQueryHandlerTests
    {
        [TestMethod]
        public void ConstructorShouldSucceed()
        {
            // Arrange
            var mapper = new Mock<IMapper>().Object;
            var remarkReadRepository = new Mock<IRemarkReadRepository>().Object;

            // Act
            Action ctor = () =>
            {
                new ListRemarkQueryHandler(mapper, remarkReadRepository);
            };

            // Assert
            ctor.Should().NotThrow();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenMapperIsNull()
        {
            // Arrange
            IMapper mapper = null;
            var remarkReadRepository = new Mock<IRemarkReadRepository>().Object;

            // Act
            Action ctor = () =>
            {
                new ListRemarkQueryHandler(mapper, remarkReadRepository);
            };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenRemarkReadRepositoryIsNull()
        {
            // Arrange
            var mapper = new Mock<IMapper>().Object;
            IRemarkReadRepository remarkReadRepository = null;

            // Act
            Action ctor = () =>
            {
                new ListRemarkQueryHandler(mapper, remarkReadRepository);
            };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public async Task HandleShouldReturnOk()
        {
            // Arrange
            var remarkReadRepositoryMock = new Mock<IRemarkReadRepository>();
            remarkReadRepositoryMock.Setup(x => x.ListAsync(It.IsAny<Expression<Func<Remark, bool>>>(), It.IsAny<int?>(), It.IsAny<int?>())).Returns(Task.FromResult(Enumerable.Empty<Remark>()));
            var remarkReadRepository = remarkReadRepositoryMock.Object;

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(x => x.Map<IEnumerable<RemarkModel>>(It.IsAny<object>())).Returns(Enumerable.Empty<RemarkModel>());
            var mapper = mapperMock.Object;

            var query = new ListRemarkQuery(null, null, null);

            var handler = new ListRemarkQueryHandler(mapper, remarkReadRepository);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsFailure.Should().BeFalse();
            result.Should().BeOfType(typeof(Result<RemarksModel>));
        }

        [TestMethod]
        public async Task HandleShouldReturnFailWhenDatabaseSpecificErrorOccurs()
        {
            // Arrange
            var remarkReadRepositoryMock = new Mock<IRemarkReadRepository>();
            remarkReadRepositoryMock.Setup(x => x.ListAsync(It.IsAny<Expression<Func<Remark, bool>>>(), It.IsAny<int?>(), It.IsAny<int?>())).Throws<SomeDatabaseSpecificException>();
            var remarkReadRepository = remarkReadRepositoryMock.Object;

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(x => x.Map<IEnumerable<RemarkModel>>(It.IsAny<object>())).Returns(Enumerable.Empty<RemarkModel>());
            var mapper = mapperMock.Object;

            var query = new ListRemarkQuery(null, null, null);

            var handler = new ListRemarkQueryHandler(mapper, remarkReadRepository);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Failures.Should().OnlyContain(x => x.Message == CustomFailures.ListRemarkFailure);
        }

        public class SomeDatabaseSpecificException : Exception { }
    }
}
