using System.Windows;
using TourPlanner.DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using TourPlanner.BusinessLayer.Factories;

namespace TourPlanner;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

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
            Console.WriteLine("Error while trying to create database:");
            Console.WriteLine(exception);
        }

    }
}