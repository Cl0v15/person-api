using FluentAssertions;
using System;
using TPICAP.Persons.Domain.Persons;
using Xunit;

namespace TPICAP.Persons.Tests.Domain
{
    public class PersonNewTests
    {
        [Fact]
        public void New_Should_Return_A_New_Instance_Of_Person()
        {
            // Act
            var person = Person.New(Name.New("FirstName"), Name.New("LastName"), BirthDate.New(new DateTime(1949, 6, 8)), Salutation.Dr);

            // Assert
            person.Should().NotBeNull();
            person.Should().BeOfType<Person>();
            person.FirstName.Should().NotBeNull();
            person.LastName.Should().NotBeNull();
            person.DateOfBirth.Should().NotBeNull();
        }

        [Fact]
        public void New_Should_Throw_An_ArgumentNullException_When_FirstName_Is_Null()
        {
            // Act
            Action person = () => Person.New(null, Name.New("LastName"), BirthDate.New(new DateTime(1949, 6, 8)), Salutation.Dr);

            // Assert
            person.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void New_Should_Throw_An_ArgumentNullException_When_LastName_Is_Null()
        {
            // Act
            Action person = () => Person.New(Name.New("FirstName"), null, BirthDate.New(new DateTime(1949, 6, 8)), Salutation.Dr);

            // Assert
            person.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void New_Should_Throw_An_ArgumentNullException_When_DateOfBirth_Is_Null()
        {
            // Act
            Action person = () => Person.New(Name.New("FirstName"), Name.New("LastName"), null, Salutation.Dr);

            // Assert
            person.Should().Throw<ArgumentNullException>();
        }
    }
}
