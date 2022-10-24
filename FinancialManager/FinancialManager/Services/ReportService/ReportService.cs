using FinancialManager.Models;
using FinancialManager.Data;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using FinancialManager.DTOs.FinancialOperations;

namespace FinancialManager.Services.ReportService
{
    public class ReportService : IReportService
    {
        private readonly IFinancialManagerContext _context;
        public ReportService(IFinancialManagerContext context)
        {
            _context = context;
        }

        public async Task<Report> DailyReportAsync(DateTime date)
        {
            return await GetReportAsync(operation => operation.DateTime.Date == date.Date);
        }

        public async Task<Report> PeriodReportAsync(DateTime start, DateTime finish)
        {
            return await GetReportAsync(operation => start.Date  <= operation.DateTime.Date && operation.DateTime.Date <= finish.Date);
        }

        private async Task<Report> GetReportAsync(Func<FinancialOperation, bool> predicate)
        {
            var report = new Report();
            var operations = (await _context.FinancialOperations.ToArrayAsync()).Where(operation => predicate(operation));
            var operationTypes = await _context.OperationTypes.ToListAsync();
            foreach (var operation in operations)
            {
                var operationType = operationTypes.FirstOrDefault(type => type.Id == operation.OperationTypeId);
                if (operationType is null)
                {
                    throw new Exception($"Can`t find operation type for operation{operation.Id}");
                }
                report.Operations.Add(operation);
                if (operationType.IsIncome)
                {
                    report.TotalIncome += operation.Amount;
                    continue;
                }

                report.TotalExprenses += operation.Amount;
            }
            return report;
        }
    }
}
