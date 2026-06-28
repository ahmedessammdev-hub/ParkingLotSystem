using Microsoft.EntityFrameworkCore;
using ParkingLotSystem.API.Data;
using ParkingLotSystem.API.Domain.Entities;
using ParkingLotSystem.API.Domain.Entities.Spots;
using ParkingLotSystem.API.DTOs.Requests;
using ParkingLotSystem.API.DTOs.Responses;
using ParkingLotSystem.API.Exceptions;
using ParkingLotSystem.API.FeeStrategies;
using ParkingLotSystem.API.Interfaces;
using ParkingLotSystem.API.Mappers;

namespace ParkingLotSystem.API.Services;

public class ParkingService : IParkingService
{
    private readonly AppDbContext _context;

    public ParkingService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ParkingTicketResponse> ParkVehicleAsync(ParkVehicleRequest request)
    {
        var vehicle = ParkingMapper.ToVehicleEntity(request);

        var existingVehicle = await _context.Vehicles
            .FirstOrDefaultAsync(v => v.LicensePlate == request.LicensePlate);

        if (existingVehicle is not null)
            vehicle = existingVehicle;
        else
            _context.Vehicles.Add(vehicle);

        var availableSpots = await _context.ParkingSpots
            .Include(s => s.Floor)
            .Where(s => !s.IsOccupied)
            .ToListAsync();

        var spot = availableSpots.FirstOrDefault(s => s.CanFitVehicle(vehicle))
            ?? throw new NoSpotAvailableException(
                $"No available spot for vehicle type: {request.VehicleType}");

        spot.Occupy();

        var ticket = new ParkingTicket
        {
            EntryTime    = DateTime.UtcNow,
            IsActive     = true,
            FeeType      = request.FeeType,
            Vehicle      = vehicle,
            ParkingSpot  = spot
        };

        _context.ParkingTickets.Add(ticket);
        await _context.SaveChangesAsync();

        var fullTicket = await GetTicketWithDetailsAsync(ticket.Id);

        return ParkingMapper.ToTicketResponse(fullTicket);
    }

    public async Task<ParkingTicketResponse> ExitVehicleAsync(int ticketId)
    {
        var ticket = await GetTicketWithDetailsAsync(ticketId);

        if (!ticket.IsActive)
            throw new TicketAlreadyClosedException(
                $"Ticket {ticketId} is already closed.");

        var calculator = FeeCalculatorFactory.GetCalculator(ticket.FeeType);
        var exitTime   = DateTime.UtcNow;
        var fee        = calculator.Calculate(ticket.EntryTime, exitTime);

        ticket.ExitTime   = exitTime;
        ticket.FeeCharged = fee;
        ticket.IsActive   = false;

        ticket.ParkingSpot.Free();

        await _context.SaveChangesAsync();

        return ParkingMapper.ToTicketResponse(ticket);
    }

    public async Task<IEnumerable<ActiveVehicleResponse>> GetActiveVehiclesAsync(int pageNumber, int pageSize)
    {
        var activeTickets = await _context.ParkingTickets
            .Include(t => t.Vehicle)
            .Include(t => t.ParkingSpot)
                .ThenInclude(s => s.Floor)
            .Where(t => t.IsActive)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return activeTickets.Select(ParkingMapper.ToActiveVehicleResponse);
    }

    public async Task<ParkingTicketResponse> GetTicketByIdAsync(int ticketId)
    {
        var ticket = await GetTicketWithDetailsAsync(ticketId);
        return ParkingMapper.ToTicketResponse(ticket);
    }

    public async Task<IEnumerable<ParkingTicketResponse>> GetVehicleHistoryAsync(string licensePlate)
    {
        var tickets = await _context.ParkingTickets
            .Include(t => t.Vehicle)
            .Include(t => t.ParkingSpot)
                .ThenInclude(s => s.Floor)
            .Where(t => t.Vehicle.LicensePlate == licensePlate)
            .OrderByDescending(t => t.EntryTime)
            .ToListAsync();

        return tickets.Select(ParkingMapper.ToTicketResponse);
    }

    private async Task<ParkingTicket> GetTicketWithDetailsAsync(int ticketId)
    {
        var ticket = await _context.ParkingTickets
            .Include(t => t.Vehicle)
            .Include(t => t.ParkingSpot)
                .ThenInclude(s => s.Floor)
            .FirstOrDefaultAsync(t => t.Id == ticketId)
            ?? throw new TicketNotFoundException(
                $"Ticket with Id {ticketId} not found.");

        return ticket;
    }
}
