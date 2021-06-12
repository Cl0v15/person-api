using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TPICAP.Persons.API.Dtos;
using TPICAP.Persons.Domain.Persons;

namespace TPICAP.Persons.API.Requests
{
    public class GetAllPersonsQueryHandler : IRequestHandler<GetAllPersonsQuery, IEnumerable<PersonDto>>
    {
        private readonly IPersonRepository _personRepository;
        private readonly IMapper _mapper;

        public GetAllPersonsQueryHandler(
            IPersonRepository personRepository,
            IMapper mapper)
        {
            _personRepository = personRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PersonDto>> Handle(
            GetAllPersonsQuery request,
            CancellationToken cancellationToken)
        {
            return _mapper.Map<IEnumerable<PersonDto>>(await _personRepository.GetAllAsync());
        }
    }
}
