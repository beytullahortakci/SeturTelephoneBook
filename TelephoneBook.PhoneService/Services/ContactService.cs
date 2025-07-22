using Microsoft.EntityFrameworkCore;
using TelephoneBook.PhoneService.Data;
using TelephoneBook.PhoneService.DTOs;
using TelephoneBook.PhoneService.Models;

namespace TelephoneBook.PhoneService.Services
{
    public class ContactService : IContactService
    {
        private readonly AppDbContext _context;

        public ContactService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Contact>> GetAllAsync()
        {
            return await _context.Contacts.Include(c => c.ContactInfos).ToListAsync();
        }

        public async Task<Contact?> GetByIdAsync(Guid id)
        {
            return await _context.Contacts.Include(c => c.ContactInfos)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Contact> CreateAsync(ContactDto dto)
        {
            var contact = new Contact
            {
                Id = Guid.NewGuid(),
                FullName = dto.FullName,
                Company = dto.Company,
                ContactInfos = dto.ContactInfos.Select(ci => new ContactInfo
                {
                    Id = Guid.NewGuid(),
                    Type = ci.Type,
                    Value = ci.Value
                }).ToList()
            };

            _context.Contacts.Add(contact);
            await _context.SaveChangesAsync();
            return contact;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var contact = await _context.Contacts.Include(c => c.ContactInfos)
                .FirstOrDefaultAsync(c => c.Id == id);
            if (contact == null) return false;

            _context.ContactInfos.RemoveRange(contact.ContactInfos);
            _context.Contacts.Remove(contact);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
