using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace FinancialManager.DTOs.FinancialOperations
{
    public class FinancialOperationUpdateDto
    {
        [JsonIgnore]
        public int Id { get; set; }
        public DateTime DateTime { get; set; } = default!;
        public string Description { get; set; } = default!;
        public double Amount { get; set; } = default!;
        public int OperationTypeId { get; set; }
    }
}
