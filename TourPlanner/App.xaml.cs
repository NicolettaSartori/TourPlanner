using System.Windows;
using TourPlanner.DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TourPlanner.BusinessLayer.Factories;
using TourPlanner.Infrastructure;

namespace TourPlanner;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        var configuration = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false)
            .Build();
        
        var logConfigPath = configuration["Logging:Log4NetConfigPath"];
        if (logConfigPath != null)
            LoggerHelper.Initialize(logConfigPath);
        
        var dbContextFactory = new AppdDbContextFactory();
        using AppdDbContext dbContext = dbContextFactory.CreateDbContext();
        
        // dbContext.Database.EnsureDeleted();
        dbContext.Database.EnsureCreated();
        
        // Prod env
        // context.Database.Migrate();

        // Dev env only
        try
        {
            if (!dbContext.Tours.Any())
            {
                foreach (var tour in TourFactory.GetTours())
                {
                    dbContext.Tours.Add(tour);
                }
                dbContext.SaveChanges();
            }
            dbContext.Database.Migrate();
        }
        catch (Exception exception)
        {
            LoggerHelper.GetLogger(typeof(App)).Error("Error while trying to create database: " + exception.Message);
        }
    }
}