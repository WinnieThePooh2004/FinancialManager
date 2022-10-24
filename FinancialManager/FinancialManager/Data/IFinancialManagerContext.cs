using FinancialManager.Models;
using Microsoft.EntityFrameworkCore;

namespace FinancialManager.Data
{
    public interface IFinancialManagerContext
    {
        DbSet<FinancialOperation> FinancialOperations { get; }
        DbSet<OperationType> OperationTypes { get; }
        Task SaveChangesAsync();
    }
}