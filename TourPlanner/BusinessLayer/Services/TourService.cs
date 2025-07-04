using log4net;
using TourPlanner.DataAccessLayer.Models;
using TourPlanner.DataAccessLayer.Repositories;
using TourPlanner.Infrastructure;

namespace TourPlanner.BusinessLayer.Services;

public class TourService
{
    private readonly TourRepository _repository = new();
    private readonly TourLogRepository _logRepository = new();
    private readonly ILog _logger = LoggerHelper.GetLogger(typeof(TourService));

    public List<Tour> GetTours()
    {
        return _repository.GetAllTours();
    }
    
    public async Task AddTourAsync(Tour tour)
    {
        await _repository.AddTourAsync(tour);
        _logger.Info($"New tour [{tour.Id}] was added");
    }
    
    public async Task UpdateTourAsync(Tour tour)
    {
        await _repository.UpdateTourAsync(tour);
        _logger.Info($"Tour [{tour.Id}] was updated");
    }

    public async Task DeleteTourAsync(Guid tourId)
    {
        await _repository.DeleteTourAsync(tourId);
        _logger.Info($"Tour [{tourId}] was deleted");
    }

    public void ExportTour(Tour tour)
    {
        var pdfService = new PdfExportService();
        pdfService.ExportTour(tour);
        _logger.Info($"PDF for tour [{tour.Id}] was generated");
    }
    
    public async Task<List<TourLog>> GetLogsAsync(Guid tourId)
    {
        return await _logRepository.GetLogsForTourAsync(tourId);
    }
}