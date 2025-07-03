using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace TourPlanner.DataAccessLayer
{
    public class AppdDbContextFactory : IDesignTimeDbContextFactory<AppdDbContext>
    {
        public AppdDbContext CreateDbContext(string[] args)
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            return new AppdDbContext(configuration);
        }

        public AppdDbContext CreateDbContext()
        {
            return CreateDbContext(Array.Empty<string>());
        }
    }
}