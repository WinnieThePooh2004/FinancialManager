﻿namespace FinancialManager.DTOs.FinancialOperations
{
    public class FinancialOperationCreateDto
    {
        public DateTime DateTime { get; set; } = default!;
        public string Description { get; set; } = default!;
        public double Amount { get; set; } = default!;
        public int OperationTypeId { get; set; }
    }
}
