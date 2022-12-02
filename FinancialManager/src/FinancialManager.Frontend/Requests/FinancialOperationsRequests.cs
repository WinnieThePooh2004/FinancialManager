using FinancialManager.Shared.DTOs;
using System.Text.Json;

namespace FinancialManager.Frontend.Requests
{
    public class FinancialOperationsRequests : IFinancialOperationsRequests
    {
        private readonly IHttpClientFactory _client;
        private readonly JsonSerializerOptions _options;
        public FinancialOperationsRequests(IHttpClientFactory client) 
        {
            _client = client;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }
        public async Task<FinancialOperationDTO> CreateAsync(FinancialOperationDTO financialOperation)
        {
            var response = await _client.CreateClient("FMApi").PostAsJsonAsync($"api/FinancialOperations", financialOperation);
            response.EnsureSuccessStatusCode();
            return JsonSerializer.Deserialize<FinancialOperationDTO>(await response.Content.ReadAsStringAsync(), _options)
                ?? throw new NullReferenceException("Cannot desirialize response object");
        }

        public async Task<FinancialOperationDTO> DeleteAsync(int id)
        {
            var response = await _client.CreateClient("FMApi").DeleteAsync($"api/FinancialOperations/{id}");
            response.EnsureSuccessStatusCode();
            return JsonSerializer.Deserialize<FinancialOperationDTO>(await response.Content.ReadAsStringAsync(), _options)
                ?? throw new NullReferenceException("Cannot desirialize response object");
        }

        public  async Task<List<FinancialOperationDTO>> GetAllAsync()
        {
            var response = await _client.CreateClient("FMApi").GetAsync($"api/FinancialOperations");
            response.EnsureSuccessStatusCode();
            return JsonSerializer.Deserialize<List<FinancialOperationDTO>>(await response.Content.ReadAsStringAsync(), _options)
                ?? throw new NullReferenceException("Cannot desirialize response object");
        }

        public async Task<FinancialOperationDTO> GetByIdAsync(int id)
        {
            var response = await _client.CreateClient("FMApi").GetAsync($"api/FinancialOperations/{id}");
            response.EnsureSuccessStatusCode();
            return JsonSerializer.Deserialize<FinancialOperationDTO>(await response.Content.ReadAsStringAsync(), _options)
                ?? throw new NullReferenceException("Cannot desirialize response object");
        }

        public async Task<FinancialOperationDTO> UpdateAsync(FinancialOperationDTO financialOperation)
        {
            var response = await _client.CreateClient("FMApi").PutAsJsonAsync($"api/FinancialOperations", financialOperation);
            response.EnsureSuccessStatusCode();
            return JsonSerializer.Deserialize<FinancialOperationDTO>(await response.Content.ReadAsStringAsync(), _options)
                ?? throw new NullReferenceException("Cannot desirialize response object");
        }
    }
}
