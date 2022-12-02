using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinancialManager.Shared.Models
{
    public class OperationType
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public bool IsIncome { get; set; }
        public List<FinancialOperation> Operations { get; set; } = default!;
    }
}
