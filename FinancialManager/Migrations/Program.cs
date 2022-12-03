using FinancialManager.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

public class Program : IDesignTimeDbContextFactory<FinancialManagerContext>
{
    public FinancialManagerContext CreateDbContext(string[] args)
    {
        var configurationBuilder = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

        IConfigurationRoot configuration = configurationBuilder.Build();

        var connString = configuration.GetConnectionString("FinancialManagerContext");

        DbContextOptionsBuilder<FinancialManagerContext> optionsBuilder = new DbContextOptionsBuilder<FinancialManagerContext>()
            .UseSqlServer(connString, b => b.MigrationsAssembly("Migrations"));

        return new FinancialManagerContext(optionsBuilder.Options);
    }

    static void Main(string[] args)
    {

        Program p = new Program();

        using (FinancialManagerContext db = p.CreateDbContext(args))
        {
            db.Database.Migrate();
        }
    }
}