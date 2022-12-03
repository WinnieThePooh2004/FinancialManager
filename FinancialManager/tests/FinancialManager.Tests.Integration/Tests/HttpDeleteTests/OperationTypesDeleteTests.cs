using FinancialManager.Shared.DTOs;
using FluentAssertions;
using System.Net;
using System.Text.Json;

namespace FinancialManager.Tests.Integration.Tests.HttpDeleteTests
{
    public class OperationTypesDeleteTests : IClassFixture<TestingWebAppFactory>
    {
        private readonly HttpClient _client;
        private readonly JsonSerializerOptions _options;
        public OperationTypesDeleteTests(TestingWebAppFactory factory)
        {
            _client = factory.CreateClient();
            _options = new() { PropertyNameCaseInsensitive = true };
        }

        [Fact]
        public async Task DeleteOperation_ReturnsObjectBack()
        {
            using var response = await _client.DeleteAsync("api/OperationTypes/1");
            response.EnsureSuccessStatusCode();

            var expected = new OperationTypeDTO { Id = 1, IsIncome = true, Name = "salary" };
            var actual = JsonSerializer.Deserialize<OperationTypeDTO>(await response.Content.ReadAsStringAsync(), _options);
            expected.Should().BeEquivalentTo(actual, opt => opt.ComparingByMembers<OperationTypeDTO>());

            using var operationTypesAfterDelete = await _client.GetAsync("api/OperationTypes");
            operationTypesAfterDelete.EnsureSuccessStatusCode();
            var actualOperationTypes = JsonSerializer
                .Deserialize<List<OperationTypeDTO>>(await operationTypesAfterDelete.Content.ReadAsStringAsync(), _options);
            actualOperationTypes?.Count.Should().Be(1);

            using var operationsAfterDelete = await _client.GetAsync("api/FinancialOperations");
            operationsAfterDelete.EnsureSuccessStatusCode();
            var actualOperations = JsonSerializer
                .Deserialize<List<FinancialOperationDTO>>(await operationsAfterDelete.Content.ReadAsStringAsync(), _options);
            actualOperations?.Count.Should().Be(2);
        }

        [Fact]
        public async Task DeleteOperation_PassedIdOfNotExistingObject_Returns404()
        {
            using var response = await _client.DeleteAsync("api/OperationTypes/100");
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}
