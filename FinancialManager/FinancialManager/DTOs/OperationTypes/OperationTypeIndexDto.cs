namespace FinancialManager.DTOs.OperationTypes
{
    public class OperationTypeIndexDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string IsIncome { get; set; } = default!;
    }
}
