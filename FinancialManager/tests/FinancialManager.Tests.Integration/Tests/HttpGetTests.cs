using FinancialManager.Shared.DTOs;
using FluentAssertions;
using System.Text.Json;
using System.Net;

namespace FinancialManager.Tests.Integration.Tests
{
    public class HttpGetTests : IClassFixture<TestingWebAppFactory>
    {
        private readonly HttpClient _client;
        private readonly JsonSerializerOptions _options;
        public HttpGetTests(TestingWebAppFactory factory)
        {
            _client = factory.CreateClient();
            _options = new() { PropertyNameCaseInsensitive = true };
        }

        [Fact]
        public async Task GetAllOperations_ReturnsFromDb()
        {
            using var response = await _client.GetAsync("api/FinancialOperations");
            response.EnsureSuccessStatusCode();
            var expected = new List<FinancialOperationDTO>
            {
                new FinancialOperationDTO { Id = 1, Amount = 100, DateTime = new DateTime(2008, 10, 10), Description = "abc", OperationTypeId = 1 },
                new FinancialOperationDTO { Id = 2, Amount = 100, DateTime = new DateTime(2007, 10, 10), Description = "bbc", OperationTypeId = 1 },
                new FinancialOperationDTO { Id = 3, Amount = 100, DateTime = new DateTime(2006, 10, 10), Description = "cbc", OperationTypeId = 2 },
                new FinancialOperationDTO { Id = 4, Amount = 100, DateTime = new DateTime(2005, 10, 10), Description = "dbc", OperationTypeId = 2 }
            };
            var actualData = JsonSerializer.Deserialize<List<FinancialOperationDTO>>(await response.Content.ReadAsStringAsync(), _options);
            actualData.Should().BeEquivalentTo(expected, opt => opt.ComparingByMembers<FinancialOperationDTO>());
        }

        [Fact]
        public async Task GetOperationById_ReturnsFromDb()
        {
            var expected = new FinancialOperationDTO { Id = 1, Amount = 100, DateTime = new DateTime(2008, 10, 10), Description = "abc", OperationTypeId = 1 };
            using var response = await _client.GetAsync("api/FinancialOperations/1");
            response.EnsureSuccessStatusCode();
            var actual = JsonSerializer.Deserialize<FinancialOperationDTO>(await response.Content.ReadAsStringAsync(), _options);
            actual.Should().BeEquivalentTo(expected, opt => opt.ComparingByMembers<FinancialOperationDTO>());
        }

        [Fact]
        public async Task GetOperationById_ObjectNotExist_Returns404()
        {
            using var response = await _client.GetAsync("api/FinancialOperations/10");
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task GetAllOperationTypes_ReturnsFromDb()
        {
            using var response = await _client.GetAsync("api/OperationTypes");
            response.EnsureSuccessStatusCode();
            var expected = new List<OperationTypeDTO>
            {
                new OperationTypeDTO { Id = 1, IsIncome = true, Name = "salary"},
                new OperationTypeDTO { Id = 2, IsIncome = false, Name = "rent" }
            };
            var actualData = JsonSerializer.Deserialize<List<OperationTypeDTO>>(await response.Content.ReadAsStringAsync(), _options);
            actualData.Should().BeEquivalentTo(expected, opt => opt.ComparingByMembers<OperationTypeDTO>());
        }

        [Fact]
        public async Task GetOperationTypeById_ReturnsFromDb()
        {
            var expected = new OperationTypeDTO { Id = 1, IsIncome = true, Name = "salary" };
            using var response = await _client.GetAsync("api/OperationTypes/1");
            response.EnsureSuccessStatusCode();
            var actual = JsonSerializer.Deserialize<OperationTypeDTO>(await response.Content.ReadAsStringAsync(), _options);
            actual.Should().BeEquivalentTo(expected, opt => opt.ComparingByMembers<OperationTypeDTO>());
        }

        [Fact]
        public async Task GetOperationTypeById_ObjectNotExist_Returns404()
        {
            using var response = await _client.GetAsync("api/OperationTypes/10");
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

    }
}
