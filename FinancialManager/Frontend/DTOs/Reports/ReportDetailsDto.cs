using Shared.DTOs.FinancialOperations;
namespace Shared.DTOs.Reports
{
    public class ReportDetailsDto
    {
        public string TotalIncome { get; set; } = "0";
        public string TotalExprenses { get; set; } = "0";
        public List<FinancialOperationIndexDto> Operations { get; set; } = new List<FinancialOperationIndexDto>();
    }
}
