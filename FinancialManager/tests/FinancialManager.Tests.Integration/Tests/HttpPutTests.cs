using FinancialManager.Shared.DTOs;
using System.Net.Http.Json;
using System.Net;
using System.Text.Json;
using FluentAssertions;

namespace FinancialManager.Tests.Integration.Tests
{
    public class HttpPutTests : IClassFixture<TestingWebAppFactory>
    {
        private readonly HttpClient _client;
        private readonly JsonSerializerOptions _options;
        public HttpPutTests(TestingWebAppFactory factory)
        {
            _client = factory.CreateClient();
            _options = new() { PropertyNameCaseInsensitive = true };
        }

        [Fact]
        public async Task PutFinancialOperation_PassedNotExistingOperationTypeId_Returns404()
        {
            var operationToPut = new FinancialOperationDTO { OperationTypeId = 100, DateTime = DateTime.Now, Amount = 300, Description = "abc", Id = 1 };
            using var respsonse = await _client.PutAsJsonAsync("api/FinancialOperations", operationToPut);
            respsonse.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task PutFinancialOperation_PassedIdOfNotExistingObject_Returns404()
        {
            var operationToPut = new FinancialOperationDTO { OperationTypeId = 1, DateTime = DateTime.Now, Amount = 300, Description = "abc", Id = 100 };
            using var respsonse = await _client.PutAsJsonAsync("api/FinancialOperations", operationToPut);
            respsonse.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task PutOperationType_PassedIdOfNotExistingObject_Returns404()
        {
            var operationTypeToPut = new OperationTypeDTO { Id = 100, Name = "10", IsIncome = true };
            using var respsonse = await _client.PutAsJsonAsync("api/OperationTypes", operationTypeToPut);
            respsonse.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

    }
}
