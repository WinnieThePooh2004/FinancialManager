using FinancialManager.Frontend.Requests;
using FinancialManager.Shared.DTOs;
using Microsoft.AspNetCore.Components;

namespace FinancialManager.Frontend.Pages.Reports
{
    public partial class PeriodReport
    {
        [Inject] private IReportRequests ReportRequests { get; set; } = default!;
        private DateTime _begin = DateTime.Now;
        private DateTime _end = DateTime.Now;
        private ReportDTO? _report = null;

        private async Task GetReport()
        {
            _report = await ReportRequests.PeriodReportAsync(_begin, _end);
        }
    }
}
