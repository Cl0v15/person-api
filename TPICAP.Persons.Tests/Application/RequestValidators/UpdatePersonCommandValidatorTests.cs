
using FluentAssertions;
using System;
using TPICAP.Persons.API.Dtos;
using TPICAP.Persons.API.Requests;
using TPICAP.Persons.API.Requests.Validators;
using Xunit;

namespace TPICAP.Persons.Tests.Application.RequestValidators
{
    public class UpdatePersonCommandValidatorTests
    {
        [Fact]
        public void Validate_Returns_0_Error_When_All_Conditions_Are_Met()
        {
            // Arrange
            var sut = new UpdatePersonCommandValidator();

            // Act
            var validation = sut.Validate(new UpdatePersonCommand(1, "FirstName", "LastName", new DateTime(1986, 1, 3), SalutationDto.Lord));

            // Assert
            validation.IsValid.Should().BeTrue();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Validate_Returns_1_Error_When_PersonId_Is_Not_Greater_Than_0(int personId)
        {
            // Arrange
            var sut = new UpdatePersonCommandValidator();

            // Act
            var validation = sut.Validate(new UpdatePersonCommand(personId, "FirstName", "LastName", new DateTime(1986, 1, 3), SalutationDto.Lord));

            // Assert
            validation.IsValid.Should().BeFalse();
            validation.Errors.Count.Should().Equals(1);
            validation.Errors[0].PropertyName.Should().Equals("PersonId");
            validation.Errors[0].ErrorCode.Should().Equals("GreaterThanValidator");
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Validate_Returns_1_Error_When_FirstName_Is_Null_Or_Empty(string firstName)
        {
            // Arrange
            var sut = new UpdatePersonCommandValidator();

            // Act
            var validation = sut.Validate(new UpdatePersonCommand(1, firstName, "LastName", new DateTime(1986, 1, 3), SalutationDto.Lord));

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
            var sut = new UpdatePersonCommandValidator();

            // Act
            var validation = sut.Validate(new UpdatePersonCommand(1, "FirstName", lastName, new DateTime(1986, 1, 3), SalutationDto.Lord));

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
            var sut = new UpdatePersonCommandValidator();

            // Act
            var validation = sut.Validate(new UpdatePersonCommand(1, "FirstName", "LastName", DateTime.Now.AddDays(1), SalutationDto.Lord));

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
            var sut = new UpdatePersonCommandValidator();

            // Act
            var validation = sut.Validate(new UpdatePersonCommand(1, "FirstName", "LastName", new DateTime(1986, 1, 3), (SalutationDto)999));

            // Assert
            validation.IsValid.Should().BeFalse();
            validation.Errors.Count.Should().Equals(1);
            validation.Errors[0].PropertyName.Should().Equals("Salutation");
            validation.Errors[0].ErrorCode.Should().Equals("EnumValidator");
        }
    }
}
