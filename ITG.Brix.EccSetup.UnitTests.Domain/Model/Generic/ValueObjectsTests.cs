﻿using FluentAssertions;
using ITG.Brix.EccSetup.Domain.Bases;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ITG.Brix.EccSetup.UnitTests.Domain.Generic
{
    [TestClass]
    public class ValueObjectsTests
    {
        [TestMethod]
        public void CreateValueObjectsWithDefaultCtorShouldFail()
        {
            // Arrange

            var expectedExceptionMessage = "No parameterless constructor defined for this object.";
            var expectedExceptionType = typeof(MissingMethodException);

            var valueObjectTypes = (from assembly in (from referencedAssembly in Assembly.GetExecutingAssembly().GetReferencedAssemblies()
                                                      select Assembly.Load(referencedAssembly)).ToArray()
                                    from assemblyType in assembly.GetTypes()
                                    where assemblyType.IsSubclassOf(typeof(ValueObject))
                                    select assemblyType).ToArray();

            // Act
            var exceptions = new List<Exception>();
            foreach (var valueObjectType in valueObjectTypes)
            {
                try
                {
                    Activator.CreateInstance(valueObjectType);
                    exceptions.Add(new Exception(string.Format("{0} created with default ctor.\r\nRemove default ctor to fix the issue.", valueObjectType)));
                }
                catch (MissingMethodException exception)
                {
                    exceptions.Add(exception);
                }
                catch (Exception exception)
                {
                    exceptions.Add(exception);
                }
            }

            // Assert
            foreach (var exception in exceptions)
            {
                exception.Message.Should().Be(expectedExceptionMessage);
                exception.Should().BeOfType(expectedExceptionType);
            }
        }
    }
}
