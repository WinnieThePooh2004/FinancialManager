using Microsoft.AspNetCore.Mvc;
using FinancialManager.Shared.Interfaces.Services;

namespace FinancialManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly IReportService _service;
        public ReportsController(IReportService service)
        {
            _service = service;
        }

        [Route("GetDailyReport")]
        [HttpGet]
        public async Task<IActionResult> GetDailyReport([FromQuery] DateTime date)
        {
            return Ok(await _service.DailyReportAsync(date));
        }

        [Route("GetPeriodReport")]
        [HttpGet]
        public async Task<IActionResult> GetReportByPeriod([FromQuery]DateTime periodStart, [FromQuery]DateTime periodEnd)
        {
            return Ok(await _service.PeriodReportAsync(periodStart, periodEnd));
        }
    }
}
