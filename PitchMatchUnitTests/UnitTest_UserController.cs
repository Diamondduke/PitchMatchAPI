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
    public class UserControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        private readonly HttpClient _client;

        public UserControllerTests()
        {
            _factory = new WebApplicationFactory<Program>();
            _client = _factory.CreateClient();

        }

        [Fact]
        public async Task GetAllUsers_ReturnsOk()
        {
            // Arrange
            var request = new HttpRequestMessage(HttpMethod.Get, "/user");

            // Act
            var response = await _client.SendAsync(request);

            // Assert
            response.EnsureSuccessStatusCode();
        }
       

        [Fact]
        public async Task PostAndGetUser_ReturnsOk()
        {
            // Arrange
            var user = new User
            {
                Id = 10,
                Name = "test",
                Email = "test@test.com",
                Password = "test123",

            };

            // Act
            // Create user
            var createUserResponse = await _client.PostAsJsonAsync("/user", user);
            createUserResponse.EnsureSuccessStatusCode();

            // Retrieve user
            var getUserResponse = await _client.GetAsync("/user/10");
            getUserResponse.EnsureSuccessStatusCode();

            // Assert
            var returnedUser = await getUserResponse.Content.ReadFromJsonAsync<User>();
            Assert.NotNull(returnedUser);
            Assert.Equal(user.Id, returnedUser.Id);
            Assert.Equal(user.Name, returnedUser.Name);
        }

        [Fact]
        public async Task DeleteUser_ReturnsOk()
        {
            // Arrange
            var user = new User
            {
                Id = 62,
                Name = "test",
                Email = "taylor123@swift.com",
                Password = "test123"
            };


            // Act
            // Create user
            var createUserResponse = await _client.PostAsJsonAsync("/user", user);
            createUserResponse.EnsureSuccessStatusCode();
            // Delete user
            var deleteUserResponse = await _client.DeleteAsync("/user/62");
            deleteUserResponse.EnsureSuccessStatusCode();

            // Assert
            var getUserResponse = await _client.GetAsync("/user/62");
            Assert.Equal(HttpStatusCode.NotFound, getUserResponse.StatusCode);
        }

        [Fact]
        public async Task DeleteUser_ReturnsNotFound()
        {
            
            // Delete user
            var deleteUserResponse = await _client.DeleteAsync("/user/0");
            deleteUserResponse.EnsureSuccessStatusCode();

            // Assert
            var getUserResponse = await _client.GetAsync("/user/63");
            Assert.Equal(HttpStatusCode.NotFound, getUserResponse.StatusCode);

        }
        [Fact]
        public async Task UpdateUser_ReturnsOk()
        {
            // Arrange
            var user = new User
            {
                Id = 63,
                Name = "test",
                Email = "agustina@test.com",
                Password = "test123"
            };

            // Act
            // Create user
            var createUserResponse = await _client.PostAsJsonAsync("/user", user);
            createUserResponse.EnsureSuccessStatusCode();

            // Update user
            var updateUserResponse = await _client.PutAsJsonAsync("/user/63", user);
            updateUserResponse.EnsureSuccessStatusCode();

            // Assert
            var getUserResponse = await _client.GetAsync("/user/63");
            getUserResponse.EnsureSuccessStatusCode();
            var returnedUser = await getUserResponse.Content.ReadFromJsonAsync<User>();
            Assert.NotNull(returnedUser);
            Assert.Equal(user.Id, returnedUser.Id);
            Assert.Equal(user.Name, returnedUser.Name);
        }
        [Fact]
        public async Task UpdateUser_ReturnsNotFound()
        {
            // Arrange
            var user = new User
            {
                Id = 64,
                Name = "test",
                Email = "agustina@gmail.com",
                Password = "test123"
            };

            // Act
            // Create user
            var createUserResponse = await _client.PostAsJsonAsync("/user", user);
            createUserResponse.EnsureSuccessStatusCode();

            // Update user
            var updateUserResponse = await _client.PutAsJsonAsync("/user/65", user);
            updateUserResponse.EnsureSuccessStatusCode();

            // Assert
            var getUserResponse = await _client.GetAsync("/user/65");
            Assert.Equal(HttpStatusCode.NotFound, getUserResponse.StatusCode);

        }
    }
}
