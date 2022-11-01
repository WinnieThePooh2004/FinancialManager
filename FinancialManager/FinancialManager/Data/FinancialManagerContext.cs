using FinancialManager.Models;
using Microsoft.EntityFrameworkCore;

namespace FinancialManager.Data
{
    public class FinancialManagerContext : DbContext, IFinancialManagerContext
    {
        public FinancialManagerContext(DbContextOptions<FinancialManagerContext> options)
            : base(options)
        {
        }
        public DbSet<FinancialOperation> FinancialOperations { get; set; } = default!;

        public DbSet<OperationType> OperationTypes { get; set; } = default!;

        public DbSet<User> Users { get; set; } = default!;

        public async Task SaveChangesAsync()
        {
            await base.SaveChangesAsync();
        }

    }
}
