namespace FinancialManager.Shared.DTOs
{
    public class OperationTypeDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public bool IsIncome { get; set; }
    }
}
