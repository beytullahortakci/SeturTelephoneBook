using System.Net;
using Microsoft.AspNetCore.Mvc;
using TelephoneBook.Application.Interfaces;
using TelephoneBook.Domain.Common;
using TelephoneBook.Domain.Entities;

namespace TelephoneBook.ReportService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;

        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(Result<List<Report>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAll()
        {
            var Reports = await _reportService.GetAllReportsAsync();
            return Ok(Reports);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Result<Report>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Result<string>), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Result<string>), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetById(string id)
        {
            var Report = await _reportService.GetReportByIdAsync(id);
            if (Report == null)
                return NotFound();

            return Ok(Report);
        }


        [HttpGet("location-statistics/{location}")]
        [ProducesResponseType(typeof(Result<Report>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Result<string>), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Result<string>), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetLocationStatistics(string location)
        {
          
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(x => x.Errors)
                    .Select(x => x.ErrorMessage)
                    .ToList();

                return BadRequest(new { Errors = errors });
            }

            var Report = await _reportService.CreateReportAsync(location);
            return Ok(Report);
        }
    }
}
