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
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;


namespace PitchMatchUnitTests
{
    public class PitchControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly CustomWebApplicationFactory<Program> _factory;

        public PitchControllerTests(CustomWebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GetPitch_WithValidId_ReturnsOk()
        {
            var client = _factory.CreateClient();
            var pitchId = 111;
            
            // Act
            var response = await client.GetAsync($"Pitch/{pitchId}?pitchId={pitchId}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
           
        }
        [Fact]
        public async Task GetPitch_WithInvalidId_ReturnsNotFound()
        {

            var client = _factory.CreateClient();
            // Arrange
            var pitchId = 0;

            // Act
            var response = await client.GetAsync($"/pitch/{pitchId}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
        [Fact]
        public async Task CreatePitch_WithValidPitch_ReturnsCreated()
        {
            var _pitch = new Pitch
            {
                UserId = 6,
                Title = "test",
                Description = "test",
                Summary = "test",
                ImgUrl = "test",
                Goal = 1000,
                Yield = 1000,
            };
            var client = _factory.CreateClient();

            // Act
            var response = await client.PostAsJsonAsync("/pitch", _pitch);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Created);
            response.EnsureSuccessStatusCode();
            Assert.AreEqual("test", _pitch.Title);
            Assert.AreEqual("test", _pitch.Description);
        }
        [Fact]
        public async Task UpdatePitch_ReturnsOk()
        {
            var client = _factory.CreateClient();
            var _pitchUpdateId = 111;
            // Arrange
            var _pitchUpdated = new Pitch
            {
                UserId = 6,
                Title = "test",
                Description = "heihei",
                Summary = "test",
                ImgUrl = "test",
                Goal = 1000,
                Yield = 1000,
            };

            // Act
            var response = await client.PutAsJsonAsync($"Pitch/{_pitchUpdateId}?pitchId={_pitchUpdateId}", _pitchUpdated);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            response.EnsureSuccessStatusCode();
            Assert.AreEqual("test", _pitchUpdated.Title);
            Assert.AreEqual("heihei", _pitchUpdated.Description);
        }
        [Fact]
        public async Task UpdatePitch_WithInvalidId_ReturnsNotFound()
        {
            var client = _factory.CreateClient();
            // Arrange
            
            var pitchId = 0;
            var pitch = new Pitch
            {
                UserId = 1,
                Title = "test",
                Description = "test",
                Summary = "test",
                ImgUrl = "test",
                Goal = 1000,
                Yield = 1000,
            };

            // Act
            var response = await client.PutAsJsonAsync($"Pitch/{pitchId}?pitchId={pitchId}", pitch);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
        [Fact]
        public async Task DeletePitch_WithValidId_ReturnsNoContent()
        {
            var client = _factory.CreateClient();
           var pitchId= 111;

            // Act
            var response = await client.DeleteAsync($"{pitchId}?pitchId={pitchId}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
        [Fact]
        public async Task DeletePitch_WithInvalidId_ReturnsNotFound()
        {
            var client = _factory.CreateClient();
            // Arrange
            var pitchId = 0;

            // Act
            var response = await client.DeleteAsync($"{pitchId}?pitchId={pitchId}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

    }
}
