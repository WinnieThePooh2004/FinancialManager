using FinancialManager.Shared.DTOs;
using System.Text.Json;

namespace FinancialManager.Frontend.Requests
{
    public class ReportRequests : IReportRequests
    {
        private readonly IHttpClientFactory _client;
        private readonly JsonSerializerOptions _options;
        public ReportRequests(IHttpClientFactory client)
        {
            _client = client;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public async Task<ReportDTO> DailyReportAsync(DateTime date)
        {
            var response = await _client.CreateClient("FMApi").GetAsync($"api/Reports/GetDailyReport?date={date:MM.dd.yyyy}");
            Console.WriteLine(await response.Content.ReadAsStringAsync());
            response.EnsureSuccessStatusCode();
            return JsonSerializer.Deserialize<ReportDTO>(await response.Content.ReadAsStringAsync(), _options)
                ?? throw new NullReferenceException("Cannot desirialize response object");
        }

        public async Task<ReportDTO> PeriodReportAsync(DateTime begin, DateTime end)
        {
            var response = await _client.CreateClient("FMApi")
                .GetAsync($"api/Reports/GetPeriodReport?periodStart={begin:MM.dd.yyyy}&periodEnd={end:MM.dd.yyyy}");
            response.EnsureSuccessStatusCode();
            return JsonSerializer.Deserialize<ReportDTO>(await response.Content.ReadAsStringAsync(), _options)
                ?? throw new NullReferenceException("Cannot desirialize response object");
        }
    }
}
