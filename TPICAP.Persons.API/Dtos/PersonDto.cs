using System;

namespace TPICAP.Persons.API.Dtos
{
    public class PersonDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public SalutationDto Salutation { get; set; }
    }
}
