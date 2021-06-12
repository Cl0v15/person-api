using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TPICAP.Persons.API.Dtos;
using TPICAP.Persons.Domain.Persons;

namespace TPICAP.Persons.API.Requests
{
    public class GetPersonQueryHandler : IRequestHandler<GetPersonQuery, PersonDto>
    {
        private readonly IPersonRepository _personRepository;
        private readonly IMapper _mapper;

        public GetPersonQueryHandler(
            IPersonRepository personRepository,
            IMapper mapper)
        {
            _personRepository = personRepository;
            _mapper = mapper;
        }

        public async Task<PersonDto> Handle(
            GetPersonQuery request,
            CancellationToken cancellationToken)
        {
            return _mapper.Map<PersonDto>(await _personRepository.GetAsync(request.PersonId));
        }
    }
}
