using FinancialManager.Frontend.Requests;
using FinancialManager.Shared.DTOs;
using Microsoft.AspNetCore.Components;

namespace FinancialManager.Frontend.Pages.Reports
{
    public partial class DailyReport
    {
        [Inject] private IReportRequests ReportRequests { get; set; } = default!;
        private DateTime _date = DateTime.Now;
        private ReportDTO? _report = null;

        private async Task GetReport()
        {
            _report = await ReportRequests.DailyReportAsync(_date);
        }

        private void DateChanged(DateTime? date)
        {
            if(date is null)
            {
                return;
            }
            var cameDate = (DateTime)date;
            var newDate = new DateTime(year: cameDate.Year, month: cameDate.Month, day: cameDate.Day);
            _date = newDate;
        }
    }
}
