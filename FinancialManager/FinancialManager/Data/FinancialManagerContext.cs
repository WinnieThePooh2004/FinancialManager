using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FinancialManager.Models;

namespace FinancialManager.Data
{
    public class FinancialManagerContext : DbContext
    {
        public FinancialManagerContext (DbContextOptions<FinancialManagerContext> options)
            : base(options)
        {
        }
        public DbSet<FinancialManager.Models.FinacialOperation> FinacialOperations { get; set; } = default!;
        public DbSet<FinancialManager.Models.OperationType> OperationTypes { get; set; } = default!;
    }
}
