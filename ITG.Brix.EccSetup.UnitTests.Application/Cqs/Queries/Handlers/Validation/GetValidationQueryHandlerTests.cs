using AutoMapper;
using FluentAssertions;
using ITG.Brix.EccSetup.Application.Bases;
using ITG.Brix.EccSetup.Application.Cqs.Queries.Definitions;
using ITG.Brix.EccSetup.Application.Cqs.Queries.Handlers;
using ITG.Brix.EccSetup.Application.Cqs.Queries.Models.BuildingBlocks.Validations;
using ITG.Brix.EccSetup.Application.Resources;
using ITG.Brix.EccSetup.Domain;
using ITG.Brix.EccSetup.Domain.Repositories;
using ITG.Brix.EccSetup.Infrastructure.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ITG.Brix.EccSetup.UnitTests.Application.Cqs.Queries.Handlers
{
    [TestClass]
    public class GetValidationQueryHandlerTests
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
                new GetValidationQueryHandler(mapper, validationReadRepository);
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
                new GetValidationQueryHandler(mapper, validationReadRepository);
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
                new GetValidationQueryHandler(mapper, validationReadRepository);
            };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public async Task HandleShouldReturnOk()
        {
            // Arrange
            var id = Guid.NewGuid();
            string name = "name";
            string nameOnApplication = "nameOnApplication";
            string description = "description";
            string instruction = "instruction";
            var icon = new BuildingBlockIcon(Guid.NewGuid());

            var validationReadRepositoryMock = new Mock<IValidationReadRepository>();
            validationReadRepositoryMock.Setup(x => x.GetAsync(id)).Returns(Task.FromResult(new Validation(id, name, nameOnApplication, description, instruction, icon)));
            var validationReadRepository = validationReadRepositoryMock.Object;

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(x => x.Map<ValidationModel>(It.IsAny<object>())).Returns(new ValidationModel());
            var mapper = mapperMock.Object;

            var query = new GetValidationQuery(id);

            var handler = new GetValidationQueryHandler(mapper, validationReadRepository);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsFailure.Should().BeFalse();
            result.Should().BeOfType(typeof(Result<ValidationModel>));
        }

        [TestMethod]
        public async Task HandleShouldReturnFailWhenNotFound()
        {
            // Arrange
            var id = Guid.NewGuid();

            var validationReadRepositoryMock = new Mock<IValidationReadRepository>();
            validationReadRepositoryMock.Setup(x => x.GetAsync(id)).Throws<EntityNotFoundDbException>();
            var validationReadRepository = validationReadRepositoryMock.Object;

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(x => x.Map<ValidationModel>(It.IsAny<object>())).Returns(new ValidationModel());
            var mapper = mapperMock.Object;

            var query = new GetValidationQuery(id);

            var handler = new GetValidationQueryHandler(mapper, validationReadRepository);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Failures.Should().OnlyContain(x => x.Code == HandlerFaultCode.NotFound.Name);
        }

        [TestMethod]
        public async Task HandleShouldReturnFailWhenDatabaseSpecificErrorOccurs()
        {
            // Arrange
            var id = Guid.NewGuid();

            var validationReadRepositoryMock = new Mock<IValidationReadRepository>();
            validationReadRepositoryMock.Setup(x => x.GetAsync(id)).Throws<SomeDatabaseSpecificException>();
            var validationReadRepository = validationReadRepositoryMock.Object;

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(x => x.Map<ValidationModel>(It.IsAny<object>())).Returns(new ValidationModel());
            var mapper = mapperMock.Object;

            var query = new GetValidationQuery(id);

            var handler = new GetValidationQueryHandler(mapper, validationReadRepository);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Failures.Should().OnlyContain(x => x.Message == CustomFailures.GetValidationFailure);
        }

        public class SomeDatabaseSpecificException : Exception { }
    }
}
