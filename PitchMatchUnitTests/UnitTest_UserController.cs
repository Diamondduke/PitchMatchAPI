using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using FluentAssertions;
using System.Threading.Tasks;
using PitchMatch;
using PitchMatch.Data.Models;
using System.Net.Http.Json;
using Microsoft.Extensions.DependencyInjection;
using PitchMatch.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PitchMatchUnitTests
{
    public class UserControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly CustomWebApplicationFactory<Program> _factory;

        public UserControllerTests(CustomWebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GetAllUsers_ReturnsOk()
        {
            var client = _factory.CreateClient();
            // Arrange
            var request = new HttpRequestMessage(HttpMethod.Get, "/user");

            // Act
            var response = await client.SendAsync(request);

            // Assert
            response.EnsureSuccessStatusCode();
        }


        [Fact]
        public async Task PostAndGetUser_ReturnsOk()
        {
            var client = _factory.CreateClient();

            var user = new User
            {
                Name = "test",
                Email = "AgustinaScapusioasd@test.com",
                Password = "test123",
            };

            //Act
            //Create user
            var response = await client.PostAsJsonAsync("/user", user);

            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.Created);

        }

        //[Fact]
        //public async Task GetUser_WithValidId_ReturnsOk()
        //{
        //    var client = _factory.CreateClient();
        //    var userId = 97;

        //    // Act
        //    var response = await client.GetAsync($"user/{userId}?userId={userId}");

        //    // Assert
        //    response.StatusCode.Should().Be(HttpStatusCode.OK);

        //}
        //[Fact]
        //public async Task GetUser_WithInvalidId_ReturnsNotFound()
        //{

        //    var client = _factory.CreateClient();
        //    // Arrange
        //    var userId = 0;

        //    // Act
        //    var response = await client.GetAsync($"user/{userId}?userId={userId}");

        //    // Assert
        //    response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        //}

        [Fact]
        public async Task DeleteUser_ReturnsOk()
        {
            // Arrange
            var client = _factory.CreateClient();
            var userId= 66;
            //Act
            var deleteUserResponse = await client.DeleteAsync($"/user?Id={userId}");
            deleteUserResponse.EnsureSuccessStatusCode();
            deleteUserResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        }


        [Fact]
        public async Task UpdateUser_ReturnsOk()
        {
            var client = _factory.CreateClient();
            var user = new User
            {
                Name = "test",
                Email = "agustinaaaa@gmail.com",
                Password = "test123"
            };
            var userID = 95;

            
            var updateUserResponse = await client.PutAsJsonAsync($"/User?id={userID}", user);
            updateUserResponse.EnsureSuccessStatusCode();
        }
    }
}
