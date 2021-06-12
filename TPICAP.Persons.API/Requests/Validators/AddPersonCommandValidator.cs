using FluentValidation;
using System;

namespace TPICAP.Persons.API.Requests.Validators
{
    public class AddPersonCommandValidator : AbstractValidator<AddPersonCommand>
    {
        public AddPersonCommandValidator()
        {
            RuleFor(r => r.FirstName).NotEmpty().MaximumLength(50);
            RuleFor(r => r.LastName).NotEmpty().MaximumLength(50);
            RuleFor(r => r.DateOfBirth).LessThanOrEqualTo(DateTime.Now);
            RuleFor(r => r.Salutation).IsInEnum();
        }
    }
}
