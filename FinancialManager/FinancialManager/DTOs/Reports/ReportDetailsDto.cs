using FinancialManager.Models;
using FinancialManager.DTOs.FinancialOperations;
namespace FinancialManager.DTOs.Reports
{
    public class ReportDetailsDto
    {
        public double TotalIncome { get; set; }
        public double TotalExprenses { get; set; }
        public List<FinancialOperationDetailsDto> Operations { get; set; } = new List<FinancialOperationDetailsDto>();

    }
}
