namespace TourPlanner.Models;

public class TourLog
{
    public DateTime DateTime { get; set; }
    public string? Comment { get; set; }
    public int Difficulty { get; set; }
    public string? TotalDistance { get; set; }
    public string? TotalTime { get; set; }
    public int Rating { get; set; }
}