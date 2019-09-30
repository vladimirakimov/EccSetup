using AutoMapper;
using FluentAssertions;
using ITG.Brix.EccSetup.Application.Bases;
using ITG.Brix.EccSetup.Application.Cqs.Queries.Definitions;
using ITG.Brix.EccSetup.Application.Cqs.Queries.Handlers;
using ITG.Brix.EccSetup.Application.Cqs.Queries.Models;
using ITG.Brix.EccSetup.Application.Resources;
using ITG.Brix.EccSetup.Domain;
using ITG.Brix.EccSetup.Domain.Repositories.OperationalDepartments;
using ITG.Brix.EccSetup.Infrastructure.Providers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ITG.Brix.EccSetup.UnitTests.Application.Cqs.Queries.Handlers
{
    [TestClass]
    public class ListOperationalDepartmentQueryHandlerTests
    {
        [TestMethod]
        public void ConstructorShouldSucceed()
        {
            // Arrange
            var mapper = new Mock<IMapper>().Object;
            var operationalDepartmentReadRepository = new Mock<IOperationalDepartmentReadRepository>().Object;
            var operationalDepartmentOdataProvider = new Mock<IOperationalDepartmentOdataProvider>().Object;

            // Act
            Action ctor = () =>
            {
                new ListOperationalDepartmentQueryHandler(mapper, operationalDepartmentReadRepository, operationalDepartmentOdataProvider);
            };

            // Assert
            ctor.Should().NotThrow();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenMapperIsNull()
        {
            // Arrange
            IMapper mapper = null;
            var operationalDepartmentReadRepository = new Mock<IOperationalDepartmentReadRepository>().Object;
            var operationalDepartmentOdataProvider = new Mock<IOperationalDepartmentOdataProvider>().Object;

            // Act
            Action ctor = () =>
            {
                new ListOperationalDepartmentQueryHandler(mapper, operationalDepartmentReadRepository, operationalDepartmentOdataProvider);
            };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenOperationalDepartmentReadRepositoryIsNull()
        {
            // Arrange
            var mapper = new Mock<IMapper>().Object;
            IOperationalDepartmentReadRepository operationalDepartmentReadRepository = null;
            var operationalDepartmentOdataProvider = new Mock<IOperationalDepartmentOdataProvider>().Object;

            // Act
            Action ctor = () =>
            {
                new ListOperationalDepartmentQueryHandler(mapper, operationalDepartmentReadRepository, operationalDepartmentOdataProvider);
            };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenOperationalDepartmentOdataProviderIsNull()
        {
            // Arrange
            var mapper = new Mock<IMapper>().Object;
            var operationalDepartmentReadRepository = new Mock<IOperationalDepartmentReadRepository>().Object;
            IOperationalDepartmentOdataProvider operationalDepartmentOdataProvider = null;

            // Act
            Action ctor = () =>
            {
                new ListOperationalDepartmentQueryHandler(mapper, operationalDepartmentReadRepository, operationalDepartmentOdataProvider);
            };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public async Task HandleShouldReturnOk()
        {
            // Arrange
            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(x => x.Map<IEnumerable<OperationalDepartmentModel>>(It.IsAny<object>())).Returns(Enumerable.Empty<OperationalDepartmentModel>());
            var mapper = mapperMock.Object;

            var OperationalDepartmentReadRepositoryMock = new Mock<IOperationalDepartmentReadRepository>();
            OperationalDepartmentReadRepositoryMock.Setup(x => x.ListAsync(It.IsAny<Expression<Func<OperationalDepartment, bool>>>(), It.IsAny<int?>(), It.IsAny<int?>())).Returns(Task.FromResult(Enumerable.Empty<OperationalDepartment>()));
            var OperationalDepartmentReadRepository = OperationalDepartmentReadRepositoryMock.Object;

            var OperationalDepartmentOdataProviderMock = new Mock<IOperationalDepartmentOdataProvider>();
            OperationalDepartmentOdataProviderMock.Setup(x => x.GetFilterPredicate(It.IsAny<string>())).Returns((Expression<Func<OperationalDepartment, bool>>)null);
            var OperationalDepartmentOdataProvider = OperationalDepartmentOdataProviderMock.Object;

            var query = new ListOperationalDepartmentQuery(null, null, null);

            var handler = new ListOperationalDepartmentQueryHandler(mapper, OperationalDepartmentReadRepository, OperationalDepartmentOdataProvider);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsFailure.Should().BeFalse();
            result.Should().BeOfType(typeof(Result<OperationalDepartmentsModel>));
        }

        [TestMethod]
        public async Task HandleShouldReturnFailWhenDatabaseSpecificErrorOccurs()
        {
            // Arrange
            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(x => x.Map<IEnumerable<OperationalDepartmentModel>>(It.IsAny<object>())).Returns(Enumerable.Empty<OperationalDepartmentModel>());
            var mapper = mapperMock.Object;

            var OperationalDepartmentReadRepositoryMock = new Mock<IOperationalDepartmentReadRepository>();
            OperationalDepartmentReadRepositoryMock.Setup(x => x.ListAsync(It.IsAny<Expression<Func<OperationalDepartment, bool>>>(), It.IsAny<int?>(), It.IsAny<int?>())).Throws<SomeDatabaseSpecificException>();
            var OperationalDepartmentReadRepository = OperationalDepartmentReadRepositoryMock.Object;

            var OperationalDepartmentOdataProviderMock = new Mock<IOperationalDepartmentOdataProvider>();
            OperationalDepartmentOdataProviderMock.Setup(x => x.GetFilterPredicate(It.IsAny<string>())).Returns((Expression<Func<OperationalDepartment, bool>>)null);
            var OperationalDepartmentOdataProvider = OperationalDepartmentOdataProviderMock.Object;

            var query = new ListOperationalDepartmentQuery(null, null, null);

            var handler = new ListOperationalDepartmentQueryHandler(mapper, OperationalDepartmentReadRepository, OperationalDepartmentOdataProvider);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Failures.Should().OnlyContain(x => x.Message == CustomFailures.ListOperationalDepartmentFailure);
        }

        public class SomeDatabaseSpecificException : Exception { }
    }
}
