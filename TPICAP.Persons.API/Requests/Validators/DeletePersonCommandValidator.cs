using FluentValidation;

namespace TPICAP.Persons.API.Requests.Validators
{
    public class DeletePersonCommandValidator : AbstractValidator<DeletePersonCommand>
    {
        public DeletePersonCommandValidator()
        {
            RuleFor(r => r.PersonId).GreaterThan(0);
        }
    }
}
