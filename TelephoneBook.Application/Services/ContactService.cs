using AutoMapper;
using TelephoneBook.Application.Interfaces;
using TelephoneBook.Application.Models;
using TelephoneBook.Domain.Common;
using TelephoneBook.Domain.Entities;
using TelephoneBook.Infrastructure.Interfaces;

namespace TelephoneBook.Application.Services
{
    public class ContactService : IContactService
    {
        private readonly IRepository<Contact> _repository;
        private readonly IMapper _mapper;

        public ContactService(IRepository<Contact> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }


        public async Task<Result<List<Contact>>> GetAllAsync()
        {
            var data = await _repository.GetAllAsync();
            return new Result<List<Contact>>(true, null, data.ToList());
        }

        public async Task<Result<Contact?>> GetByIdAsync(string id)
        {
            var contact = await _repository.GetByIdAsync(id);
            if (contact == null)
                return new Result<Contact?>(false, "Contact not found", null);

            return new Result<Contact?>(true, null, contact);
        }

        public async Task<Result<Contact?>> CreateAsync(ContactAddRequestDto dto)
        {
            var model = _mapper.Map<Contact>(dto);
            await _repository.AddAsync(model);
            return new Result<Contact?>(true, "Contact created successfully", model);
        }

        public async Task<Result<bool>> DeleteAsync(string id)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null)
                return new Result<bool>(false, "Contact not found", false);

            await _repository.DeleteAsync(id);
            return new Result<bool>(true, "Contact deleted", true);
        }

        public async Task<Result<bool>> UpdateAsync(string id, ContactAddRequestDto dto)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null)
                return new Result<bool>(false, "Contact not found", false);

            var updatedContact = _mapper.Map(dto, existing);
            await _repository.UpdateAsync(id, updatedContact);
            return new Result<bool>(true, "Contact updated successfully", true);
        }
    }
}
