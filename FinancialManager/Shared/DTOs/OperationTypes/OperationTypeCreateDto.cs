namespace Shared.DTOs.OperationTypes
{
    public class OperationTypeCreateDto
    {
        public string Name { get; set; } = default!;
        public bool IsIncome { get; set; } = default!;
    }
}
