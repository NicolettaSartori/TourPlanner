namespace TourPlanner.Models;

public class Tour
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? From { get; set; }
    public string? To { get; set; }
    public string? TransportType { get; set; }
    public int Distance { get; set; }
    public int EstimatedTime { get; set; }
    public string? RouteInformation { get; set; } // todo zu einem Bild machen
}