using FinancialManager.Shared.DTOs;
using FluentAssertions;
using System.Text.Json;
using System.Net;
using FinancialManager.Shared.Exceptions.InfrastructureExceptions;
using FinancialManager.Shared.Models;

namespace FinancialManager.Tests.Integration.Tests.HttpDeleteTests
{
    public class FinancialOperationsDeleteTests : IClassFixture<TestingWebAppFactory>
    {
        private readonly HttpClient _client;
        private readonly JsonSerializerOptions _options;
        public FinancialOperationsDeleteTests(TestingWebAppFactory factory)
        {
            _client = factory.CreateClient();
            _options = new() { PropertyNameCaseInsensitive = true };
        }

        [Fact]
        public async Task DeleteOperation_ReturnsObjectBack()
        {
            using var response = await _client.DeleteAsync("api/FinancialOperations/1");
            response.EnsureSuccessStatusCode();
            var expected = new FinancialOperationDTO { Id = 1, Amount = 100, DateTime = new DateTime(2008, 10, 10), Description = "abc", OperationTypeId = 1 };
            var actual = JsonSerializer.Deserialize<FinancialOperationDTO>(await response.Content.ReadAsStringAsync(), _options);
            expected.Should().BeEquivalentTo(actual, opt => opt.ComparingByMembers<FinancialOperationDTO>());
            using var itemsAfterDelete = await _client.GetAsync("api/FinancialOperations");
            itemsAfterDelete.EnsureSuccessStatusCode();
            var actualData = JsonSerializer.Deserialize<List<FinancialOperationDTO>>(await itemsAfterDelete.Content.ReadAsStringAsync(), _options);
            actualData?.Count.Should().Be(3);
        }

        [Fact]
        public async Task DeleteOperation_PassedIdOfNotExistingObject_Returns404()
        {
            using var response = await _client.DeleteAsync("api/FinancialOperations/100");
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}
