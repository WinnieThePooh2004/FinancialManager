using FinancialManager.Shared.DTOs;
namespace FinancialManager.Shared.Interfaces.Services
{
    public interface IReportService
    {
        Task<ReportDTO> DailyReportAsync(DateTime date);
        Task<ReportDTO> PeriodReportAsync(DateTime start, DateTime finish);
    }
}
