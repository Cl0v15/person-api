using FluentAssertions;
using System;
using TPICAP.Persons.Domain.Persons;
using Xunit;

namespace TPICAP.Persons.Tests.Domain
{
    public class NameNewTests
    {
        [Fact]
        public void New_Should_Return_A_New_Instance_Of_Name()
        {
            // Arrange
            var value = "FirstName";

            // Act
            var name = Name.New(value);

            // Assert
            name.Should().NotBeNull();
            name.Should().BeOfType<Name>();
            name.Value.Should().NotBeEmpty();
            name.Value.Should().Be(value);
        }

        [Fact]
        public void New_Should_Throw_An_ArgumentNullException_When_Value_Is_Null()
        {
            // Act
            Action name = () => Name.New(null);

            // Assert
            name.Should().Throw<ArgumentNullException>();
        }

        [Theory]
        [InlineData(" ")]
        [InlineData("")]
        public void New_Should_Throw_An_ArgumentException_When_FirstName_Is_Value_Or_WhiteSpace(string value)
        {
            // Act
            Action name = () => Name.New(value);

            // Assert
            name.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void New_Should_Throw_An_ArgumentException_When_Value_Length_Is_Greater_Than_50_Characters()
        {
            // Act
            Action name = () => Name.New("averyverylongnamewithalengthgreaterthanfiftycharacters");

            // Assert
            name.Should().Throw<ArgumentException>();
        }
    }
}
