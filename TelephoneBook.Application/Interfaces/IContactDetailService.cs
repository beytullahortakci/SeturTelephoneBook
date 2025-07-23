using TelephoneBook.Application.Models;
using TelephoneBook.Domain.Common;
using TelephoneBook.Domain.Entities;

namespace TelephoneBook.Application.Interfaces
{
    public interface IContactDetailService
    {
        Task<Result<List<ContactDetail>>> GetAllDetailAsync();
        Task<Result<ContactDetail?>> GetDetailByContactIdAsync(string id);
        Task<Result<ContactDetail?>> CreateDetailAsync(ContactDetailAddRequestDto dto);

        Task<Result<ContactDetail?>> UpdateDetailAsync(ContactDetailAddRequestDto dto);
        Task<Result<bool>> DeleteDetailAsync(string id);
    }
}
