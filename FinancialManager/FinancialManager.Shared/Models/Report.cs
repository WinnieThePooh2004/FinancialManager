namespace FinancialManager.Shared.Models
{
    public class Report
    {
        public double TotalIncome { get; set; }
        public double TotalExprenses { get; set; }
        public List<FinancialOperation> Operations { get; set; } = new List<FinancialOperation>();
    }
}
