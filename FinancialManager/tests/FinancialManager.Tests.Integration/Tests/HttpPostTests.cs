using System.Net;
using FinancialManager.Shared.DTOs;
using FluentAssertions;
using System.Net.Http.Json;
using System.Text.Json;

namespace FinancialManager.Tests.Integration.Tests
{
    public class HttpPostTests : IClassFixture<TestingWebAppFactory>
    {
        private readonly HttpClient _client;
        private readonly JsonSerializerOptions _options;

        public HttpPostTests(TestingWebAppFactory factory)
        {
            _client = factory.CreateClient();
            _options = new() { PropertyNameCaseInsensitive = true };
        }

        [Fact]
        public async Task PostFinancialOperation_ReturnsItBack_AddToDb()
        {
            var operationToPost = new FinancialOperationDTO { OperationTypeId = 1, DateTime = DateTime.Now, Amount = 300, Description = "abc", Id = 5 };

            using var respsonse = await _client.PostAsJsonAsync("api/FinancialOperations", operationToPost);
            respsonse.EnsureSuccessStatusCode();
            var responseObject = JsonSerializer.Deserialize<FinancialOperationDTO>(await respsonse.Content.ReadAsStringAsync(), _options);

            using var responseWithObjectFromDb = await _client.GetAsync("api/FinancialOperations/5");
            responseWithObjectFromDb.EnsureSuccessStatusCode();
            var objectFromDb = JsonSerializer.Deserialize<FinancialOperationDTO>(await responseWithObjectFromDb.Content.ReadAsStringAsync(), _options);
            responseObject.Should().BeEquivalentTo(objectFromDb, opt => opt.ComparingByMembers<FinancialOperationDTO>());
        }

        [Fact]
        public async Task PostFinancialOperation_PassedNotExistingOperationTypeId()
        {
            var operationToPost = new FinancialOperationDTO { OperationTypeId = 100, DateTime = DateTime.Now, Amount = 300, Description = "abc", Id = 5 };
            using var respsonse = await _client.PostAsJsonAsync("api/FinancialOperations", operationToPost);
            respsonse.StatusCode.Should().Be(HttpStatusCode.NotFound);

        }

        [Fact]
        public async Task PostOperationType_ReturnsItBack_AddToDb()
        {
            var operationToPost = new OperationTypeDTO { Id = 3, IsIncome = true, Name = "name" };

            using var respsonse = await _client.PostAsJsonAsync("api/OperationTypes", operationToPost);
            respsonse.EnsureSuccessStatusCode();
            var responseObject = JsonSerializer.Deserialize<OperationTypeDTO>(await respsonse.Content.ReadAsStringAsync(), _options);

            using var responseWithObjectFromDb = await _client.GetAsync("api/OperationTypes/3");
            responseWithObjectFromDb.EnsureSuccessStatusCode();
            var objectFromDb = JsonSerializer.Deserialize<OperationTypeDTO>(await responseWithObjectFromDb.Content.ReadAsStringAsync(), _options);
            responseObject.Should().BeEquivalentTo(objectFromDb, opt => opt.ComparingByMembers<OperationTypeDTO>());
        }
    }
}
