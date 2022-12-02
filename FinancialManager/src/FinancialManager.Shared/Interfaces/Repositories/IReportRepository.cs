using FinancialManager.Shared.Models;
namespace FinancialManager.Shared.Interfaces.Repositiories
{
    public interface IReportRepository
    {
        Task<Report> DailyReport(DateTime date);
        Task<Report> PeriodReport(DateTime begin, DateTime end);
    }
}
