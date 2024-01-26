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
    public class PersonalDataControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly CustomWebApplicationFactory<Program> _factory;

        public PersonalDataControllerTests(CustomWebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }
        [Fact]
        public async Task CreatePersonalData_ReturnsOk()
        {
            var client = _factory.CreateClient();
            // Arrange
            var PersonalData = new PersonalData
            {
                UserId = 97,
                PhoneNumber = "12345678",
                PersonNr = "12345678910",
                Address = "test",
                Longitude = 1,
                Latitude = 1,
                IsVerified = true
            };

            // Act
            var PersonalDataResponse = await client.PostAsJsonAsync("/PersonalData", PersonalData);

            // Assert
            PersonalDataResponse.EnsureSuccessStatusCode();
        }
        [Fact]
        public async Task GetPersonalData_WithValidId_ReturnsOk()
        {
            // Arrange
            var client = _factory.CreateClient();
            var PersonalDataId = 14;

            // Act
            var response = await client.GetAsync($"/PersonalData/{PersonalDataId}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
        [Fact]
        public async Task GetPersonalData_WithInvalidId_ReturnsNotFound()
        {
            // Arrange
            var client = _factory.CreateClient();
            var PersonalDataId = 6;

            // Act
            var response = await client.GetAsync($"/PersonalData/{PersonalDataId}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task DeleteUser_ReturnsOk()
        {
            // Arrange
            var client = _factory.CreateClient();
            var userId = 97;
            //Act
            var deleteUserResponse = await client.DeleteAsync($"/PersonalData?userId={userId}");
            deleteUserResponse.EnsureSuccessStatusCode();
            deleteUserResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        }
        [Fact]
        public async Task UpdateUserPersonalData_ReturnsOk()
        {
            var client = _factory.CreateClient();
            var PersonalData = new PersonalData
            {
                UserId = 97,
                PhoneNumber = "12345678",
                PersonNr = "12345678910",
                Address = "test",
                Longitude = 1,
                Latitude = 1,
                IsVerified = true
            };
            var response = await client.PutAsJsonAsync($"/PersonalData", PersonalData);
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
        
} 
           
