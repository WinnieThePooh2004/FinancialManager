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
            return await GetReportAsync(operation => operation.DateTime.Date == date.Date);
        }

        public async Task<Report> PeriodReport(DateTime start, DateTime finish)
        {
            return await GetReportAsync(operation => start.Date <= operation.DateTime.Date && operation.DateTime.Date <= finish.Date);
        }

        private async Task<Report> GetReportAsync(Func<FinancialOperation, bool> predicate)
        {
            var report = new Report();
            var operation = await _context.FinancialOperations
                .Include(f => f.OperationType)
                .Where(f => predicate(f))
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
