using Microsoft.AspNetCore.Mvc;
using TelephoneBook.Application.Interfaces;
using TelephoneBook.Application.Models;
using TelephoneBook.PhoneService.DTOs;
using TelephoneBook.PhoneService.Services;

namespace TelephoneBook.PhoneService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactController : ControllerBase
    {
        private readonly IContactService _contactService;

        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var contacts = await _contactService.GetAllAsync();
            return Ok(contacts);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var contact = await _contactService.GetByIdAsync(id);
            if (contact == null)
                return NotFound();

            return Ok(contact);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ContactAddRequestDto contactDto)
        {
            var contact = await _contactService.CreateAsync(contactDto);
            return Ok(contact);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _contactService.DeleteAsync(id);
            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}
