using FinancialManager.Shared.DTOs;
using Microsoft.AspNetCore.Components;

namespace FinancialManager.Frontend.Components
{
    public partial class Report
    {
        [Parameter] public ReportDTO Model { get; set; } = new();
    }
}
