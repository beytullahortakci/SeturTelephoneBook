using TelephoneBook.PhoneService.DTOs;
using TelephoneBook.PhoneService.Models;

namespace TelephoneBook.PhoneService.Services
{
    public interface IContactService
    {
        Task<List<Contact>> GetAllAsync();
        Task<Contact?> GetByIdAsync(Guid id);
        Task<Contact> CreateAsync(ContactDto dto);
        Task<bool> DeleteAsync(Guid id);
    }
}
