using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using FinancialManager.Services.ReportService;
using Shared.DTOs.Reports;

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
        public async Task<ActionResult<ReportDetailsDto>> GetDailyReport([FromQuery] DateTime date)
        {
            return _mapper.Map<ReportDetailsDto>(await _service.DailyReportAsync(date));
        }

        [Route("GetPeriodReport")]
        [HttpGet]
        public async Task<ActionResult<ReportDetailsDto>> GetReportByPeriod([FromQuery]DateTime periodStart, [FromQuery]DateTime periodEnd)
        {
            return _mapper.Map<ReportDetailsDto>(await _service.PeriodReportAsync(periodStart, periodEnd));
        }
    }
}
