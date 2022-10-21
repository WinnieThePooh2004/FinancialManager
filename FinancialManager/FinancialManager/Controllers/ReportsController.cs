using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FinancialManager.Data;
using FinancialManager.Models;
using AutoMapper;
using FinancialManager.DTOs.FinancialOperations;

namespace FinancialManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly FinancialManagerContext _context;
        private readonly IMapper _mapper;
        public ReportsController(FinancialManagerContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [Route("GetDailyReport")]
        [HttpPut]
        public async Task<ActionResult<Report>> GetDailyReport(string dateString)
        {
            if(!DateTime.TryParse(dateString, out DateTime date))
            {
                return BadRequest();
            }
            return await GetReportAsync(operation => operation.DateTime.Date == date.Date);
        }

        [Route("GetPeriodReport")]
        [HttpPut]
        public async Task<ActionResult<Report>> GetReportByPeriod(string periodStart, string periodEnd)
        {
            if (!DateTime.TryParse(periodStart, out DateTime startDate) || !DateTime.TryParse(periodEnd, out DateTime endDate))
            {
                Console.WriteLine("here");
                return BadRequest();
            }
            return await GetReportAsync(operation => operation.DateTime.Date >= startDate.Date && operation.DateTime.Date <= endDate.Date);
        }

        private async Task<Report> GetReportAsync(Func<FinacialOperation, bool> predicate)
        {
            var report = new Report();
            var operations = (await _context.FinacialOperations.ToArrayAsync()).Where(predicate);
            var operationTypes = await _context.OperationTypes.ToListAsync();
            foreach (var operation in operations)
            {
                var operationType = operationTypes.FirstOrDefault(type => type.Id == operation.OperationTypeId);
                if (operationType is null)
                {
                    throw new Exception($"Can`t find operation type for operation{operation.Id}");
                }
                report.Operations.Add(_mapper.Map<FinancialOperationDetailsDto>(operation));
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
