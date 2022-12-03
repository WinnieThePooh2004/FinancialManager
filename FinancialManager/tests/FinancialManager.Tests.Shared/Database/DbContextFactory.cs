using FinancialManager.Infrastructure;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace FinancialManager.Tests.Shared.Database
{
    public class DbContextFactory
    {
        public static FinancialManagerContext CreateInMemoryContext()
        {
            var connection = new SqliteConnection("Data Source=:memory:");
            connection.Open();
            var options = new DbContextOptionsBuilder<FinancialManagerContext>().UseSqlite(connection).Options;
            return new(options);
        }
    }
}
