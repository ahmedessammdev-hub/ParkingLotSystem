using ParkingLotSystem.API.Domain.Enums;

namespace ParkingLotSystem.API.Domain.Entities.Vehicles;

public class Car : Vehicle
{
    public Car()
    {
        Type = VehicleType.Car;
    }
}