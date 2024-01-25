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


namespace PitchMatchUnitTests
{
    public class PitchControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        private readonly HttpClient _client;

        public PitchControllerTests()
        {
            _factory = new WebApplicationFactory<Program>();
            _client = _factory.CreateClient();
        }
        [Fact]
        public async Task GetPitch_WithValidId_ReturnsOk()
        {
            // Arrange
            var pitch = new Pitch
            {
                Id = 1,
                UserId = 1,
                Title = "test",
                Description = "test",
                Summary = "test",
                ImgUrl = "test",
                Goal= 1000,
                Yield = 1000,
            
            };

            var pitchResponse = await _client.PostAsJsonAsync("/pitch", pitch);
            pitchResponse.EnsureSuccessStatusCode();

            // Act
            var response = await _client.GetAsync($"/pitch/{pitch.Id}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            Assert.Equal("test", pitch.Title);
            Assert.Equal("test", pitch.Description);
            response.EnsureSuccessStatusCode();
        }
        [Fact]
        public async Task GetPitch_WithInvalidId_ReturnsNotFound()
        {
            // Arrange
            var pitchId = 0;

            // Act
            var response = await _client.GetAsync($"/pitch/{pitchId}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
        [Fact]
        public async Task CreatePitch_WithValidPitch_ReturnsCreated()
        {
            // Arrange
            var pitch = new Pitch
            {
                Id = 1,
                UserId = 1,
                Title = "test",
                Description = "test",
                Summary = "test",
                ImgUrl = "test",
                Goal = 1000,
                Yield = 1000,
            };

            // Act
            var response = await _client.PostAsJsonAsync("/pitch", pitch);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Created);
            response.EnsureSuccessStatusCode();
            Assert.Equal("test", pitch.Title);
            Assert.Equal("test", pitch.Description);
        }
        [Fact]
        public async Task UpdatePitch_ReturnsOk()
        {

            // Arrange
            var pitch = new Pitch
            {
                Id = 1,
                UserId = 1,
                Title = "test",
                Description = "test",
                Summary = "test",
                ImgUrl = "test",
                Goal = 1000,
                Yield = 1000,
            };
            var pitchUpdated = new Pitch
            {
                Id = 1,
                UserId = 1,
                Title = "test",
                Description = "heihei",
                Summary = "test",
                ImgUrl = "test",
                Goal = 1000,
                Yield = 1000,
            };

            var pitchResponse = await _client.PostAsJsonAsync("/pitch", pitch);
            pitchResponse.EnsureSuccessStatusCode();

            // Act
            var response = await _client.PutAsJsonAsync($"/pitch/{pitch.Id}", pitchUpdated);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
            response.EnsureSuccessStatusCode();
            Assert.Equal("test", pitchUpdated.Title);
            Assert.Equal("heihei", pitchUpdated.Description);
        }
        [Fact]
        public async Task UpdatePitch_WithInvalidId_ReturnsNotFound()
        {
            // Arrange
            var pitchId = 0;
            var pitch= new Pitch
            {
                Id = 1,
                UserId = 1,
                Title = "test",
                Description = "test",
                Summary = "test",
                ImgUrl = "test",
                Goal = 1000,
                Yield = 1000,
            };

            // Act
            var response = await _client.PutAsJsonAsync($"/pitch/{pitchId}", pitch);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
        [Fact]
        public async Task DeletePitch_WithValidId_ReturnsNoContent()
        {
            // Arrange
            var pitch = new Pitch
            {
                Id = 1,
                UserId = 1,
                Title = "test",
                Description = "test",
                Summary = "test",
                ImgUrl = "test",
                Goal = 1000,
                Yield = 1000,
            };

            var pitchResponse = await _client.PostAsJsonAsync("/pitch", pitch);
            pitchResponse.EnsureSuccessStatusCode();

            // Act
            var response = await _client.DeleteAsync($"/pitch/{pitch.Id}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }
        [Fact]
        public async Task DeletePitch_WithInvalidId_ReturnsNotFound()
        {
            // Arrange
            var pitchId = 0;

            // Act
            var response = await _client.DeleteAsync($"/pitch/{pitchId}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

    }
}
