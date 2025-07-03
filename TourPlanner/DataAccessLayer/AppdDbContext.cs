using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TourPlanner.DataAccessLayer.Models;

namespace TourPlanner.DataAccessLayer;

public class AppdDbContext : DbContext
{
    private readonly IConfiguration _configuration;

    public DbSet<Tour> Tours { get; set; }
    public DbSet<TourLog> TourLogs { get; set; }

    public AppdDbContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.EnableSensitiveDataLogging();

        optionsBuilder.UseNpgsql(_configuration.GetConnectionString("PostgresDatabase"),
            sopt => sopt.UseNetTopologySuite());            
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.HasPostgresExtension("postgis");

        builder.Ignore<System.Windows.Controls.GroupStyleSelector>();
        builder.Ignore<System.Windows.Controls.Primitives.CustomPopupPlacementCallback>();
        builder.Ignore<System.Windows.Controls.ValidationError>();
        builder.Ignore<System.Windows.Controls.Primitives.Popup>();
        
    }

}