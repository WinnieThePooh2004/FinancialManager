namespace FinancialManager.DTOs.FinancialOperations
{
    public class FinancialOperationIndexDto
    {
        public int Id { get; set; }
        public string DateTime { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string Amount { get; set; } = default!;
        public int OperationTypeId { get; set; }
    }
}
