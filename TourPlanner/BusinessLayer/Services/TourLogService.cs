using TourPlanner.DataAccessLayer.Models;
using TourPlanner.DataAccessLayer.Repositories;

namespace TourPlanner.BusinessLayer.Services;

public class TourLogService
{
    private readonly TourLogRepository _repository = new();
    
    public async Task AddTourLogAsync(TourLog log, Guid tourId)
    {
        await _repository.AddTourLogAsync(log, tourId);
    }
    
    public async Task UpdateTourLogAsync(TourLog log)
    {
        await _repository.UpdateTourLogAsync(log);
    }

    public async Task DeleteTourLogAsync(Guid logId)
    {
        await _repository.DeleteTourLogAsync(logId);
    }
}