using ParkingLotSystem.API.Domain.Entities.Vehicles;
using ParkingLotSystem.API.Domain.Enums;

namespace ParkingLotSystem.API.Domain.Entities.Spots;

public class HandicappedSpot : ParkingSpot
{
    public HandicappedSpot()
    {
        Type = SpotType.Handicapped;
    }

    public override bool CanFitVehicle(Vehicle vehicle)
        => vehicle.Type == VehicleType.Motorcycle
        || vehicle.Type == VehicleType.Car;
}