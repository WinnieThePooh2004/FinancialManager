namespace Shared.DTOs.OperationTypes
{
    public class OperationTypeDetailsDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string IsIncome { get; set; } = default!;
    }
}
