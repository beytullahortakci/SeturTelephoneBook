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

        public Task<Result<List<Contact>>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Result<Contact?>> GetByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<Contact?>> CreateAsync(ContactAddRequestDto dto)
        {
            var model = _mapper.Map<ContactAddRequestDto, Contact>(dto);

            await _repository.AddAsync(model);
            
            return new Result<Contact?>(true,null,null);
        }

        public Task<Result<bool>> DeleteAsync(string id)
        {
            throw new NotImplementedException();
        }
    }
}
