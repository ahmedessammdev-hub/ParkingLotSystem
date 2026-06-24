using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ParkingLotSystem.API.Domain.Entities.Spots;
using ParkingLotSystem.API.Domain.Entities.Vehicles;
using ParkingLotSystem.API.Domain.Enums;

namespace ParkingLotSystem.API.Domain.Entities;

[Table("ParkingTickets")]
public class ParkingTicket
{
    public int Id { get; set; }

    [Required]
    public DateTime EntryTime { get; set; }

    public DateTime? ExitTime { get; set; }

    [Column(TypeName = "decimal(10,2)")]
    public decimal? FeeCharged { get; set; }

    public bool IsActive { get; set; } = true;

    public FeeType FeeType { get; set; }

    // Foreign Keys
    public int VehicleId { get; set; }
    public int ParkingSpotId { get; set; }

    // Navigation Properties
    public Vehicle Vehicle { get; set; } = null!;
    public ParkingSpot ParkingSpot { get; set; } = null!;
}