using FluentAssertions;
using System;
using TPICAP.Persons.Domain.Persons;
using Xunit;

namespace TPICAP.Persons.Tests.Domain
{
    public class BirthDateNewTests
    {
        [Fact]
        public void New_Should_Return_A_New_Instance_Of_BirthDate()
        {
            // Arrange
            var value = new DateTime(1999, 1, 5);

            // Act
            var birthDate = BirthDate.New(value);

            // Assert
            birthDate.Should().NotBeNull();
            birthDate.Should().BeOfType<BirthDate>();
            birthDate.Value.Should().Be(value);
        }

        [Fact]
        public void New_Should_Throw_An_ArgumentException_When_Value_Is_Greater_Than_Today()
        {
            // Arrange
            var value = DateTime.Now.AddDays(1);

            // Act
            Action name = () => BirthDate.New(value);

            // Assert
            name.Should().Throw<ArgumentException>();
        }
    }
}
