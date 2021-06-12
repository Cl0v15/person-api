using MediatR;
using System;
using TPICAP.Persons.API.Dtos;

namespace TPICAP.Persons.API.Requests
{
    public class AddPersonCommand : IRequest
    {
        public string FirstName { get; }
        public string LastName { get; }
        public DateTime DateOfBirth { get; }
        public SalutationDto Salutation { get; }

        public AddPersonCommand(
            string firstName,
            string lastName,
            DateTime dateOfBirth,
            SalutationDto salutation)
        {
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            Salutation = salutation;
        }
    }
}
