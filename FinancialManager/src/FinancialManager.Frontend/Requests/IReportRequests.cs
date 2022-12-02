using FinancialManager.Shared.DTOs;

namespace FinancialManager.Frontend.Requests
{
    public interface IReportRequests
    {
        Task<ReportDTO> DailyReportAsync(DateTime date);
        Task<ReportDTO> PeriodReportAsync(DateTime begin, DateTime end);
    }
}