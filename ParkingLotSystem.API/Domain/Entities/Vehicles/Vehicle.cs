using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ParkingLotSystem.API.Domain.Enums;

namespace ParkingLotSystem.API.Domain.Entities.Vehicles;

[Table("Vehicles")]
public abstract class Vehicle
{
    public int Id { get; set; }

    [Required, MaxLength(20)]
    public string LicensePlate { get; set; } = string.Empty;

    [Required, MaxLength(100)]
    public string OwnerName { get; set; } = string.Empty;

    public VehicleType Type { get; protected set; }

    // Navigation Property
    public ICollection<ParkingTicket> Tickets { get; set; } = new List<ParkingTicket>();
}