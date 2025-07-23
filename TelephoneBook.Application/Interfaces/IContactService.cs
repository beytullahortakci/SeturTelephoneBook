using TelephoneBook.Application.Models;
using TelephoneBook.Domain.Common;
using TelephoneBook.Domain.Entities;

namespace TelephoneBook.Application.Interfaces
{
    public interface IContactService
    {
        Task<Result<List<Contact>>> GetAllAsync();
        Task<Result<Contact?>> GetByIdAsync(string id);
        Task<Result<Contact?>> CreateAsync(ContactAddRequestDto dto);
        Task<Result<bool>> DeleteAsync(string id);

        Task<Result<bool>> UpdateAsync(string id, ContactAddRequestDto dto);
    }
}
