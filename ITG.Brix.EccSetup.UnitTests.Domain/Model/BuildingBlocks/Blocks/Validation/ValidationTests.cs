using FluentAssertions;
using ITG.Brix.EccSetup.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ITG.Brix.EccSetup.UnitTests.Domain.BuildingBlocks
{
    [TestClass]
    public class ValidationTests
    {
        [TestMethod]
        public void CreateValidationShouldSucceed()
        {
            //Act
            var validation = new Validation(Guid.NewGuid(),
                                            "LicensePlateValidation",
                                            "IdCheck",
                                            "Some description",
                                            "Scan license plate",
                                            new BuildingBlockIcon(Guid.NewGuid()));
            //Assert
            validation.Should().NotBeNull();
            validation.BlockType.Should().Be(BlockType.Validation);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateValidationWithEmptyIdShouldFail()
        {
            //Act
            var validation = new Validation(Guid.Empty,
                                           "LicensePlateValidation",
                                           "IdCheck",
                                           "Some description",
                                           "Scan license plate",
                                           new BuildingBlockIcon(Guid.NewGuid()));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateValicationWithEmptyNameShouldFail()
        {
            //Act
            var validation = new Validation(Guid.NewGuid(),
                                           string.Empty,
                                           "IdCheck",
                                           "Some description",
                                           "Scan license plate",
                                           new BuildingBlockIcon(Guid.NewGuid()));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateValidationWithNullNameShouldFail()
        {
            //Act
            var validation = new Validation(Guid.NewGuid(),
                                           null,
                                           "IdCheck",
                                           "Some description",
                                           "Scan license plate",
                                           new BuildingBlockIcon(Guid.NewGuid()));
        }

        [TestMethod]
        public void GetAtomicValuesShouldSucceedWhenObjectsAreEquivalent()
        {
            //Act
            var validation1 = new Validation(new Guid("d9998c9b-c0d4-4a90-8d62-bc1b538be9c7"),
                                            "LicensePlateValidation",
                                            "IdCheck",
                                            "Some description",
                                            "Scan license plate",
                                            new BuildingBlockIcon(new Guid("618c6f2d-34c7-41e8-a2fa-584c4c56db03")));

            //Act
            var validation2 = new Validation(new Guid("d9998c9b-c0d4-4a90-8d62-bc1b538be9c7"),
                                            "LicensePlateValidation",
                                            "IdCheck",
                                            "Some description",
                                            "Scan license plate",
                                            new BuildingBlockIcon(new Guid("618c6f2d-34c7-41e8-a2fa-584c4c56db03")));

            //Assert
            validation1.Should().BeEquivalentTo(validation2);
        }
    }
}
