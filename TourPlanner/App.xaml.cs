using System.Windows;
using Microsoft.Extensions.Configuration;
using TourPlanner.DataAccessLayer;

namespace TourPlanner;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        IConfiguration configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        using AppdDbContext dbContext = new(configuration);
        // Prod env
        // context.Database.Migrate();

        // Dev env only
        try
        {
            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();
        }
        catch (Exception exception)
        {
            Console.WriteLine("Error while trying to create database:");
            Console.WriteLine(exception);
        }

    }
}