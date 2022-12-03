using FinancialManager.Infrastructure;
using FinancialManager.Shared.DTOs;
using FinancialManager.Shared.Models;

namespace FinancialManager.Tests.Shared.Database
{
    public static class DataSeeder
    {
        public static void SeedData(this FinancialManagerContext context)
        {
            context.FinancialOperations.AddRange(CreateTestFinancialOperations());
            context.OperationTypes.AddRange(CreateTestOperationTypes());
            context.SaveChanges();
        }

        public static List<FinancialOperation> CreateTestFinancialOperations() => new()
        {
            new FinancialOperation { Id = 1, Amount = 100, DateTime = new DateTime(2008, 10, 10), Description = "abc", OperationTypeId = 1 },
            new FinancialOperation { Id = 2, Amount = 100, DateTime = new DateTime(2007, 10, 10), Description = "bbc", OperationTypeId = 1 },
            new FinancialOperation { Id = 3, Amount = 100, DateTime = new DateTime(2006, 10, 10), Description = "cbc", OperationTypeId = 2 },
            new FinancialOperation { Id = 4, Amount = 100, DateTime = new DateTime(2005, 10, 10), Description = "dbc", OperationTypeId = 2 }
        };

        public static List<OperationType> CreateTestOperationTypes() => new()
        {
            new OperationType { Id = 1, IsIncome = true, Name = "salary"},
            new OperationType { Id = 2, IsIncome = false, Name = "rent" }
        };

    }
}
