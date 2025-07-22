namespace TelephoneBook.PhoneService.DTOs
{
    public class ContactDto
    {
        public string FullName { get; set; }
        public string Company { get; set; }

        public List<ContactInfoDto> ContactInfos { get; set; } = new();
    }

    public class ContactInfoDto
    {
        public string Type { get; set; }
        public string Value { get; set; }
    }
}
