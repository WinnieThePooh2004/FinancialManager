namespace FinancialManager.DTOs.FinancialOperations
{
    public class FinancialOperationCreateDto
    {
        public string DateTime { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string Amount { get; set; } = default!;
        public string OperationTypeId { get; set; } = default!;
    }
}
