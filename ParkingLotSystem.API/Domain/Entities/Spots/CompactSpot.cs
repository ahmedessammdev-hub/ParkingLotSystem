using ParkingLotSystem.API.Domain.Entities.Vehicles;
using ParkingLotSystem.API.Domain.Enums;

namespace ParkingLotSystem.API.Domain.Entities.Spots;

public class CompactSpot : ParkingSpot
{
    public CompactSpot()
    {
        Type = SpotType.Compact;
    }

    public override bool CanFitVehicle(Vehicle vehicle)
        => vehicle.Type == VehicleType.Motorcycle;
}