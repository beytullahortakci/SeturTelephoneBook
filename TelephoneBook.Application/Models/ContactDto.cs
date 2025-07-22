namespace TelephoneBook.Application.Models
{
    public class ContactAddRequestDto
    {
        public string FullName { get; set; }
        public string Company { get; set; }
    }

    public class ContactInfoRequestDto
    {
        public string Type { get; set; }
        public string Value { get; set; }
    }

    public class ContactDetailAddRequestDto
    {
        public string ContactId { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
    }
    public class ContactDetailAddResponseDto
    {
        public string FullName { get; set; }
        public string Company { get; set; }
        public List<ContactInfoRequestDto> ContactInfos { get; set; } = new List<ContactInfoRequestDto>();
    }
}
