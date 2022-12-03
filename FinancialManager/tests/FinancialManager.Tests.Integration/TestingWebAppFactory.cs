using FinancialManager.Infrastructure;
using Microsoft.AspNetCore.Mvc.Testing;
using FinancialManager.Tests.Shared.Database;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FinancialManager.Tests.Integration
{
    public class TestingWebAppFactory : WebApplicationFactory<Program>
    {
        private FinancialManagerContext _context = DbContextFactory.CreateInMemoryContext();

        public TestingWebAppFactory()
        {
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();
            _context.SeedData();
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(service => services.GetType() == typeof(DbContextOptions<FinancialManagerContext>));
                if(descriptor is not null)
                {
                    services.Remove(descriptor);
                }
                services.AddSingleton(_context);
            });
        }

        public void RefreshContext()
        {
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();
            _context.SeedData();
        }
    }
}
