using Microsoft.Extensions.Configuration;
using System.IO;

namespace TourPlanner.Infrastructure
{
    public static class ConfigurationHelper
    {
        private static readonly IConfiguration _configuration;

        static ConfigurationHelper()
        {
            _configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();
        }

        public static IConfiguration Configuration => _configuration;
    }
}