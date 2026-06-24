using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ParkingLotSystem.API.Domain.Entities.Vehicles;
using ParkingLotSystem.API.Domain.Enums;
using ParkingLotSystem.API.Exceptions;

namespace ParkingLotSystem.API.Domain.Entities.Spots;

[Table("ParkingSpots")]
public abstract class ParkingSpot
{
    public int Id { get; set; }

    [Required, MaxLength(10)]
    public string SpotNumber { get; set; } = string.Empty;

    public SpotType Type { get; protected set; }

    public bool IsOccupied { get; private set; } = false;

    // Foreign Key
    public int FloorId { get; set; }

    // Navigation Properties
    public Floor Floor { get; set; } = null!;

    protected ParkingSpot() { }

    public abstract bool CanFitVehicle(Vehicle vehicle);

    public void Occupy()
    {
        if (IsOccupied)
            throw new SpotAlreadyOccupiedException($"Spot {SpotNumber} is already occupied.");

        IsOccupied = true;
    }

    public void Free()
    {
        if (!IsOccupied)
            throw new SpotAlreadyFreeException($"Spot {SpotNumber} is already free.");

        IsOccupied = false;
    }
}