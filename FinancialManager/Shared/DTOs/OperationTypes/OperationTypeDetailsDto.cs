﻿namespace Shared.DTOs.OperationTypes
{
    public class OperationTypeDetailsDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public bool IsIncome { get; set; } = default!;
    }
}