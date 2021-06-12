using MediatR;
using System.Collections.Generic;
using TPICAP.Persons.API.Dtos;

namespace TPICAP.Persons.API.Requests
{
    public class GetAllPersonsQuery : IRequest<IEnumerable<PersonDto>>
    {
    }
}
