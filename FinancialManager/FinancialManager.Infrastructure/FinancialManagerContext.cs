using FinancialManager.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace FinancialManager.Infrastructure
{
    public class FinancialManagerContext : DbContext
    {
        public FinancialManagerContext(DbContextOptions<FinancialManagerContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(FinancialManagerContext).Assembly);
        }

        public DbSet<FinancialOperation> FinancialOperations { get; set; } = default!;

        public DbSet<OperationType> OperationTypes { get; set; } = default!;
    }
}
