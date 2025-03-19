using TourPlanner.Enums;

namespace TourPlanner.Models;

public class TourLog
{
    public required DateTime DateTime { get; set; }
    public string? Comment { get; set; }
    public Difficulty Difficulty { get; set; }
    public required string? TotalDistance { get; set; }
    public required string? TotalTime { get; set; }
    public int Rating { get; set; }
}