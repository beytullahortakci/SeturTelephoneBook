using FluentValidation;
using TelephoneBook.PhoneService.DTOs;

namespace TelephoneBook.PhoneService.Validators
{
    public class ContactValidator : AbstractValidator<ContactDto>
    {
        public ContactValidator()
        {
            RuleFor(x => x.FullName).NotEmpty().WithMessage("FullName is required.");
            RuleFor(x => x.Company).NotEmpty().WithMessage("Company is required.");

            RuleForEach(x => x.ContactInfos).SetValidator(new ContactInfoValidator());
        }
    }

    public class ContactInfoValidator : AbstractValidator<ContactInfoDto>
    {
        public ContactInfoValidator()
        {
            RuleFor(x => x.Type).NotEmpty().WithMessage("Type is required.");
            RuleFor(x => x.Value).NotEmpty().WithMessage("Value is required.");
        }
    }
}
