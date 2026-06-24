using ParkingLotSystem.API.Domain.Enums;

namespace ParkingLotSystem.API.Domain.Entities.Vehicles;

public class Motorcycle : Vehicle
{
    public Motorcycle()
    {
        Type = VehicleType.Motorcycle;
    }
}