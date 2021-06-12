using System;

namespace TPICAP.Persons.API.Dtos
{
    public class NewPersonDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public SalutationDto Salutation { get; set; }
    }
}
