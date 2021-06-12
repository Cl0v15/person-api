using FluentAssertions;
using FluentValidation;
using System;
using TPICAP.Persons.Domain.Persons;
using Xunit;

namespace TPICAP.Persons.Tests.Domain
{
    public class ExistsPersonSpecificationTests
    {
        [Fact]
        public void EnforceRule_Should_Throw_A_ValidationException_When_Person_Is_Null()
        {
            // Arrange
            var sut = new ExistsPersonSpecification();

            // Act
            Action act = () => sut.EnforceRule(null, "Person not found");

            // Assert
            act.Should().Throw<ValidationException>();
        }
    }
}
