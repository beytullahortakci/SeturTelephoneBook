using AutoMapper;
using TelephoneBook.Application.Models;
using TelephoneBook.Domain.Entities;

namespace TelephoneBook.Application.Mapping
{
    public class GeneralMappings : Profile
    {
        public GeneralMappings()
        {
            CreateMap<Contact, ContactAddRequestDto>().ReverseMap();
            CreateMap<ContactDetail, ContactDetailAddRequestDto>().ReverseMap();
        }
    }
}
