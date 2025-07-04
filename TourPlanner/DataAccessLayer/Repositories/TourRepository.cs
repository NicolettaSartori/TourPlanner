using Microsoft.EntityFrameworkCore;
using TourPlanner.DataAccessLayer.Models;

namespace TourPlanner.DataAccessLayer.Repositories;

public class TourRepository : RepositoryBase
{
    public List<Tour> GetAllTours()
    {
        return Context.Tours.ToList();
    }
    
    public async Task<List<Tour>> GetAllToursAsync()
    {
        return await Context.Tours.ToListAsync();
    }

    public async Task AddTourAsync(Tour tour)
    {
        Context.Tours.Add(tour);
        await Context.SaveChangesAsync();
    }
    
    public async Task UpdateTourAsync(Tour updatedTour)
    {
        var existingTour = await Context.Tours.FindAsync(updatedTour.Id);
        if (existingTour != null)
        {
            Context.Entry(existingTour).CurrentValues.SetValues(updatedTour);
            await Context.SaveChangesAsync();
        }
    }


    public async Task DeleteTourAsync(Guid tourId)
    {
        var tour = await Context.Tours.FindAsync(tourId);
        if (tour != null)
        {
            Context.Tours.Remove(tour);
            await Context.SaveChangesAsync();
        }
    }

}