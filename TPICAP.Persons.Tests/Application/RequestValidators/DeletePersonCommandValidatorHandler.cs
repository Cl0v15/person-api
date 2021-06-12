using FluentAssertions;
using TPICAP.Persons.API.Requests;
using TPICAP.Persons.API.Requests.Validators;
using Xunit;

namespace TPICAP.Persons.Tests.Application.RequestValidators
{
    public class DeletePersonCommandValidatorHandler
    {
        [Fact]
        public void Validate_Returns_0_Error_When_All_Conditions_Are_Met()
        {
            // Arrange
            var sut = new DeletePersonCommandValidator();

            // Act
            var validation = sut.Validate(new DeletePersonCommand(1));

            // Assert
            validation.IsValid.Should().BeTrue();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Validate_Returns_1_Error_When_PersonId_Is_Not_Greater_Than_0(int personId)
        {
            // Arrange
            var sut = new DeletePersonCommandValidator();

            // Act
            var validation = sut.Validate(new DeletePersonCommand(personId));

            // Assert
            validation.IsValid.Should().BeFalse();
            validation.Errors.Count.Should().Equals(1);
            validation.Errors[0].PropertyName.Should().Equals("PersonId");
            validation.Errors[0].ErrorCode.Should().Equals("GreaterThanValidator");
        }
    }
}
