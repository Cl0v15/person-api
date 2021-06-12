using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TPICAP.Persons.Domain.Persons;

namespace TPICAP.Persons.API.Requests
{
    public class UpdatePersonCommandHandler : IRequestHandler<UpdatePersonCommand>
    {
        private readonly IPersonRepository _personRepository;
        private readonly IExistsPersonSpecification _existsPersonSpecification;

        public UpdatePersonCommandHandler(
            IPersonRepository personRepository,
            IExistsPersonSpecification existsPersonSpecification)
        {
            _personRepository = personRepository;
            _existsPersonSpecification = existsPersonSpecification;
        }

        public async Task<Unit> Handle(UpdatePersonCommand request, CancellationToken cancellationToken)
        {
            var personToUpdate = await RetrievePersonToUpdate(request);
            UpdatePerson(request, personToUpdate);

            return Unit.Value;
        }

        private async Task<Person> RetrievePersonToUpdate(UpdatePersonCommand request)
        {
            var personToUpdate = await _personRepository.GetAsync(request.PersonId);
            _existsPersonSpecification.EnforceRule(personToUpdate, "Person not found.");

            return personToUpdate;
        }

        private void UpdatePerson(UpdatePersonCommand request, Person personToUpdate)
        {
            personToUpdate.Update(
                Name.New(request.FirstName),
                Name.New(request.LastName),
                BirthDate.New(request.DateOfBirth),
                (Salutation)request.Salutation);

            _personRepository.Update(personToUpdate);
        }
    }
}
