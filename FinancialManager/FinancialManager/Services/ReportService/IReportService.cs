using FinancialManager.Models;
namespace FinancialManager.Services.ReportService
{
    public interface IReportService
    {
        Task<Report> DailyReportAsync(DateTime date);
        Task<Report> PeriodReportAsync(DateTime start, DateTime finish);
    }
}
