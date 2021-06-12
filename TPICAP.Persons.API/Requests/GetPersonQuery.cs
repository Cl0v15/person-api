using MediatR;
using TPICAP.Persons.API.Dtos;

namespace TPICAP.Persons.API.Requests
{
    public class GetPersonQuery : IRequest<PersonDto>
    {
        public int PersonId { get; set; }

        public GetPersonQuery(int personId)
        {
            PersonId = personId;
        }
    }
}
