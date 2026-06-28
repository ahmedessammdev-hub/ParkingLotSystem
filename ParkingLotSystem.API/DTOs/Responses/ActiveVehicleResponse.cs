using ParkingLotSystem.API.Domain.Enums;

namespace ParkingLotSystem.API.DTOs.Responses;

public class ActiveVehicleResponse
{
    public int TicketId { get; set; }
    public string LicensePlate { get; set; } = string.Empty;
    public string OwnerName { get; set; } = string.Empty;
    public VehicleType VehicleType { get; set; }
    public string SpotNumber { get; set; } = string.Empty;
    public int FloorNumber { get; set; }
    public DateTime EntryTime { get; set; }

    public string TimeParked => 
        $"{(int)(DateTime.UtcNow - EntryTime).TotalHours}h " +
        $"{(DateTime.UtcNow - EntryTime).Minutes}m";
}
