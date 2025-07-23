using AutoMapper;
using MongoDB.Driver;
using TelephoneBook.Application.Interfaces;
using TelephoneBook.Application.Models;
using TelephoneBook.Domain.Common;
using TelephoneBook.Domain.Entities;
using TelephoneBook.Infrastructure.Interfaces;

namespace TelephoneBook.Application.Services
{
    public class ContactDetailService : IContactDetailService
    {
        private readonly IRepository<ContactDetail> _repository;
        private readonly IMapper _mapper;

        public ContactDetailService(IRepository<ContactDetail> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        
        public async Task<Result<List<ContactDetail>>> GetAllDetailAsync()
        {
            var data = await _repository.GetAllAsync();
            return new Result<List<ContactDetail>>(true, null, data.ToList());
        }

        public async Task<Result<ContactDetail?>> GetDetailByContactIdAsync(string id)
        {
            var filter = Builders<ContactDetail>.Filter.Eq(x => x.ContactId, id);

            var contact = await _repository.FilterFirstAsync(filter);
          
            if (contact == null)
                return new Result<ContactDetail?>(false, "Contact Detail not found", null);

            return new Result<ContactDetail?>(true, null, contact);
        }

        public async Task<Result<ContactDetail?>> CreateDetailAsync(ContactDetailAddRequestDto dto)
        {
            var model = _mapper.Map<ContactDetail>(dto);
            await _repository.AddAsync(model);
            return new Result<ContactDetail?>(true, "Contact Detail created successfully", model);
        }

        public async Task<Result<ContactDetail?>> UpdateDetailAsync(ContactDetailAddRequestDto dto)
        {
            var existing = await _repository.GetByIdAsync(dto.ContactId);
            if (existing == null)
                return new Result<ContactDetail?>(false, "Contact not found", null);

            var updatedContact = _mapper.Map(dto, existing);
            await _repository.UpdateAsync(existing.Id, updatedContact);
            return new Result<ContactDetail>(true, "Contact updated successfully", updatedContact)!;
        }

        public async Task<Result<bool>> DeleteDetailAsync(string id)
        {
            var filter = Builders<ContactDetail>.Filter.Eq(x => x.ContactId, id);

            var contact = await _repository.FilterFirstAsync(filter);

            if (contact == null)
                return new Result<bool>(false, "Contact Detail not found",false);
            await _repository.DeleteAsync(contact.Id);

            return new Result<bool>(true, "Contact Detail deleted", true);
        }
    }
}
