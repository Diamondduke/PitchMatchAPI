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
    public class InvestmentControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        private readonly HttpClient _client;

        public InvestmentControllerTests()
        {
            _factory = new WebApplicationFactory<Program>();
            _client = _factory.CreateClient();
        }

        [Fact]
        public async Task GetInvestment_WithValidId_ReturnsOk()
        {
            // Arrange
            var investment = new Investment
            {
                Id = 1,
                PitchId = 1,
                UserId = 1,
                Amount = 1000
            };

            var investmentResponse = await _client.PostAsJsonAsync("/Investment", investment);
            investmentResponse.EnsureSuccessStatusCode();

            // Act
            var response = await _client.GetAsync($"/Investment/{investment.Id}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task GetInvestment_WithInvalidId_ReturnsNotFound()
        {
            // Arrange
            var investmentId = 0;

            // Act
            var response = await _client.GetAsync($"/investment/{investmentId}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task CreatePitch_WithValidInvestment_ReturnsCreated()
        {
            // Arrange
            var pitch = new Pitch
            {
                Id = 1,
                Title = "Test Pitch",
                Description = "Test Description",
                Funding = 1000,
                UserId = 1,
                Views = 0,
                VideoUrl = "https://www.youtube.com/watch?v=QH2-TGUlwu4",
                ImgUrl = "https://www.youtube.com/watch?v=QH2-TGUlwu4",
                Summary = "Test Summary",
                Categories = "Test Categories",
                Goal = 10000,
                Latitude = 0.1,
                Location = "Test Location",
                Longitude = 0.1,
                Yield = 110,

            };
            var investment = new Investment
            {
                PitchId = 1,
                UserId = 1,
                Amount = 1000
            };

            // Act
            var createdPitch = await _client.PostAsJsonAsync("/pitch", pitch);
            createdPitch.EnsureSuccessStatusCode();

            var createdInvestment = await _client.PostAsJsonAsync("/investment", investment);
            createdInvestment.EnsureSuccessStatusCode();

            // Assert
            createdInvestment.StatusCode.Should().Be(HttpStatusCode.Created);
            createdPitch.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [Fact]
        public async Task GetInvestmentsForPitch_WithValidPitchId_ReturnsOk()
        {
            // Arrange
            var pitchId = 1;

            // Act
            var response = await _client.GetAsync($"/investment/pitch/{pitchId}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
        [Fact]
        public async Task GetInvestmentsForPitch_WithInvalidPitchId_ReturnsNotFound()
        {
            // Arrange
            var pitchId = 1000;

            // Act
            var response = await _client.GetAsync($"/investment/pitch/{pitchId}");

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}