using ParkingLotSystem.API.Domain.Entities;
using ParkingLotSystem.API.Domain.Entities.Vehicles;
using ParkingLotSystem.API.Domain.Enums;
using ParkingLotSystem.API.DTOs.Requests;
using ParkingLotSystem.API.DTOs.Responses;

namespace ParkingLotSystem.API.Mappers;

public static class ParkingMapper
{
    public static Vehicle ToVehicleEntity(ParkVehicleRequest request)
    {
        return request.VehicleType switch
        {
            VehicleType.Motorcycle => new Motorcycle
            {
                LicensePlate = request.LicensePlate,
                OwnerName = request.OwnerName
            },
            VehicleType.Car => new Car
            {
                LicensePlate = request.LicensePlate,
                OwnerName = request.OwnerName
            },
            VehicleType.Truck => new Truck
            {
                LicensePlate = request.LicensePlate,
                OwnerName = request.OwnerName
            },
            _ => throw new ArgumentException($"Unknown vehicle type: {request.VehicleType}")
        };
    }

    public static ParkingTicketResponse ToTicketResponse(ParkingTicket ticket)
    {
        return new ParkingTicketResponse
        {
            TicketId      = ticket.Id,
            EntryTime     = ticket.EntryTime,
            ExitTime      = ticket.ExitTime,
            FeeCharged    = ticket.FeeCharged,
            IsActive      = ticket.IsActive,
            FeeType       = ticket.FeeType,

            LicensePlate  = ticket.Vehicle.LicensePlate,
            OwnerName     = ticket.Vehicle.OwnerName,
            VehicleType   = ticket.Vehicle.Type,

            SpotNumber    = ticket.ParkingSpot.SpotNumber,
            SpotType      = ticket.ParkingSpot.Type,
            FloorNumber   = ticket.ParkingSpot.Floor.FloorNumber
        };
    }

    public static ActiveVehicleResponse ToActiveVehicleResponse(ParkingTicket ticket)
    {
        return new ActiveVehicleResponse
        {
            TicketId      = ticket.Id,
            LicensePlate  = ticket.Vehicle.LicensePlate,
            OwnerName     = ticket.Vehicle.OwnerName,
            VehicleType   = ticket.Vehicle.Type,
            SpotNumber    = ticket.ParkingSpot.SpotNumber,
            FloorNumber   = ticket.ParkingSpot.Floor.FloorNumber,
            EntryTime     = ticket.EntryTime
        };
    }
}
