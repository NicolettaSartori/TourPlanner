using System.Windows;
using Microsoft.Extensions.Configuration;
using TourPlanner.DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using TourPlanner.BusinessLayer.Enums; 
using TourPlanner.DataAccessLayer.Models;

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
            if (!dbContext.Tours.Any())
            {
                dbContext.Tours.Add(new Tour
                {
                    Id = Guid.NewGuid(),
                    Name = "Ruhrtal Radweg",
                    Description = "Von Winterberg nach Meschede",
                    From = "Winterberg",
                    To = "Meschede",
                    TransportType = TransportType.Bike,
                    Distance = "40,5 km",
                    EstimatedTime = "2h 27min"
                });
                dbContext.SaveChanges();
            }
            dbContext.Database.Migrate();
            
            //dbContext.Database.EnsureDeleted();
            //dbContext.Database.EnsureCreated();
        }
        catch (Exception exception)
        {
            Console.WriteLine("Error while trying to create database:");
            Console.WriteLine(exception);
        }

    }
}