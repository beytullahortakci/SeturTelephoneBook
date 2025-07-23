using TelephoneBook.Domain.Enums;

namespace TelephoneBook.Application.Models
{
    public class ContactAddRequestDto
    {
        public string ContactName { get; set; }
        public string ContactLastName { get; set; }
        public string ContactCompany { get; set; }
    }

    public class ContactInfoRequestDto
    {
        public string Type { get; set; }
        public string Value { get; set; }
    }

    public class ContactDetailAddRequestDto
    {
        public string ContactId { get; set; }
        public ContactDetailsType Type { get; set; }
        public string Value { get; set; }
    }
    public class ContactDetailAddResponseDto
    {
        public string FullName { get; set; }
        public string Company { get; set; }
        public List<ContactInfoRequestDto> ContactInfos { get; set; } = new List<ContactInfoRequestDto>();
    }
}
