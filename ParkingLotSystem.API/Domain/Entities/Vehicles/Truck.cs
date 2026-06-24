using ParkingLotSystem.API.Domain.Enums;

namespace ParkingLotSystem.API.Domain.Entities.Vehicles;

public class Truck : Vehicle
{
    public Truck()
    {
        Type = VehicleType.Truck;
    }
}