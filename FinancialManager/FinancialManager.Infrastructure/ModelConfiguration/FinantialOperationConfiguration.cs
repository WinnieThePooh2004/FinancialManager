using FinancialManager.Shared.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinancialManager.Data.ModelConfiguration
{
    public class FinantialOperationConfiguration : IEntityTypeConfiguration<FinancialOperation>
    {
        public void Configure(EntityTypeBuilder<FinancialOperation> builder)
        {
            builder.HasOne(f => f.OperationType)
                .WithMany(o => o.Operations)
                .HasForeignKey(f => f.OperationTypeId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
