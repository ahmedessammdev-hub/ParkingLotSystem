using ParkingLotSystem.API.Domain.Enums;

namespace ParkingLotSystem.API.DTOs.Responses;

public class ParkingTicketResponse
{
    public int TicketId { get; set; }
    public DateTime EntryTime { get; set; }
    public DateTime? ExitTime { get; set; }
    public decimal? FeeCharged { get; set; }
    public bool IsActive { get; set; }
    public FeeType FeeType { get; set; }

    // Vehicle Info
    public string LicensePlate { get; set; } = string.Empty;
    public string OwnerName { get; set; } = string.Empty;
    public VehicleType VehicleType { get; set; }

    // Spot Info
    public string SpotNumber { get; set; } = string.Empty;
    public SpotType SpotType { get; set; }
    public int FloorNumber { get; set; }
}
