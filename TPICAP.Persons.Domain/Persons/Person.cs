using System;
using TPICAP.Persons.Shared;

namespace TPICAP.Persons.Domain.Persons
{
    public class Person : Entity
    {
        public Name FirstName { get; private set; }
        public Name LastName { get; private set; }
        public BirthDate DateOfBirth { get; private set; }
        public Salutation Salutation { get; private set; }

        private Person() { }

        public static Person New(
            Name firstName,
            Name lastName,
            BirthDate dateOfBirth,
            Salutation salutation)
        {
            firstName.NotNull();
            lastName.NotNull();
            dateOfBirth.NotNull();

            return new Person
            {
                FirstName = firstName,
                LastName = lastName,
                DateOfBirth = dateOfBirth,
                Salutation = salutation
            };
        }

        public void Update(
            Name firstName,
            Name lastName,
            BirthDate dateOfBirth,
            Salutation salutation)
        {
            firstName.NotNull();
            lastName.NotNull();
            dateOfBirth.NotNull();

            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            Salutation = salutation;

            Update();
        }
    }
}
