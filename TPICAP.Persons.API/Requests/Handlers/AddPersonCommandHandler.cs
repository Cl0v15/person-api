using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TPICAP.Persons.Domain.Persons;

namespace TPICAP.Persons.API.Requests.Handlers
{
    public class AddPersonCommandHandler : IRequestHandler<AddPersonCommand>
    {
        private readonly IPersonRepository _personRepository;

        public AddPersonCommandHandler(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public Task<Unit> Handle(
            AddPersonCommand request,
            CancellationToken cancellationToken)
        {
            _personRepository.Add(
                Person.New(
                    Name.New(request.FirstName),
                    Name.New(request.LastName),
                    BirthDate.New(request.DateOfBirth),
                    (Salutation)request.Salutation));

            return Unit.Task;
        }
    }
}
