using FluentAssertions;
using System;
using TPICAP.Persons.Domain.Persons;
using Xunit;

namespace TPICAP.Persons.Tests.Domain
{
    public class PersonUpdateTests
    {
        [Fact]
        public void Update_Should_Update_Person()
        {
            // Arrange
            var personToUpdate = Person.New(Name.New("FirstName1"), Name.New("LastName1"), BirthDate.New(new DateTime(1962, 5, 10)), Salutation.Mr);
            var updatedFirstName = Name.New("FirstNameUpdated");
            var updatedLastName = Name.New("lastNameUpdated");
            var updatedDateOfBirth = BirthDate.New(new System.DateTime(1982, 2, 1));
            var updatedSalutation = Salutation.Major;

            // Act
            personToUpdate.Update(updatedFirstName, updatedLastName, updatedDateOfBirth, updatedSalutation);

            // Assert
            personToUpdate.FirstName.Should().Be(updatedFirstName);
            personToUpdate.LastName.Should().Be(updatedLastName);
            personToUpdate.DateOfBirth.Should().Be(updatedDateOfBirth);
            personToUpdate.Salutation.Should().Be(updatedSalutation);
        }

        [Fact]
        public void Update_Should_Throw_An_ArgumentNullException_When_FirstName_Is_Null()
        {
            // Act
            Action person = () => Person.New(null, Name.New("LastName"), BirthDate.New(new DateTime(1949, 6, 8)), Salutation.Dr);

            // Assert
            person.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Update_Should_Throw_An_ArgumentNullException_When_LastName_Is_Null()
        {
            // Act
            Action person = () => Person.New(Name.New("FirstName"), null, BirthDate.New(new DateTime(1949, 6, 8)), Salutation.Dr);

            // Assert
            person.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Update_Should_Throw_An_ArgumentNullException_When_DateOfBirth_Is_Null()
        {
            // Act
            Action person = () => Person.New(Name.New("FirstName"), Name.New("LastName"), null, Salutation.Dr);

            // Assert
            person.Should().Throw<ArgumentNullException>();
        }
    }
}
