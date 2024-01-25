using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using FluentAssertions;
using System.Threading.Tasks;
using PitchMatch;
using PitchMatch.Data.Models;
using System.Net.Http.Json;


namespace PitchMatchUnitTests
{
    public class PersonalDataControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        private readonly HttpClient _client;

        public PersonalDataControllerTests()
        {
            _factory = new WebApplicationFactory<Program>();
            _client = _factory.CreateClient();
        }

        [Fact]
        public async Task CreatePersonalData_ReturnsOk()
        {
            // Arrange
            var PersonalData = new PersonalData
            {
                Id = 1,
                UserId = 1,
                PhoneNumber = "12345678",
                PersonNr = "12345678910",
                Address = "test",
                Longitude = 1,
                Latitude = 1,
                IsVerified = true
            };

            // Act
            var PersonalDataResponse = await _client.PostAsJsonAsync("/PersonalData", PersonalData);

            // Assert
            PersonalDataResponse.EnsureSuccessStatusCode();
        }
        [Fact]
        public async Task GetPersonalData_WithValidId_ReturnsOk()
        {
            // Arrange
            var PersonalData = new PersonalData
            {
                Id = 1,
                UserId = 1,
                PhoneNumber = "12345678",
                PersonNr = "12345678910",
                Address = "test",
                Longitude = 1,
                Latitude = 1,
                IsVerified = true
            };

            var PersonalDataResponse = await _client.PostAsJsonAsync("/PersonalData", PersonalData);
            PersonalDataResponse.EnsureSuccessStatusCode();

            // Act
            var response = await _client.GetAsync($"/PersonalData/{PersonalData.Id}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
        [Fact]
        public async Task GetPersonalData_WithInvalidId_ReturnsNotFound()
        {
            // Arrange
            var PersonalDataId = 0;

            // Act
            var response = await _client.GetAsync($"/PersonalData/{PersonalDataId}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task DeletePersonalData_ReturnsOk()
        {
            // Arrange
            var PersonalData = new PersonalData
            {
                Id = 1,
                UserId = 1,
                PhoneNumber = "12345678",
                PersonNr = "12345678910",
                Address = "test",
                Longitude = 1,
                Latitude = 1,
                IsVerified = true
            };
            //Act
            var PersonalDataResponse = await _client.PostAsJsonAsync("/PersonalData", PersonalData);
            PersonalDataResponse.EnsureSuccessStatusCode();
            // Delete user
            var deletePersonalDataResponse = await _client.DeleteAsync("/PersonalData/1");
            deletePersonalDataResponse.EnsureSuccessStatusCode();

            // Assert
            var getPersonalDataResponse = await _client.GetAsync("/PersonalData/1");
            Assert.Equal(HttpStatusCode.NotFound, getPersonalDataResponse.StatusCode);
        }
    }
        
} 
           
