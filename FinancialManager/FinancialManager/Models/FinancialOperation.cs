using System.ComponentModel.DataAnnotations.Schema;

namespace FinancialManager.Models
{
    public class FinancialOperation
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime DateTime { get; set; } = default!;
        public string Description { get; set; } = default!;
        public int Amount { get; set; } = default!;
        public int OperationTypeId { get; set; }
    }
}
