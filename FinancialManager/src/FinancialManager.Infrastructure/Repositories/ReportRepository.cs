using FinancialManager.Shared.Exceptions;
using FinancialManager.Shared.Models;
using Microsoft.EntityFrameworkCore;
using FinancialManager.Shared.Interfaces.Repositiories;

namespace FinancialManager.Infrastructure.Repositiories
{
    public class ReportRepository : IReportRepository
    {

        private readonly FinancialManagerContext _context;
        public ReportRepository(FinancialManagerContext context)
        {
            _context = context;
        }

        public async Task<Report> DailyReport(DateTime date)
        {
            return await GetReportAsync(date, date);
        }

        public async Task<Report> PeriodReport(DateTime start, DateTime finish)
        {
            return await GetReportAsync(start, finish);
        }

        private async Task<Report> GetReportAsync(DateTime min, DateTime max)
        {
            var report = new Report();
            var operation = await _context.FinancialOperations
                .Include(f => f.OperationType)
                .Where(f => min.Date <= f.DateTime.Date && f.DateTime.Date <= max.Date)
                .ToListAsync();
            operation.ForEach(operation => 
            { 
                if(operation.OperationType is null)
                {
                    throw new HttpResponseExeption(System.Net.HttpStatusCode.NotFound, "Some operatins don`t have operation type");
                }
                if(operation.OperationType.IsIncome)
                {
                    report.TotalIncome += operation.Amount;
                }
                else
                {
                    report.TotalExprenses += operation.Amount;
                }
                report.Operations.Add(operation);
            });
            return report;
        }
    }
}
