using AutoMapper;
using TPICAP.Persons.API.Dtos;
using TPICAP.Persons.Domain.Persons;

namespace TPICAP.Persons.API.Mappings
{
    public class PersonMapping : Profile
    {
        public PersonMapping()
        {
            CreateMap<Person, PersonDto>()
                .ForMember(d => d.FirstName, o => o.MapFrom(s => s.FirstName.Value))
                .ForMember(d => d.LastName, o => o.MapFrom(s => s.LastName.Value))
                .ForMember(d => d.DateOfBirth, o => o.MapFrom(s => s.DateOfBirth.Value));
        }
    }
}
