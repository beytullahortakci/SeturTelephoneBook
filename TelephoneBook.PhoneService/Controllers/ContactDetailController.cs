using Microsoft.AspNetCore.Mvc;
using System.Net;
using TelephoneBook.Application.Interfaces;
using TelephoneBook.Application.Models;
using TelephoneBook.Domain.Common;
using TelephoneBook.Domain.Entities;

namespace TelephoneBook.PhoneService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactDetailController : ControllerBase
    {
        private readonly IContactDetailService _contactDetailService;

        public ContactDetailController(IContactDetailService contactDetailService)
        {
            _contactDetailService = contactDetailService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(Result<List<ContactDetail>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAll()
        {
            var contacts = await _contactDetailService.GetAllDetailAsync();
            return Ok(contacts);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Result<ContactDetail>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Result<string>), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetDetailByContactIdAsync(string id)
        {
            var contact = await _contactDetailService.GetDetailByContactIdAsync(id);
            if (contact == null)
                return NotFound();

            return Ok(contact);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Result<ContactDetail>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Result<string>), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Result<string>), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Create([FromBody] ContactDetailAddRequestDto contactDto)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(x => x.Errors)
                    .Select(x => x.ErrorMessage)
                    .ToList();

                return BadRequest(new { Errors = errors });
            }

            var contact = await _contactDetailService.CreateDetailAsync(contactDto);
            return Ok(contact);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Result<bool>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Result<string>), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeleteWithContactId(string id)
        {
            var result = await _contactDetailService.DeleteDetailAsync(id);
            if (!result.IsSuccess)
                return NotFound();

            return Ok(result);
        }
    }
}
