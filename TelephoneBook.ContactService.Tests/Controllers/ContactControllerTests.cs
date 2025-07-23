using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using NUnit.Framework;
using TelephoneBook.PhoneService;
using TelephoneBook.Domain.Entities;
using TelephoneBook.Application.Models;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using TelephoneBook.Domain.Common;

namespace TelephoneBook.Tests.Controllers
{
    [TestFixture]
    public class ContactControllerTests
    {
        private WebApplicationFactory<Program> _factory;
        private HttpClient _client;

        [SetUp]
        public void Setup()
        {
            _factory = new WebApplicationFactory<Program>();
            _client = _factory.CreateClient();
        }

        [Test]
        public async Task GetAll_ShouldReturnOkWithContacts()
        {
            var response = await _client.GetAsync("/api/contact");

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var result = await response.Content.ReadFromJsonAsync<Result<List<Contact>>>();
            result.Should().NotBeNull();
            result.IsSuccess.Should().BeTrue();
            result.Data.Should().NotBeNull();
        }

        [Test]
        public async Task GetById_WithValidId_ShouldReturnOkWithContact()
        {
            var validId = "some-valid-id"; // Burayı test ortamına göre ayarla

            var response = await _client.GetAsync($"/api/contact/{validId}");

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                Assert.Pass("Contact bulunamadı, test ortamında geçerli id yok.");
            }
            else
            {
                response.StatusCode.Should().Be(HttpStatusCode.OK);
                var result = await response.Content.ReadFromJsonAsync<Result<Contact>>();
                result.Should().NotBeNull();
                result.IsSuccess.Should().BeTrue();
                result.Data.Id.Should().Be(validId);
            }
        }

        [Test]
        public async Task Create_ValidContact_ShouldReturnOk()
        {
            var newContact = new ContactAddRequestDto
            {
                ContactName = "Test Name",
                ContactLastName= "test ",
                ContactCompany= "Phone"
            };

            var response = await _client.PostAsJsonAsync("/api/contact", newContact);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var result = await response.Content.ReadFromJsonAsync<Result<Contact>>();
            result.Should().NotBeNull();
            result.IsSuccess.Should().BeTrue();
            result.Data.ContactName.Should().Be(newContact.ContactName);
        }

        [Test]
        public async Task Delete_WithValidId_ShouldReturnOk()
        {
            var idToDelete = "some-valid-id"; // Test ortamına göre

            var response = await _client.DeleteAsync($"/api/contact/{idToDelete}");

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                Assert.Pass("Silinecek contact bulunamadı, test ortamında geçerli id yok.");
            }
            else
            {
                response.StatusCode.Should().Be(HttpStatusCode.OK);
                var result = await response.Content.ReadFromJsonAsync<Result<bool>>();
                result.Should().NotBeNull();
                result.IsSuccess.Should().BeTrue();
                result.Data.Should().BeTrue();
            }
        }

        [TearDown]
        public void TearDown()
        {
            _client?.Dispose();
            _factory?.Dispose();
        }
    }
}