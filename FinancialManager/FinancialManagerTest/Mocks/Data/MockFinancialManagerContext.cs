using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinancialManager.Models;
using FinancialManager.Data;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace FinancialManagerTest.Mocks.Data
{
    internal class MockFinancialManagerContext : IFinancialManagerContext
    {
        public DbSet<FinancialOperation> FinancialOperations { get; set; }

        public DbSet<OperationType> OperationTypes { get; set; }

        public MockFinancialManagerContext()
        {
            FinancialOperations = new MockDbSet<FinancialOperation>(CreateFinancialOperations()).Object;
            OperationTypes = new MockDbSet<OperationType>(CreateOperationTypes()).Object;
        }

        public async Task SaveChangesAsync()
        {
            await Task.Delay(1);
        }

        private List<FinancialOperation> CreateFinancialOperations()
        {
            return new List<FinancialOperation>
            {
                new FinancialOperation()
                {
                    Id = 123,
                    Amount = 1000,
                    DateTime = DateTime.Parse("10.10.2002"),
                    Description = "Got salary", OperationTypeId = 11
                },
                new FinancialOperation()
                {
                    Id = 124,
                    Amount = 1000,
                    DateTime = DateTime.Parse("10.11.2002"),
                    Description = "Got salary", OperationTypeId = 11
                },
                new FinancialOperation()
                {
                    Id = 125,
                    Amount = 1000,
                    DateTime = DateTime.Parse("10.12.2002"),
                    Description = "Rent", OperationTypeId = 12
                },
                new FinancialOperation()
                {
                    Id = 126,
                    Amount = 1000,
                    DateTime = DateTime.Parse("10.10.2003"),
                    Description = "Rent", OperationTypeId = 12
                }
            };
        }

        private List<OperationType> CreateOperationTypes()
        {
            return new List<OperationType>
            {
                new OperationType(){ Id = 11, IsIncome = true, Name = "salary" },
                new OperationType(){ Id = 12, IsIncome = false, Name = "rent" }
            };
        }
        
    }
}
