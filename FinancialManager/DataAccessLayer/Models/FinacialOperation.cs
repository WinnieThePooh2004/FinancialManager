using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Models
{
    public class FinacialOperation
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime DateTime { get; set; } = default!;
        public string Description { get; set; } = default!;
        public int Amount { get; set; } = default!;
        public int OperationTypeId { get; set; }
    }
}
