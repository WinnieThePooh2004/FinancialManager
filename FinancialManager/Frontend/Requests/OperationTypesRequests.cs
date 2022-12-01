using FinancialManager.Shared.DTOs;
using System.Text.Json;

namespace Frontend.Requests
{
    public class OperationTypesRequests : IOperationTypesRequests
    {
        IHttpClientFactory _client;
        private readonly JsonSerializerOptions _options;
        public OperationTypesRequests(IHttpClientFactory client)
        {
            _client = client;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public async Task<OperationTypeDTO> CreateAsync(OperationTypeDTO operation)
        {
            var response = await _client.CreateClient("FMApi").PostAsJsonAsync("api/OperationTypes", operation);
            response.EnsureSuccessStatusCode();
            return JsonSerializer.Deserialize<OperationTypeDTO>(await response.Content.ReadAsStringAsync(), _options) ??
                throw new InvalidOperationException("Can`t desirialize response");
        }

        public async Task<OperationTypeDTO> UpdateAsync(OperationTypeDTO operation)
        {
            var response = await _client.CreateClient("FMApi").PutAsJsonAsync("api/OperationTypes", operation);
            response.EnsureSuccessStatusCode();
            return JsonSerializer.Deserialize<OperationTypeDTO>(await response.Content.ReadAsStringAsync(), _options) ??
                throw new InvalidOperationException("Can`t desirialize response");
        }

        public async Task<OperationTypeDTO> DeleteAsync(int? id)
        {
            var response = await _client.CreateClient("FMApi").DeleteAsync($"api/OperationTypes/{id}");
            response.EnsureSuccessStatusCode();
            return JsonSerializer.Deserialize<OperationTypeDTO>(await response.Content.ReadAsStringAsync(), _options) ??
                throw new InvalidOperationException("Can`t desirialize response");
        }

        public async Task<OperationTypeDTO> GetByIdAsync(int? id)
        {
            var response = await _client.CreateClient("FMApi").GetAsync($"api/OperationTypes/{id}");
            response.EnsureSuccessStatusCode();
            return JsonSerializer.Deserialize<OperationTypeDTO>(await response.Content.ReadAsStringAsync(), _options) ??
                throw new InvalidOperationException("Can`t desirialize response");
        }

        public async Task<List<OperationTypeDTO>> GetAllAsync()
        {
            var response = await _client.CreateClient("FMApi").GetAsync("api/OperationTypes");
            response.EnsureSuccessStatusCode();
            return JsonSerializer.Deserialize<List<OperationTypeDTO>>(await response.Content.ReadAsStringAsync(), _options) ??
                throw new InvalidOperationException("Can`t desirialize response");
        }
    }
}
