using FluentAssertions;
using System;
using TPICAP.Persons.API.Dtos;
using TPICAP.Persons.API.Requests;
using TPICAP.Persons.API.Requests.Validators;
using Xunit;

namespace TPICAP.Persons.Tests.Application.RequestValidators
{
    public class AddPersonCommandValidatorTests
    {
        [Fact]
        public void Validate_Returns_0_Error_When_All_Conditions_Are_Met()
        {
            // Arrange
            var sut = new AddPersonCommandValidator();

            // Act
            var validation = sut.Validate(new AddPersonCommand("FirstName", "LastName", new DateTime(1986, 1, 3), SalutationDto.Lord));

            // Assert
            validation.IsValid.Should().BeTrue();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Validate_Returns_1_Error_When_FirstName_Is_Null_Or_Empty(string firstName)
        {
            // Arrange
            var sut = new AddPersonCommandValidator();

            // Act
            var validation = sut.Validate(new AddPersonCommand(firstName, "LastName", new DateTime(1986, 1, 3), SalutationDto.Lord));

            // Assert
            validation.IsValid.Should().BeFalse();
            validation.Errors.Count.Should().Equals(1);
            validation.Errors[0].PropertyName.Should().Equals("FirstName");
            validation.Errors[0].ErrorCode.Should().Equals("NotEmptyValidator");
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Validate_Returns_1_Error_When_LastName_Is_Null_Or_Empty(string lastName)
        {
            // Arrange
            var sut = new AddPersonCommandValidator();

            // Act
            var validation = sut.Validate(new AddPersonCommand("FirstName", lastName, new DateTime(1986, 1, 3), SalutationDto.Lord));

            // Assert
            validation.IsValid.Should().BeFalse();
            validation.Errors.Count.Should().Equals(1);
            validation.Errors[0].PropertyName.Should().Equals("LastName");
            validation.Errors[0].ErrorCode.Should().Equals("NotEmptyValidator");
        }

        [Fact]
        public void Validate_returns_1_error_when_DateOfBirth_Is_Greater_Than_Today()
        {
            // Arrange
            var sut = new AddPersonCommandValidator();

            // Act
            var validation = sut.Validate(new AddPersonCommand("FirstName", "LastName", DateTime.Now.AddDays(1), SalutationDto.Lord));

            // Assert
            validation.IsValid.Should().BeFalse();
            validation.Errors.Count.Should().Equals(1);
            validation.Errors[0].PropertyName.Should().Equals("DateOfBirth");
            validation.Errors[0].ErrorCode.Should().Equals("LessThanOrEqualValidator");
        }

        [Fact]
        public void Validate_returns_1_error_when_Salutation_Is_Not_In_Enum()
        {
            // Arrange
            var sut = new AddPersonCommandValidator();

            // Act
            var validation = sut.Validate(new AddPersonCommand("FirstName", "LastName", new DateTime(1986, 1, 3), (SalutationDto)999));

            // Assert
            validation.IsValid.Should().BeFalse();
            validation.Errors.Count.Should().Equals(1);
            validation.Errors[0].PropertyName.Should().Equals("Salutation");
            validation.Errors[0].ErrorCode.Should().Equals("EnumValidator");
        }
    }
}
