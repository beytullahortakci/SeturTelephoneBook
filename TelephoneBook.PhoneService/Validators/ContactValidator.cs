using FluentValidation;
using TelephoneBook.Application.Models;

namespace TelephoneBook.PhoneService.Validators
{
    public class ContactValidator : AbstractValidator<ContactAddRequestDto>
    {
        public ContactValidator()
        {
            RuleFor(x => x.ContactName).NotEmpty().WithMessage("Name is required.");
            RuleFor(x => x.ContactLastName).NotEmpty().WithMessage("LastName is required.");
            RuleFor(x => x.ContactCompany).NotEmpty().WithMessage("Company is required.");
        }
    }

    public class ContactInfoValidator : AbstractValidator<ContactDetailAddRequestDto>
    {
        public ContactInfoValidator()
        {
            RuleFor(x => x.ContactId).NotEmpty().WithMessage("ContactId is required.");
            RuleFor(x => x.Type).IsInEnum();
            RuleFor(x => x.Value).NotEmpty().WithMessage("Value is required.");
        }
    }
}
