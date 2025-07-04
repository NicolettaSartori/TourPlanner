using log4net;
using TourPlanner.DataAccessLayer.Models;
using TourPlanner.DataAccessLayer.Repositories;
using TourPlanner.Infrastructure;

namespace TourPlanner.BusinessLayer.Services;

public class TourLogService
{
    private readonly TourLogRepository _repository = new();
    private readonly ILog _logger = LoggerHelper.GetLogger(typeof(TourLogService));
    
    public async Task AddTourLogAsync(TourLog log, Guid tourId)
    {
        await _repository.AddTourLogAsync(log, tourId);
        _logger.Info($"New tourlog [{log.Id}] was added");
    }
    
    public async Task UpdateTourLogAsync(TourLog log)
    {
        await _repository.UpdateTourLogAsync(log);
        _logger.Info($"Tourlog [{log.Id}] was updated");
    }

    public async Task DeleteTourLogAsync(Guid logId)
    {
        await _repository.DeleteTourLogAsync(logId);
        _logger.Info($"Tour [{logId}] was deleted");
    }
}