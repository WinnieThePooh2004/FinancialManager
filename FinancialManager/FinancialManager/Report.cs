using FinancialManager.DTOs.FinancialOperations;

namespace FinancialManager
{
    public class Report
    {
        public double TotalIncome { get; set; }
        public double TotalExprenses { get; set; }
        public List<FinancialOperationDetailsDto> Operations { get; set; } = new List<FinancialOperationDetailsDto>();
    }
}
