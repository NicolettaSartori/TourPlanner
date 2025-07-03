using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using TourPlanner.DataAccessLayer.Models;

namespace TourPlanner.DataAccessLayer.Repositories;

public class TourRepository
{
    private readonly AppdDbContext _context;

    public TourRepository(AppdDbContext context)
    {
        _context = context;
    }

    public ObservableCollection<Tour> GetAllTours()
    {
        return new ObservableCollection<Tour>(_context.Tours.ToList());
    }

    public void AddTour(Tour tour)
    {
        _context.Tours.Add(tour);
        _context.SaveChanges();
    }

    public async Task AddTourAsync(Tour tour)
    {
        _context.Tours.Add(tour);
        await _context.SaveChangesAsync();
    }
    
    public async Task UpdateTourAsync(Tour updatedTour)
    {
        var existingTour = await _context.Tours.FindAsync(updatedTour.Id);
        if (existingTour != null)
        {
            _context.Entry(existingTour).CurrentValues.SetValues(updatedTour);
            await _context.SaveChangesAsync();
        }
    }


    public async Task DeleteTourAsync(Guid tourId)
    {
        var tour = await _context.Tours.FindAsync(tourId);
        if (tour != null)
        {
            _context.Tours.Remove(tour);
            await _context.SaveChangesAsync();
        }
    }

}