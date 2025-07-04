using TourPlanner.DataAccessLayer.Models;
using TourPlanner.DataAccessLayer.Repositories;

namespace TourPlanner.BusinessLayer.Services;

public class TourService
{
    private readonly TourRepository _repository = new();
    private readonly TourLogRepository _logRepository = new();

    public List<Tour> GetTours()
    {
        return _repository.GetAllTours();
    }
    
    public async Task AddTourAsync(Tour tour)
    {
        await _repository.AddTourAsync(tour);
    }
    
    public async Task UpdateTourAsync(Tour updatedTour)
    {
        await _repository.UpdateTourAsync(updatedTour);
    }

    public async Task DeleteTourAsync(Guid tourId)
    {
        await _repository.DeleteTourAsync(tourId);
    }

    public void ExportTour(Tour tour)
    {
        var pdfService = new PdfExportService();
        pdfService.ExportTour(tour);
    }
    
    public async Task<List<TourLog>> GetLogsAsync(Guid tourId)
    {
        return await _logRepository.GetLogsForTourAsync(tourId);
    }
}