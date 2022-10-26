using FinancialManager.Models;
using FinancialManager.DTOs.FinancialOperations;
namespace FinancialManager.DTOs.Reports
{
    public class ReportDetailsDto
    {
        public string TotalIncome { get; set; } = "0";
        public string TotalExprenses { get; set; } = "0";
        public List<FinancialOperationIndexDto> Operations { get; set; } = new List<FinancialOperationIndexDto>();
    }
}
