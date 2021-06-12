using MediatR;
using System;
using TPICAP.Persons.API.Dtos;

namespace TPICAP.Persons.API.Requests
{
    public class UpdatePersonCommand : IRequest
    {
        public int PersonId { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public DateTime DateOfBirth { get; }
        public SalutationDto Salutation { get; }

        public UpdatePersonCommand(
            int personId,
            string firstName,
            string lastName,
            DateTime dateOfBirth,
            SalutationDto salutation)
        {
            PersonId = personId;
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            Salutation = salutation;
        }
    }
}
