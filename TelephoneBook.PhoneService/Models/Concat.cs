using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TelephoneBook.PhoneService.Models
{
    public class Contact
    {
        [Key] public Guid Id { get; set; }

        [Required] public string FullName { get; set; }

        public string Company { get; set; }

        public ICollection<ContactInfo> ContactInfos { get; set; } = new List<ContactInfo>();
    }

    public class ContactInfo
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Type { get; set; }

        [Required]
        public string Value { get; set; }

        [ForeignKey("Contact")]
        public Guid ContactId { get; set; }

        public Contact Contact { get; set; }
    }
}