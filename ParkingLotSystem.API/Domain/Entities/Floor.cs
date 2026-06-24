using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ParkingLotSystem.API.Domain.Entities.Spots;

namespace ParkingLotSystem.API.Domain.Entities;

[Table("Floors")]
public class Floor
{
    public int Id { get; set; }

    [Required]
    public int FloorNumber { get; set; }

    // Foreign Key
    public int ParkingLotId { get; set; }

    // Navigation Properties
    public ParkingLot ParkingLot { get; set; } = null!;
    public ICollection<ParkingSpot> ParkingSpots { get; set; } = new List<ParkingSpot>();
}