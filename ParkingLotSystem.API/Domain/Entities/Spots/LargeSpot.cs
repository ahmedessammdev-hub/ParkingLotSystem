using ParkingLotSystem.API.Domain.Entities.Vehicles;
using ParkingLotSystem.API.Domain.Enums;

namespace ParkingLotSystem.API.Domain.Entities.Spots;

public class LargeSpot : ParkingSpot
{
    public LargeSpot()
    {
        Type = SpotType.Large;
    }

    public override bool CanFitVehicle(Vehicle vehicle)
        => true;
}