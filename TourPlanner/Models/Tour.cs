using TourPlanner.Enums;

namespace TourPlanner.Models;

public class Tour
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required string From { get; set; }
    public required string To { get; set; }
    public required TransportType TransportType { get; set; }
    public required string Distance { get; set; }
    public required string EstimatedTime { get; set; }
    public List<TourLog> Logs { get; } = [];
}