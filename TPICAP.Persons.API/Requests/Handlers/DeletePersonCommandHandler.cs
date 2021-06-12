using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TPICAP.Persons.Domain.Persons;

namespace TPICAP.Persons.API.Requests.Handlers
{
    public class DeletePersonCommandHandler : IRequestHandler<DeletePersonCommand>
    {
        private readonly IPersonRepository _personRepository;
        private readonly IExistsPersonSpecification _existsPersonSpecification;

        public DeletePersonCommandHandler(
            IPersonRepository personRepository,
            IExistsPersonSpecification existsPersonSpecification)
        {
            _personRepository = personRepository;
            _existsPersonSpecification = existsPersonSpecification;
        }

        public async Task<Unit> Handle(DeletePersonCommand request, CancellationToken cancellationToken)
        {
            var personToDelete = await RetrievePersonToDelete(request);
            _personRepository.Delete(personToDelete);

            return Unit.Value;
        }

        private async Task<Person> RetrievePersonToDelete(DeletePersonCommand request)
        {
            var personToUpdate = await _personRepository.GetAsync(request.PersonId);
            _existsPersonSpecification.EnforceRule(personToUpdate, "Person not found.");

            return personToUpdate;
        }
    }
}
