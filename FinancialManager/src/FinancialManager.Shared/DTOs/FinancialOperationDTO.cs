namespace FinancialManager.Shared.DTOs
{
    public class FinancialOperationDTO
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; } = DateTime.Now;
        public string Description { get; set; } = default!;
        public int Amount { get; set; }
        public int OperationTypeId { get; set; }
    }
}
