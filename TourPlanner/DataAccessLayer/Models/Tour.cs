using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TourPlanner.BusinessLayer.Enums;

namespace TourPlanner.DataAccessLayer.Models;

[Table("tours")]
public class Tour
{
    [Key]
    public required Guid Id { get; set; }
    [Required, MaxLength(255)]
    public required string Name { get; set; }
    [Required, MaxLength(500)]
    public required string Description { get; set; }
    [Required, MaxLength(255)]
    public required string From { get; set; }
    [Required, MaxLength(255)]
    public required string To { get; set; }
    [Required]
    public required TransportType TransportType { get; set; }
    [Required, MaxLength(255)]
    public required string Distance { get; set; }
    [Required, MaxLength(255)]
    public required string EstimatedTime { get; set; }
    public List<TourLog> Logs { get; set; } = [];
}