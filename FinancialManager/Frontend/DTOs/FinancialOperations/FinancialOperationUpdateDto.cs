
namespace Shared.DTOs.FinancialOperations
{
    public class FinancialOperationUpdateDto
    {
        public int Id { get; set; }
        public string DateTime { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string Amount { get; set; } = default!;
        public string OperationTypeId { get; set; } = default!;
    }
}
