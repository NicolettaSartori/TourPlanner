using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TourPlanner.BusinessLayer.Enums;

namespace TourPlanner.DataAccessLayer.Models;

[Table("tour_logs")]
public class TourLog
{
    [Key]
    public required Guid Id { get; set; }
    [Required]
    public required DateTime DateTime { get; set; }
    [MaxLength(500)]
    public string? Comment { get; set; }
    [Required]
    public Difficulty Difficulty { get; set; }
    [Required, MaxLength(255)]
    public required string? TotalDistance { get; set; }
    [Required, MaxLength(255)]
    public required string? TotalTime { get; set; }
    public int? Rating { get; set; }
    [Required]
    public required Guid TourId { get; set; }

    public Tour? Tour { get; set; }
}