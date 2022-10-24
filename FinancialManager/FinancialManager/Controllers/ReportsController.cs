using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FinancialManager.Data;
using FinancialManager.Models;
using AutoMapper;
using FinancialManager.DTOs.FinancialOperations;
using FinancialManager.Services.ReportService;
using FinancialManager.DTOs.Reports;

namespace FinancialManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly IReportService _service;
        private readonly IMapper _mapper;
        public ReportsController(IReportService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [Route("GetDailyReport")]
        [HttpGet]
        public async Task<ActionResult<Report>> GetDailyReport([FromQuery] DateTime date)
        {
            return await _service.DailyReportAsync(date);
        }

        [Route("GetPeriodReport")]
        [HttpGet]
        public async Task<ActionResult<ReportDetailsDto>> GetReportByPeriod(DateTime periodStart, DateTime periodEnd)
        {
            return _mapper.Map<ReportDetailsDto>(await _service.PeriodReportAsync(periodStart, periodEnd));
        }
    }
}
