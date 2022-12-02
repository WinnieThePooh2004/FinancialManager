namespace FinancialManager.Shared.DTOs
{
    public class ReportDTO
    {
        public int TotalIncome { get; set; }
        public int TotalExprenses { get; set; }
        public List<FinancialOperationDTO> Operations { get; set; } = new List<FinancialOperationDTO>();

    }
}
